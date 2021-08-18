using System;
using System.IO;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    class Söyleşi
    {
        public static void Söyle(yeni_söz söz)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Söz, Söyleyen, Oturum, " +
                                "Tarih, Söyleşi, Başlatan_Paylaşım, Bu_İlk) VALUES " +
                                "(@söz, @söyleyen, @oturum, @tarih, @söyleşi, " +
                                "@başlatan_paylaşım, @bu_ilk);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@söz", söz.SÖZ);
            komut.Parameters.AddWithValue("@söyleyen", söz.SÖYLEYEN);
            komut.Parameters.AddWithValue("@oturum", söz.OTURUM);
            komut.Parameters.AddWithValue("@tarih", söz.TARİH);
            komut.Parameters.AddWithValue("@söyleşi", söz.SÖYLEŞİ);
            komut.Parameters.AddWithValue("@başlatan_paylaşım", söz.BAŞLATAN_PAYLAŞIM);
            komut.Parameters.AddWithValue("@bu_ilk", söz.BU_İLK);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static söz[] Yorumlar(string paylaşım_kimliği)
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            string komut_metni = $"SELECT COUNT(Söz) FROM {TabloAdı()} WHERE " +
                                "Başlatan_Paylaşım = @paylaşım AND Bu_İlk = @bu_ilk;";
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım", paylaşım_kimliği);
            komut.Parameters.AddWithValue("@bu_ilk", true);
            long yorum_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Başlatan_Paylaşım = @paylaşım " +
                        "AND Bu_İlk = @bu_ilk ORDER BY Tarih DESC, Genel_Sıra DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım", paylaşım_kimliği);
            komut.Parameters.AddWithValue("@bu_ilk", true);

            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            söz[] yorumlar = new söz[yorum_niceliği];
            long döngü_turu = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                yorumlar[döngü_turu].SÖZ = veri_okuyucu["Söz"].ToString();
                yorumlar[döngü_turu].SÖYLEYEN = veri_okuyucu["Söyleyen"].ToString();
                yorumlar[döngü_turu].OTURUM = veri_okuyucu["Oturum"].ToString();
                yorumlar[döngü_turu].TARİH = veri_okuyucu["Tarih"].ToString();
                yorumlar[döngü_turu].SÖYLEŞİ = veri_okuyucu["Söyleşi"].ToString();
                yorumlar[döngü_turu].BAŞLATAN_PAYLAŞIM = veri_okuyucu["Başlatan_Paylaşım"].ToString();
                yorumlar[döngü_turu].GENEL_SIRA = long.Parse(veri_okuyucu["Genel_Sıra"].ToString());
                yorumlar[döngü_turu].BU_İLK = bool.Parse(veri_okuyucu["Bu_İlk"].ToString());

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return yorumlar;
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 6 && belge_içeriği[4].StartsWith("-"))
            {
                return belge_içeriği[4].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}