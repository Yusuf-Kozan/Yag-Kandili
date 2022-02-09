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
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kim, Neyi, Ne_Kadar, Ne_Zaman) " +
                                "VALUES (@kim, @neyi, @ne_kadar, @ne_zaman);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kim", beğeni.KİM);
            komut.Parameters.AddWithValue("@neyi", beğeni.NEYİ);
            komut.Parameters.AddWithValue("@ne_kadar", beğeni.NE_KADAR);
            komut.Parameters.AddWithValue("@ne_zaman", beğeni.NE_ZAMAN.ToString("yyyyMMddHHmmss"));
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static void BeğeniyiSil(string beğenen, string beğenilen)
        {
            string komut_metni = $"DELETE FROM {TabloAdı()} WHERE Kim = @beğenen AND Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenen", beğenen);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static double PaylaşımınBeğeniOrtalaması(string beğenilen)
        {
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            long beğeni_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            if (beğeni_niceliği < 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return 0;
            }

            komut_metni = $"SELECT AVG(Ne_Kadar) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            double ortalama = double.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return ortalama;
        }
        public static double KişininBeğeniOrtalaması(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                $"WHERE Paylaşan = @beğenilen_kişi AND Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen_kişi", kullanıcı_kimliği);
            long beğeni_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            if (beğeni_niceliği < 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return 0;
            }

            komut_metni = $"SELECT AVG(Ne_Kadar) FROM {TabloAdı()} INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                $"WHERE Paylaşan = @beğenilen_kişi AND Eklenti NOT LIKE '%>gizli%';";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen_kişi", kullanıcı_kimliği);
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
        public static int[] BakanKişininBeğenisi(string bakan, string beğenilen)
        {
            // [0] == 0 ise hiç değerlendirme yapılmamıştır.
            // [0] == 1 ise [1] beğenidir.
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} WHERE Kim = @bakan AND Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bakan", bakan);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());

            if (nicelik != 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                if (nicelik > 1)
                {
                    BeğeniyiSil(bakan, beğenilen);
                }
                return new int[]{0, 0};
            }

            komut_metni = $"SELECT Ne_Kadar FROM {TabloAdı()} WHERE Kim = @bakan AND Neyi = @beğenilen;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bakan", bakan);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int beğeni = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            int[] sonuç = new int[]{1, beğeni};
            return sonuç;
        }

        public static string[,][] KişininDeğerlendirdiğiPaylaşımlar(string kullanıcı_kimliği)
        {
            // a sıra sayısı olursa
            // [a,0] == paylaşım bilgisi
            // [a,1] == beğeni değeri

            // Sonuçlar değerlendirme tarihine göre
            // yeniden eskiye doğru sıralanır.

            string komut_metni = $"SELECT COUNT(Neyi) FROM {Beğeni.TabloAdı()} " +
                                $"INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                "WHERE Eklenti NOT LIKE '%>gizli%' AND " +
                                "Kim = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            long paylaşım_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (paylaşım_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0,0][];
            }

            komut_metni = $"SELECT * FROM {Beğeni.TabloAdı()} " +
                        $"INNER JOIN {Paylaşım.TabloAdı()} " +
                        $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' AND " +
                        "Kim = @kullanıcı_kimliği;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long döngü_turu = 0;
            string[,][] sonuç = new string[paylaşım_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                sonuç[döngü_turu, 0] = new string[8];
                sonuç[döngü_turu, 1] = new string[4];

                sonuç[döngü_turu, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                sonuç[döngü_turu, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                sonuç[döngü_turu, 0][2] = veri_okuyucu["Başlık"].ToString();
                sonuç[döngü_turu, 0][3] = veri_okuyucu["İçerik"].ToString();
                sonuç[döngü_turu, 0][4] = veri_okuyucu["Eklenti"].ToString();
                sonuç[döngü_turu, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                sonuç[döngü_turu, 0][6] = veri_okuyucu["Tarih"].ToString();
                sonuç[döngü_turu, 0][7] = veri_okuyucu["Lisans"].ToString();

                sonuç[döngü_turu, 1][0] = veri_okuyucu["Kim"].ToString();
                sonuç[döngü_turu, 1][1] = veri_okuyucu["Neyi"].ToString();
                sonuç[döngü_turu, 1][2] = veri_okuyucu["Ne_Kadar"].ToString();
                sonuç[döngü_turu, 1][3] = veri_okuyucu["Ne_Zaman"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return sonuç;
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