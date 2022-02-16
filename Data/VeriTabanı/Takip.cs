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
            if (!TakipEdiliyor(takip.TAKİP_EDEN, takip.TAKİP_EDİLEN))
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
        }
        public static void TakibiBırak(string takip_eden, string takip_edilen)
        {
            if (TakipEdiliyor(takip_eden, takip_edilen))
            {
                string komut_metni = $"DELETE FROM {TabloAdı()} WHERE " +
                                    "Takip_Eden = @takip_eden AND " +
                                    "Takip_Edilen = @takip_edilen;";
                MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
                bağlantı.Open();
                MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
                komut.Parameters.AddWithValue("@takip_eden", takip_eden);
                komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
                komut.ExecuteNonQuery();
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
            }
        }
        public static string[,] TakipEdilenKullanıcılarınKimlikleri(string takip_eden)
        {
            // a sıra sayısı olursa [a, 0] = kullanıcı kimliği, [a, 1] = takip düzeyi
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()}" +
                                " WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen NOT LIKE '%₺%';";
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
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen NOT LIKE '%₺%';";
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
        public static takip[] TakipEdilenKullanıcılar(string takip_eden)
        {
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()} " +
                                "WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen NOT LIKE '%₺%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new takip[0];
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen NOT LIKE '%₺%' " +
                        "ORDER BY Tarih DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long tur = 0;
            takip[] takip_edilenler = new takip[takip_niceliği];
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                takip_edilenler[tur].TAKİP_EDEN = veri_okuyucu["Takip_Eden"].ToString();
                takip_edilenler[tur].TAKİP_EDİLEN = veri_okuyucu["Takip_Edilen"].ToString();
                takip_edilenler[tur].TAKİP_DÜZEYİ = short.Parse(veri_okuyucu["Takip_Düzeyi"].ToString());
                takip_edilenler[tur].TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(),
                                                                "yyyyMMddHHmmss",
                                                                TR);

                tur++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return takip_edilenler;
        }
        public static string[,] TakipEdilenSöyleşilerinKimlikleri(string takip_eden)
        {
            // a sıra sayısı olursa [a, 0] = söyleşi kimliği, [a, 1] = takip düzeyi
            string komut_metni = $"SELECT COUNT(DISTINCT Takip_Edilen) FROM {TabloAdı()}" +
                                " WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen LIKE '%₺Y';";
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

            komut_metni = $"SELECT DISTINCT Takip_Edilen, Takip_Düzeyi FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen LIKE '%₺Y';";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            string[,] söyleşiler = new string[takip_niceliği, 2];
            long döngü_turu = 0;

            while (veri_okuyucu.Read())
            {
                söyleşiler[döngü_turu, 0] = veri_okuyucu["Takip_Edilen"].ToString();
                söyleşiler[döngü_turu, 1] = veri_okuyucu["Takip_Düzeyi"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return söyleşiler;
        }
        public static long TakipçiNiceliği(string takip_edilen)
        {
            string komut_metni = $"SELECT COUNT(Takip_Eden) FROM {TabloAdı()} " +
                                "WHERE Takip_Edilen = @takip_edilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
            long nicelik = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return nicelik;
        }
        public static bool TakipEdiliyor(string takip_eden, string takip_edilen)
        {
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()} " +
                                "WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen = @takip_edilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());
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
        internal static string TabloAdı()
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