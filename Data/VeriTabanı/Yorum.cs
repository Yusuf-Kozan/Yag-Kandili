using System;
using System.IO;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    class Yorum
    {
        public static void Yorumla(Esas.Yorum yorum)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Tür, Kimden, Neye, İçerik, Kimlik, Ne_Zaman, Oturum) " +
                        $"VALUES ('Yorum', @kimden, '{yorum.NEYE}', @içerik, '{yorum.KİMLİK}', " +
                        $"'{yorum.NE_ZAMAN.ToString("yyyyMMddHHmmss")}', @oturum_kimliği);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimden", yorum.KİM);
            komut.Parameters.AddWithValue("@içerik", yorum.İÇERİK);
            komut.Parameters.AddWithValue("@oturum_kimliği", yorum.OTURUM);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        public static Esas.Yorum[] PaylaşımınBirincilYorumları(string İkinciPaylaşımKimliği)
        {
            int yorum_niceliği = BirincilYorumlarınNiceliği(İkinciPaylaşımKimliği);
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Tür = 'Yorum' " +
                        "AND Neye = @paylaşım_kimliği ORDER BY Ne_Zaman DESC;";
                        
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım_kimliği", İkinciPaylaşımKimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();

            Esas.Yorum[] yorumlar = new Esas.Yorum[yorum_niceliği];
            int döngü_turu = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                yorumlar[döngü_turu] = new Esas.Yorum();
                yorumlar[döngü_turu].KİM = veri_okuyucu["Kimden"].ToString();
                yorumlar[döngü_turu].NEYE = veri_okuyucu["Neye"].ToString();
                yorumlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                yorumlar[döngü_turu].NE_ZAMAN = DateTime.ParseExact(veri_okuyucu["Ne_Zaman"].ToString(), "yyyyMMddHHmmss", TR);
                yorumlar[döngü_turu].OTURUM = veri_okuyucu["Oturum"].ToString();
                yorumlar[döngü_turu].KİMLİK = veri_okuyucu["Kimlik"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu = null;
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return yorumlar;
        }
        private static int BirincilYorumlarınNiceliği(string İkinciPaylaşımKimliği)
        {
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE Tür = 'Yorum' " +
                        "AND Neye = @paylaşım_kimliği ORDER BY Ne_Zaman DESC;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım_kimliği", İkinciPaylaşımKimliği);
            int yorum_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return yorum_niceliği;
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[4].StartsWith("-"))
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