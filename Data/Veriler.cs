using System;
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

        public static void Üyek(ÜyeBil üye)
        {
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;"
                + "Pooling=false;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            vtbağ.Open();
            string ek = "INSERT INTO üyelik(Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, E_Posta, Başlangıç)" +
                    " VALUES ('" + üye.KULLANICI_ADI + "','" + üye.AD + "','" + üye.SOYADI + "','" + üye.PAROLA +
                    "','" + üye.ÜSTÜNLÜK + "','" + üye.E_POSTA + "','" + üye.BAŞLANGIÇ.ToString() + "');";
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

        public static void YazılıPaylaş(yazpay paylaşım)
        {
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;"
            + "charset=utf8;";
            IDbConnection vtbağ = new MySqlConnection(bağlantı);
            string ek = "INSERT INTO yazılı_paylaşım(Başlık, İçerik, Paylaşan, Tarih)" +
                " VALUES ('" + paylaşım.BAŞLIK + "','" + paylaşım.İÇERİK + "','" + paylaşım.PAYLAŞAN +
                "','" + paylaşım.TARİH.ToString() + "');";
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
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            ZöçKilmik zöç = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(kilmik);
            IDbConnection bağ = new MySqlConnection(bağlantı);
            string ek = "INSERT INTO oturumlar(Kullanıcı_Adı, Kilmik, Tarih, Son_Tarih, Kapandı_Mı)" +
                " VALUES ('" + zöç.daluk + "','" + kilmik + "','" + zöç.hirat.ToString() + "','" +
                zöç.hirat.AddHours(24).ToString() + "'," + "'0'" + ");";
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
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            string ek = "UPDATE oturumlar " + "SET Kapandı_Mı = '1' , Kapanma_Tarihi = '" + DateTime.Now.ToString() +
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

        public static bool KPDoğru(string kullanıcı_adı, string parola)
        {
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı +
                "' AND Parola = '" + parola + "';";
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
            else
                return true;
        }

        public static string sonn = null;
        public static bool DoğruMu2(string kullanıcı_adı, string kilmik)
        {
            string bağlantı = "Server=127.0.0.1;" + "Database=yagkandili;" + "User ID=YagKandili;" + "Pooling=false;";
            IDbConnection bağ = new MySqlConnection(bağlantı);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            string ek = "SELECT Son_Tarih FROM oturumlar WHERE Kullanıcı_Adı = '" + kullanıcı_adı +
                "' AND Kilmik = '" + kilmik + "' AND Kapandı_Mı = '0'" + ";";

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

        public static bool DoğruMu(string kullanıcı_adı)
        {
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

        public static bool DoğruMu2(string e_posta)
        {
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

        public static ÜyeBil Bilüyom(string kilmik)
        {
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
        public static ÜyeBil Bilüyom1(string kullanıcı_adı)
        {
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
        public static string pylşck;
        public static string Gönderiler()
        {
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
    } 
}
/*paylaşımcık += "<li>" + "<div class=\"geçici\">" + "<div>" +
                    "<div class=\"yeter\">" + "<h3>" + başlık[c] + "</h3>" + "</div>" +
                    "<div class=\"yeter\">" + "<a href=\"Başkası.aspx?k="+ paylaşan[c] 
                    + "\">" + paylaşan[c] + "</a></div></div>" + "<div>" + "<p>" + 
                    içerik[c].Replace("\n", "<br>") + "</p>" + "</div></div></li>";
*/