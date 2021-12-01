using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Arayış
    {
        public static string[] KullanıcıAdınıAra(string aranan_söz)
        {
            string komut_metni = $"SELECT COUNT(Kullanıcı_Adı) FROM {Üyelik.TabloAdı()} " +
                                "WHERE Kullanıcı_Adı LIKE @aranan_söz;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@aranan_söz", $"%{aranan_söz}%");
            long sonuç_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (sonuç_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0];
            }

            string[] sonuçlar = new string[sonuç_niceliği];

            komut_metni = $"SELECT Kullanıcı_Adı FROM {Üyelik.TabloAdı()} " +
                            "WHERE Kullanıcı_Adı LIKE @hepsi GROUP BY Kullanıcı_Adı " +
                            "ORDER BY (CASE " +
                            "WHEN Kullanıcı_Adı = @aranan_söz THEN 0 " + 
                            "WHEN Kullanıcı_Adı LIKE @başta THEN 1 " +
                            "WHEN Kullanıcı_Adı LIKE @ortada THEN 2 " +
                            "WHEN Kullanıcı_Adı LIKE @sonda THEN 3 " +
                            "ELSE 4 END), Kullanıcı_Adı;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@hepsi", $"%{aranan_söz}%");
            komut.Parameters.AddWithValue("@aranan_söz", aranan_söz);
            komut.Parameters.AddWithValue("@başta", $"{aranan_söz}_%");
            komut.Parameters.AddWithValue("@ortada", $"%_{aranan_söz}_%");
            komut.Parameters.AddWithValue("@sonda", $"%_{aranan_söz}");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long döngü_turu = 0;
            while (veri_okuyucu.Read())
            {
                sonuçlar[döngü_turu] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return sonuçlar;
        }
    }
}