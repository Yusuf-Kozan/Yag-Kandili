using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Esas.GeçiciBağlantı;

namespace Esas.VeriTabanı
{
    internal class GeçiciBağlantı
    {
        internal static void BağlantıEkle
                    (Esas.GeçiciBağlantı.GeçiciBağlantı geçici_bağlantı)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} " +
                    "(Bağlantı_Değişkeni, Hedef_Kullanıcı, " +
                    "Başlangıç_Tarihi, Bitiş_Tarihi, İçerik_Türü, " +
                    "Sağlanacak_Belge) VALUES (@bağlantı_değişkeni, " +
                    "@hedef_kullanıcı, @başlangıç_tarihi, " +
                    "@bitiş_tarihi, @içerik_türü, @sağlanacak_belge);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bağlantı_değişkeni",
                                        geçici_bağlantı.BAĞLANTI_DEĞİŞKENİ);
            komut.Parameters.AddWithValue("@hedef_kullanıcı",
                                        geçici_bağlantı.HEDEF_KULLANICI);
            komut.Parameters.AddWithValue("@başlangıç_tarihi",
                            geçici_bağlantı.BAŞLANGIÇ_TARİHİ.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@bitiş_tarihi",
                            geçici_bağlantı.BİTİŞ_TARİHİ.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@içerik_türü",
                                        geçici_bağlantı.İÇERİK_TÜRÜ);
            komut.Parameters.AddWithValue("@sağlanacak_belge",
                                        geçici_bağlantı.SAĞLANACAK_BELGE);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return;
        }
        
        internal static Esas.GeçiciBağlantı.GeçiciBağlantı BağlantıBilgisi(string bağlantı_değişkeni)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} " +
                                "WHERE Bağlantı_Değişkeni = @bağlantı_değişkeni;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bağlantı_değişkeni", bağlantı_değişkeni);
            short tur = 0;
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            Esas.GeçiciBağlantı.GeçiciBağlantı sonuç = new Esas.GeçiciBağlantı.GeçiciBağlantı();
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                sonuç.BAĞLANTI_DEĞİŞKENİ = veri_okuyucu["Bağlantı_Değişkeni"].ToString();
                sonuç.HEDEF_KULLANICI = veri_okuyucu["Hedef_Kullanıcı"].ToString();
                sonuç.BAŞLANGIÇ_TARİHİ = DateTime.ParseExact
                                        (
                                            veri_okuyucu["Başlangıç_Tarihi"].ToString(),
                                            "yyyyMMddHHmmss",
                                            TR
                                        );
                sonuç.BİTİŞ_TARİHİ = DateTime.ParseExact
                                        (
                                            veri_okuyucu["Bitiş_Tarihi"].ToString(),
                                            "yyyyMMddHHmmss",
                                            TR
                                        );
                sonuç.İÇERİK_TÜRÜ = veri_okuyucu["İçerik_Türü"].ToString();
                sonuç.SAĞLANACAK_BELGE = veri_okuyucu["Sağlanacak_Belge"].ToString();
                
                tur++;
                if (tur != 0)
                    break;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return sonuç;
        }

        internal static bool BuBağlantıKullanımda(string bağlantı_değişkeni)
        {
            string komut_metni = $"SELECT COUNT (Bağlantı_Değişkeni) FROM {TabloAdı()} " +
                                "WHERE Bağlantı_Değişkeni = @bağlantı_değişkeni;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bağlantı_değişkeni", bağlantı_değişkeni);
            short nicelik = short.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            if (nicelik == 0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        internal static short BağlantıSüresiDurumu(string bağlantı_değişkeni)
        {
            // -1: gelmemiş , 0: süre içinde , 1: geçmiş
            string komut_metni = "SELECT Başlangıç_Tarihi, Bitiş_Tarihi " +
                                $"FROM {TabloAdı()} WHERE " +
                                "Bağlantı_Değişkeni = @bağlantı_değişkeni;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bağlantı_değişkeni", bağlantı_değişkeni);
            DateTime başlangıç = new DateTime();
            DateTime bitiş = new DateTime();
            short tur = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            while (veri_okuyucu.Read())
            {
                başlangıç = DateTime.ParseExact
                                (
                                    veri_okuyucu["Başlangıç_Tarihi"].ToString(),
                                    "yyyyMMddHHmmss",
                                    TR
                                );
                bitiş = DateTime.ParseExact
                                (
                                    veri_okuyucu["Bitiş_Tarihi"].ToString(),
                                    "yyyyMMddHHmmss",
                                    TR
                                );
                tur++;
                if (tur != 0)
                    break;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            bool başlamış = (DateTime.Compare(DateTime.Now, başlangıç) > 0);
            bool bitmiş = (DateTime.Compare(DateTime.Now, bitiş) > 0);

            if (!başlamış)
            {
                return -1;
            }
            else if (başlamış && !bitmiş)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 9 && belge_içeriği[8].StartsWith("-"))
            {
                return belge_içeriği[1].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}