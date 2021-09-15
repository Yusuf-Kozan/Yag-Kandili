using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Paylaşım
    {
        public static void PaylaşımYap(Esas.Paylaşım paylaşım)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kimlik2, Başlık, İçerik, Eklenti, " +
                                "Paylaşan, Oturum, Tarih, Lisans) VALUES (@kimlik2, @başlık, " +
                                "@içerik, @eklenti, @paylaşan, @oturum, @tarih, @lisans);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", paylaşım.KİMLİK_2);
            komut.Parameters.AddWithValue("@başlık", paylaşım.BAŞLIK);
            komut.Parameters.AddWithValue("@içerik", paylaşım.İÇERİK);
            komut.Parameters.AddWithValue("@eklenti", paylaşım.EKLENTİ);
            komut.Parameters.AddWithValue("@paylaşan", paylaşım.PAYLAŞAN);
            komut.Parameters.AddWithValue("@oturum", paylaşım.OTURUM);
            komut.Parameters.AddWithValue("@tarih", paylaşım.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@lisans", paylaşım.LİSANS);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
        }
        public static void PaylaşımYap(Esas.yeni_paylaşım paylaşım)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kimlik2, Başlık, İçerik, Eklenti, " +
                                "Paylaşan, Oturum, Tarih, Lisans) VALUES (@kimlik2, @başlık, " +
                                "@içerik, @eklenti, @paylaşan, @oturum, @tarih, @lisans);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", paylaşım.KİMLİK_2);
            komut.Parameters.AddWithValue("@başlık", paylaşım.BAŞLIK);
            komut.Parameters.AddWithValue("@içerik", paylaşım.İÇERİK);
            komut.Parameters.AddWithValue("@eklenti", paylaşım.EKLENTİ);
            komut.Parameters.AddWithValue("@paylaşan", paylaşım.PAYLAŞAN);
            komut.Parameters.AddWithValue("@oturum", paylaşım.OTURUM);
            komut.Parameters.AddWithValue("@tarih", paylaşım.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@lisans", paylaşım.LİSANS);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
        }
        public static Esas.Paylaşım TekPaylaşım(string paylaşım_kimliği)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik2 = @paylaşım_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım_kimliği", paylaşım_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            Esas.Paylaşım paylaşım = new Esas.Paylaşım();
            while (veri_okuyucu.Read())
            {
                if (döngü_turu > 0)
                {
                    break;
                }
                paylaşım.KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşım.KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşım.BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşım.İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşım.EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşım.PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                paylaşım.OTURUM = veri_okuyucu["Oturum"].ToString();
                paylaşım.TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(), "yyyyMMddHHmmss", TR);
                paylaşım.LİSANS = veri_okuyucu["Lisans"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return paylaşım;
        }
        public static Esas.Paylaşım[] TümPaylaşımlar()
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            Esas.Paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new Esas.Paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new Esas.Paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new Esas.Paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu].OTURUM = veri_okuyucu["Oturum"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        public static string[,][] KullanıcıBilgileriyleTümPaylaşımlar()
        {
            string komut_metni = $"SELECT COUNT(Kimlik2) FROM {Paylaşım.TabloAdı()} INNER JOIN {Üyelik.TabloAdı()} " +
                                $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                                "WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            string[,][] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new string[0,0][];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {Paylaşım.TabloAdı()} INNER JOIN {Üyelik.TabloAdı()} " +
                        $" ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new string[paylaşım_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu, 0] = new string[9]; //paylaşım bilgileri
                paylaşımlar[döngü_turu, 1] = new string[8]; //paylaşan bilgileri

                paylaşımlar[döngü_turu, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                paylaşımlar[döngü_turu, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu, 0][2] = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu, 0][3] = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu, 0][4] = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu, 0][6] = veri_okuyucu["Oturum"].ToString();
                paylaşımlar[döngü_turu, 0][7] = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu, 0][8] = veri_okuyucu["Lisans"].ToString();

                paylaşımlar[döngü_turu, 1][0] = veri_okuyucu["Ad"].ToString();
                paylaşımlar[döngü_turu, 1][1] = veri_okuyucu["Soyadı"].ToString();
                paylaşımlar[döngü_turu, 1][2] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                paylaşımlar[döngü_turu, 1][3] = veri_okuyucu["E_Posta"].ToString();
                paylaşımlar[döngü_turu, 1][4] = veri_okuyucu["Üstünlük"].ToString();
                paylaşımlar[döngü_turu, 1][5] = veri_okuyucu["Başlangıç"].ToString();
                paylaşımlar[döngü_turu, 1][6] = veri_okuyucu["Resim"].ToString();
                paylaşımlar[döngü_turu, 1][7] = veri_okuyucu["Kimlik"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        public static string[,][] TakipEdilenlerinBilgileriylePaylaşımları(string[,] takip_edilenler)
        {
            // a sıra sayısı olursa
            // takip_edilenler[a, 0] = kullanıcı kimliği, takip_edilenler[a, 1] = takip düzeyi
            
            // [a, 0][] paylaşım bilgileri, [a, 1][] paylaşan bilgileri
            string SorguİçinKimlikler = "";
            for (long i = 0; i < takip_edilenler.GetLongLength(0); i++)
            {
                SorguİçinKimlikler += $"@takip{i.ToString()}, ";
            }
            SorguİçinKimlikler = $"({SorguİçinKimlikler.Substring(0,(SorguİçinKimlikler.Length-2))})";
            string komut_metni = $"SELECT COUNT(Kimlik2) FROM {Paylaşım.TabloAdı()} " +
                                $"INNER JOIN {Üyelik.TabloAdı()} " +
                                $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                                "WHERE Eklenti NOT LIKE '%>gizli%' AND" + 
                                $" Paylaşan IN {SorguİçinKimlikler};";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            string[,][] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new string[0,0][];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {Paylaşım.TabloAdı()} " +
                        $"INNER JOIN {Üyelik.TabloAdı()} " +
                        $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' AND" + 
                        $" Paylaşan IN {SorguİçinKimlikler} " +
                        "ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new string[paylaşım_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu, 0] = new string[9]; //paylaşım bilgileri
                paylaşımlar[döngü_turu, 1] = new string[8]; //paylaşan bilgileri

                paylaşımlar[döngü_turu, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                paylaşımlar[döngü_turu, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu, 0][2] = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu, 0][3] = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu, 0][4] = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu, 0][6] = veri_okuyucu["Oturum"].ToString();
                paylaşımlar[döngü_turu, 0][7] = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu, 0][8] = veri_okuyucu["Lisans"].ToString();

                paylaşımlar[döngü_turu, 1][0] = veri_okuyucu["Ad"].ToString();
                paylaşımlar[döngü_turu, 1][1] = veri_okuyucu["Soyadı"].ToString();
                paylaşımlar[döngü_turu, 1][2] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                paylaşımlar[döngü_turu, 1][3] = veri_okuyucu["E_Posta"].ToString();
                paylaşımlar[döngü_turu, 1][4] = veri_okuyucu["Üstünlük"].ToString();
                paylaşımlar[döngü_turu, 1][5] = veri_okuyucu["Başlangıç"].ToString();
                paylaşımlar[döngü_turu, 1][6] = veri_okuyucu["Resim"].ToString();
                paylaşımlar[döngü_turu, 1][7] = veri_okuyucu["Kimlik"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        public static paylaşım[] KişininTümAçıkPaylaşımları(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} " +
                                "WHERE Paylaşan = @paylaşan AND Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Paylaşan = @paylaşan AND " +
                        "Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu].OTURUM = veri_okuyucu["Oturum"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        public static int TümPaylaşımlarınNiceliği()
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return paylaşım_niceliği;
        }
        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[3].StartsWith("-"))
            {
                return belge_içeriği[3].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}