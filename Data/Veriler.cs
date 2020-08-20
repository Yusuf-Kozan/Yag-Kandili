using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas
{
    public class TabanlıVeri
    {
        public static string Mayesküel_tar(DateTime a)
        {
            return a.Year.ToString() + "-" + a.Month.ToString() + "-" + a.Day.ToString() +
                " " + a.Hour.ToString() + ":" + a.Minute.ToString() + ":" + a.Second.ToString();
        }

        private static string bağlantıDizesi = "Server=127.0.0.1;Database=yagkandili;User ID=YagKandili;Pooling=false;";

        public static void Üye_Ekle(ÜyeBil üye, string kimlik)
        {
            //Üyelik veri tabanına yeni üye kaydı yapılıyor.
            string ek = "INSERT INTO üyelik(Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, E_Posta, Başlangıç, Kimlik)" +
                    " VALUES ('" + üye.KULLANICI_ADI + "','" + üye.AD + "','" + üye.SOYADI + "','" + üye.PAROLA +
                    "','" + üye.ÜSTÜNLÜK + "','" + üye.E_POSTA + "','" + üye.BAŞLANGIÇ.ToString() + "','" + kimlik + "');";
            IDbConnection bağ = new MySqlConnection(bağlantıDizesi);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
        }
        public static void Üye_Ekle(çÜye üye, string kimlik)
        {
            //Üye_Ekle fonksiyonunun ÜyeBil yerine çÜye kullanan hâli
            string ek = "INSERT INTO üyelik(Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, E_Posta, Başlangıç, Kimlik)" +
                    " VALUES ('" + üye.KULLANICI_ADI + "','" + üye.AD + "','" + üye.SOYADI + "','" + üye.PAROLA +
                    "','" + üye.ÜSTÜNLÜK + "','" + üye.E_POSTA + "','" + üye.BAŞLANGIÇ.ToString() + "','" + kimlik + "');";
            IDbConnection bağ = new MySqlConnection(bağlantıDizesi);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
        }

        public static void Yazılı_Paylaşım_Yap(yazpay paylaşım)
        {
            //Paylaşım veri tabanına yeni yazılı paylaşım yazılıyor.
            string ek = "INSERT INTO yazılı_paylaşım(Başlık, İçerik, Paylaşan, Kilmik, Tarih)" +
                " VALUES ('" + paylaşım.BAŞLIK + "','" + paylaşım.İÇERİK + "','" + paylaşım.PAYLAŞAN +
                "','" + paylaşım.KİLMİK + "','" + paylaşım.TARİH.ToString() + "');";
            IDbConnection vtbağ = new MySqlConnection(bağlantıDizesi);
            vtbağ.Open();
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
        }

        public static void OturumAç(string kilmik)
        {
            //Oturum veri tabanına yeni oturum kaydı ekleniyor.
            ZöçKilmik zöç = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(kilmik);
            string ek = "INSERT INTO oturumlar(Kullanıcı_Adı, Kilmik, Tarih, Son_Tarih, Kapandı)" +
                " VALUES ('" + zöç.daluk + "','" + kilmik + "','" + zöç.hirat.ToString() + "','" +
                zöç.hirat.AddHours(24).ToString() + "'," + "'0'" + ");";
            IDbConnection bağ = new MySqlConnection(bağlantıDizesi);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
        }
        public static void OturumKapat(string kilmik)
        {
            //Kimlikle eşleşen oturum kaydı "kapalı" olarak işaretleniyor.
            IDbConnection bağ = new MySqlConnection(bağlantıDizesi);
            string ek = "UPDATE oturumlar " + "SET Kapandı = '1' , Kapanma_Tarihi = '" + DateTime.Now.ToString() +
                "' WHERE Kilmik = '" + kilmik + "';";
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
        }

        public static bool KP_Kullanımda(string kullanıcı_adı, string parola)
        {
            //Girilen kullanıcı adı ile parolayı kullanan bir üyelik kaydı olup olmadığı denetleniyor.
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı +
                "' AND Parola = '" + parola + "';";
            IDbConnection bağlantı = new MySqlConnection(bağlantıDizesi);
            bağlantı.Open();
            IDbCommand komut = bağlantı.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            int i = 0;
            while (oku.Read())
            {
                i++; 
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağlantı.Close();
            bağlantı = null;
            if (i != 1)
                return false;
            else
                return true;
        }

        public static string sonn = null;
        public static bool KK_Doğru(string kullanıcı_adı, string kilmik)
        {
            //Kullanıcı adı ve oturum kimliğiyle eşleşen açık bir oturum olup olmadığı denetleniyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            string ek = "SELECT Son_Tarih FROM oturumlar WHERE Kullanıcı_Adı = '" + kullanıcı_adı +
                "' AND Kilmik = '" + kilmik + "' AND Kapandı = '0'" + ";";

            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            int a = 0;

            while (oku.Read())
            {
                a++;
                sonn += "?" + (string)oku["Son_Tarih"];
            }
            string[] sonnn = new string[a];
            sonnn = sonn.Split((char)0x003B);
            DateTime[] tarr = new DateTime[a];
            for (int i = 1; i < a; i++)
            {
                tarr[i] = Convert.ToDateTime(sonnn[i]);
                if (tarr[i] <= DateTime.Now)
                {
                    return false;
                }
                
            }
            return true;
        }

        public static bool Kullanıcı_Var(string kullanıcı_adı)
        {
            //Girilen kullanıcı adının kullanımda olup olmadığını denetliyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı + "';";
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            int i = 0;
            while (oku.Read())
            {
                i++;
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
            if (i != 1)
                return false;
            return true;
        }

        public static bool ePosta_Var(string e_posta)
        {
            //Girilen e-posta adresinin kullanımda olup olmadığı denetleniyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            string ek = "SELECT * FROM üyelik WHERE E_Posta = '" + e_posta + "';";
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            int i = 0;
            while (oku.Read())
            {
                i++;
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
            if (i != 1)
                return false;
            return true;
        }

        public static ÜyeBil Kilmikten_ÜyeBil(string kilmik)
        {
            //Oturum kimliği girilen kullanıcının bilgileri ÜyeBil türünde döndürülüyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;"
                + "Pooling=false;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            vtbağ.Open();
            ZöçKilmik kimlik = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(kilmik);
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kimlik.daluk + "';";
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            ÜyeBil üye = new ÜyeBil();
            while (oku.Read())
            {
                üye.AD = oku["Ad"].ToString();
                üye.SOYADI = oku["Soyadı"].ToString();
                üye.KULLANICI_ADI = oku["Kullanıcı_Adı"].ToString();
                üye.E_POSTA = oku["E_Posta"].ToString();
                üye.ÜSTÜNLÜK = oku["Üstünlük"].ToString();
                üye.BAŞLANGIÇ = Convert.ToDateTime((string)oku["Başlangıç"]);
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
            return üye;
        }
        public static ÜyeBil KullanıcıAdından_ÜyeBil(string kullanıcı_adı)
        {
            //Kullanıcı adı girilen kullanıcının bilgileri ÜyeBil türünde döndürülüyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;"
                + "Pooling=false;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            vtbağ.Open();
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı + "';";
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            ÜyeBil üye = new ÜyeBil();
            while (oku.Read())
            {
                üye.AD = oku["Ad"].ToString();
                üye.SOYADI = oku["Soyadı"].ToString();
                üye.KULLANICI_ADI = oku["Kullanıcı_Adı"].ToString();
                üye.E_POSTA = oku["E_Posta"].ToString();
                üye.ÜSTÜNLÜK = oku["Üstünlük"].ToString();
                üye.BAŞLANGIÇ = Convert.ToDateTime((string)oku["Başlangıç"]);
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
            return üye;
        }
        public static çÜye KullanıcıAdından_çÜye(string kullanıcı_adı)
        {
            //Kullanıcı adı girilen kullanıcının bilgileri çÜye türünde döndürülüyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;"
                + "Pooling=false;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            vtbağ.Open();
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı + "';";
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            çÜye üye = new çÜye();
            while (oku.Read())
            {
                üye.AD = oku["Ad"].ToString();
                üye.SOYADI = oku["Soyadı"].ToString();
                üye.KULLANICI_ADI = oku["Kullanıcı_Adı"].ToString();
                üye.E_POSTA = oku["E_Posta"].ToString();
                üye.ÜSTÜNLÜK = oku["Üstünlük"].ToString();
                üye.BAŞLANGIÇ = Convert.ToDateTime((string)oku["Başlangıç"]);
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
            return üye;
        }
        public static string pylşck;
        public static string Tüm_Yazılı_Paylaşımlar()
        {
            //Yapılan bütün yazılı paylaşımlar HTML listesi ögeleri biçiminde dödürülüyor.
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;"
                + "Pooling=false;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            vtbağ.Open();
            string ek = "SELECT * FROM yazılı_paylaşım;";
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            pylşck = null;
            while (oku.Read())
            {
                pylşck = "<li>" + "<div class=\"geçici\">" + "<div>" +
                    "<div class=\"yeter\">" + "<h3>" + oku["Başlık"].ToString() + "</h3>" + "</div>" +
                    "<div class=\"yeter\">" + "<a href=\"Başkası1.aspx?k=" + oku["Paylaşan"].ToString()
                    + "\">" + oku["Paylaşan"].ToString() + "</a></div></div>" + "<div>" + "<p>" +
                    oku["İçerik"].ToString().Replace("\n", "<br>") + "</p>" + "</div></div></li>" + pylşck;
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
            return pylşck;
        }
        public static void Kullanıcı_Dizini_Oluştur(çÜye üye)
        {
            if (!Directory.Exists(üye.DizinYolu()))
            {
                Directory.CreateDirectory(üye.DizinYolu());
            }
        }
    } 
}
/*paylaşımcık += "<li>" + "<div class=\"geçici\">" + "<div>" +
                    "<div class=\"yeter\">" + "<h3>" + başlık[c] + "</h3>" + "</div>" +
                    "<div class=\"yeter\">" + "<a href=\"Başkası.aspx?k="+ paylaşan[c] 
                    + "\">" + paylaşan[c] + "</a></div></div>" + "<div>" + "<p>" + 
                    içerik[c].Replace("\n", "<br>") + "</p>" + "</div></div></li>";
*/