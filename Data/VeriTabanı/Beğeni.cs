using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Beğeni
    {
        public static void Beğen(Esas.Beğeni beğeni)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kim, Neyi, Ne_Kadar, Ne_Zaman, Oturum) " +
                                "VALUES (@kim, @neyi, @ne_kadar, @ne_zaman, @oturum);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kim", beğeni.KİM);
            komut.Parameters.AddWithValue("@neyi", beğeni.NEYİ);
            komut.Parameters.AddWithValue("@ne_kadar", beğeni.NE_KADAR);
            komut.Parameters.AddWithValue("@ne_zaman", beğeni.NE_ZAMAN.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@oturum", beğeni.OTURUM);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static double BeğeniOrtalaması(string beğenilen)
        {
            string komut_metni = $"SELECT AVG(Ne_Kadar) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            double ortalama = double.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return ortalama;
        }
        public static int DeğerlendirmeNiceliği(string beğenilen)
        {
            string komut_metni = $"SELECT COUNT(Neyi) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return nicelik;
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 6 && belge_içeriği[3].StartsWith("-"))
            {
                return belge_içeriği[5].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}