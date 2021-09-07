using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    class Takip
    {
        public static void TakipEt(Esas.Takip takip)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Takip_Eden, Takip_Edilen, " +
                                "Takip_Düzeyi, Tarih) VALUES (@takip_eden, " +
                                "@takip_edilen, @takip_düzeyi, @tarih);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip.TAKİP_EDEN);
            komut.Parameters.AddWithValue("@takip_edilen", takip.TAKİP_EDİLEN);
            komut.Parameters.AddWithValue("@takip_düzeyi", takip.TAKİP_DÜZEYİ);
            komut.Parameters.AddWithValue("@tarih", takip.TarihMetni());
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static string[,] TakipEdilenKullanıcılarınKimlikleri(string takip_eden)
        {
            // a sıra sayısı olursa [a, 0] = kullanıcı kimliği, [a, 1] = takip düzeyi
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()}" +
                                " WHERE Takip_Eden = @takip_eden;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0,0];
            }

            komut_metni = $"SELECT Takip_Edilen, Takip_Düzeyi FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            string[,] kullanıcılar = new string[takip_niceliği, 2];
            long döngü_turu = 0;

            while (veri_okuyucu.Read())
            {
                kullanıcılar[döngü_turu, 0] = veri_okuyucu["Takip_Edilen"].ToString();
                kullanıcılar[döngü_turu, 1] = veri_okuyucu["Takip_Düzeyi"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return kullanıcılar;
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 7 && belge_içeriği[6].StartsWith("-"))
            {
                return belge_içeriği[6].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}