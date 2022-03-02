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