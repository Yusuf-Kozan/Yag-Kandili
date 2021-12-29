using System;
using System.IO;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Oturum
    {
        public static void OturumAç(string oturum_kimliği, string kullanıcı_kimliği)
        {
            // Kullanıcı_Kimliği, Oturum_Kimliği, Başlangıç_Tarihi, Son_Tarih
            string komut_metni = $"INSERT INTO {TabloAdı()}(Kullanıcı_Kimliği, Oturum_Kimliği, Başlangıç_Tarihi, Son_Tarih) "+
                        $"VALUES (@kullanıcı_kimliği, @oturum_kimliği, @başlangıç_tarihi, @son_tarih);";
            DateTime başlangıç_tarihi = DateTime.Now;
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@başlangıç_tarihi", başlangıç_tarihi.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@son_tarih", başlangıç_tarihi.AddHours(4).ToString("yyyyMMddHHmmss"));
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        public static void OturumKapat(string oturum_kimliği)
        {
            string komut_metni = $"DELETE FROM {TabloAdı()} WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        public static bool BuOturumAçık(string oturum_kimliği, string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Oturum_Kimliği) FROM {TabloAdı()} WHERE Oturum_Kimliği = @oturum_kimliği AND " +
                                $"Kullanıcı_Kimliği = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            int oturum_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (oturum_niceliği == 0)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return false;
            }

            komut_metni = $"SELECT Son_Tarih FROM {TabloAdı()} WHERE " + 
                            "Oturum_Kimliği = @oturum_kimliği AND Kullanıcı_Kimliği = @kullanıcı_kimliği;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            DateTime son_tarih = DateTime.ParseExact(komut.ExecuteScalar().ToString(),
                                                    "yyyyMMddHHmmss", TR);
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            if (DateTime.Now.CompareTo(son_tarih) < 0)
            {
                return true;
            }
            return false;
        }
        public static string Oturumunİyesi(string oturum_kimliği)
        {
            // Oturum kimliği bilinen kullanıcının kullanıcı kimliğini döndürür.
            string komut_metni = $"SELECT Kullanıcı_Kimliği FROM {TabloAdı()} " + 
                                    "WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            string kullanıcı_kimliği = komut.ExecuteScalar().ToString();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return kullanıcı_kimliği;
        }
        public static bool KimlikKullanımda(string oturum_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Oturum_Kimliği) FROM {TabloAdı()} " +
                                    "WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
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
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[2].StartsWith("-"))
            {
                return belge_içeriği[2].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}