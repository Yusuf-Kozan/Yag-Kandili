using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Üyelik
    {
        public static void ÜyeEkle(Üye üye, string kullanıcı_kimliği)
        {
            // Üye türü bir YAPIDIR, sınıf değildir.

            //INSERT INTO tablo_adı (Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, E_Posta, Başlangıç, Resim, Kimlik)
            //VALUES (@kullanıcı_adı, @ad, @soyadı, @parola, @üstünlük, @e_posta, @başlangıç, @resim, @kimlik);
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, " + 
                                "E_Posta, Başlangıç, Resim, Kimlik) VALUES (@kullanıcı_adı, @ad, @soyadı, " +
                                "@parola, @üstünlük, @e_posta, @başlangıç, @resim, @kimlik);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", üye.KULLANICI_ADI);
            komut.Parameters.AddWithValue("@ad", üye.AD);
            komut.Parameters.AddWithValue("@soyadı", üye.SOYADI);
            komut.Parameters.AddWithValue("@parola", üye.PAROLA);
            komut.Parameters.AddWithValue("@üstünlük", üye.ÜSTÜNLÜK);
            komut.Parameters.AddWithValue("@e_posta", üye.E_POSTA);
            komut.Parameters.AddWithValue("@başlangıç", üye.BAŞLANGIÇ.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@resim", üye.RESİM);
            komut.Parameters.AddWithValue("@kimlik", kullanıcı_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;

            if (!Directory.Exists(üye.DizinYolu()))
            {
                Directory.CreateDirectory(üye.DizinYolu());
            }
        }

        public static ÜyeBil ÜyeBilgileri(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik = @kullanıcı_kimliği LIMIT 1;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            CultureInfo TR = new CultureInfo("tr-TR");
            ÜyeBil üye = new ÜyeBil();
            while (veri_okuyucu.Read())
            {
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.SOYADI = veri_okuyucu["Soyadı"].ToString();
                üye.PAROLA = veri_okuyucu["Parola"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.ParseExact(veri_okuyucu["Başlangıç"].ToString(), "yyyyMMddHHmmss", TR);
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return üye;
        }
        public static ÜyeBil Kimliğinİyesi(string kullanıcı_kimliği)
        {
            //SELECT * FROM tablo_adı WHERE Kimlik=@kullanıcı_kimliği;
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            ÜyeBil üye = new ÜyeBil();
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            while (veri_okuyucu.Read())
            {
                üye = new ÜyeBil();
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.SOYADI = veri_okuyucu["Soyadı"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.ParseExact(veri_okuyucu["Başlangıç"].ToString(), "yyyyMMddHHmmss", TR);
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu = null;
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return üye;
        }
        public static string KullanıcınınKimliği(string kullanıcı_adı)
        {
            string komut_metni = $"SELECT Kimlik FROM {TabloAdı()} WHERE Kullanıcı_Adı = @kullanıcı_adı";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            string kullanıcı_kimliği = komut.ExecuteScalar().ToString();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return kullanıcı_kimliği;
        }

        public static bool KullanıcıAdıKullanımda(string kullanıcı_adı)
        {
            //SELECT COUNT(Kimlik) FROM tablo_adı WHERE Kullanıcı_Adı = @kullanıcı_adı;
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE Kullanıcı_Adı = @kullanıcı_adı";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            int kullanıcı_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            
            if (kullanıcı_niceliği == 0)
            {
                return false;
            }
            return true;
        }
        public static bool EPostaKullanımda(string e_posta)
        {
            //SELECT COUNT(Kimlik) FROM tablo_adı WHERE E_Posta = @e_posta;
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE E_Posta = @e_posta";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@e_posta", e_posta);
            int kullanıcı_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            
            if (kullanıcı_niceliği == 0)
            {
                return false;
            }
            return true;
        }
        public static bool GirişBilgileriDoğru(string kullanıcı_adı, string karılmış_parola)
        {
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} " +
                                    "WHERE Kullanıcı_Adı = @kullanıcı_adı AND Parola = @parola;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            komut.Parameters.AddWithValue("@parola", karılmış_parola);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            if (nicelik == 1)
            {
                return true;
            }
            return false;
        }
        
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[1].StartsWith("-"))
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