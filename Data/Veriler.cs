using System;
using System.IO;
using System.Data;
using System.Globalization;
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

        private static void komutGönder(string ek)
        {
            // Veri tabanına gönderilecek olan komutun gönderme
            // kısmını yapmak için bir kolaylaştırıcı.
            IDbConnection bağ = new MySqlConnection(bağlantıDizesi);
            bağ.Open();
            IDbCommand komut = bağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            oku.Close(); oku = null;
            komut.Dispose(); komut = null;
            bağ.Close(); bağ = null;
        }
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
        public static void Üye_Ekle(çÜye üye, string kimlik, string resim)
        {
            //Üye_Ekle fonksiyonunun ÜyeBil yerine çÜye kullanan hâli
            string ek = "INSERT INTO üyelik(Kullanıcı_Adı, Ad, Soyadı, Parola, Üstünlük, E_Posta, Başlangıç, Resim, Kimlik)" +
                    " VALUES ('" + üye.KULLANICI_ADI + "','" + üye.AD + "','" + üye.SOYADI + "','" + üye.PAROLA +
                    "','" + üye.ÜSTÜNLÜK + "','" + üye.E_POSTA + "','" + üye.BAŞLANGIÇ.ToString() + "','" + resim + "','" +
                    kimlik + "');";
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
        public static void OturumAç(string kilmik)
        {
            //Oturum veri tabanına yeni oturum kaydı ekleniyor.
            ZöçKilmik zöç = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(kilmik);
            string ek = "INSERT INTO oturumlar(Kullanıcı_Adı, Kilmik, Tarih, Son_Tarih, Kapandı)" +
                " VALUES ('" + zöç.daluk + "','" + kilmik + "','" + zöç.hirat.ToString() + "','" +
                zöç.hirat.AddHours(4).ToString() + "'," + "'0'" + ");";
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
            string bag = "Server=127.0.0.1;Database=yagkandili;User ID=YagKandili;Pooling=false;";
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı +
                "' AND Parola = '" + parola + "';";
            IDbConnection bağlantı = new MySqlConnection(bag);
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
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            bağ.Close();
            bağ = null;
            
            string esas_son_tarih = sonn.Split('?')[a];
            DateTime est = Convert.ToDateTime(esas_son_tarih);
            if (est > DateTime.Now)
                return true;
            else
                return false;
            /*string[] sonnn = new string[a];
            sonnn = sonn.Split('?');//(char)0x003B
            DateTime[] tarr = new DateTime[a];
            for (int i = 1; i < a; i++)
            {
                tarr[i] = Convert.ToDateTime(sonnn[i]);
                if (tarr[i] > DateTime.Now)
                {
                    return true;
                }
                
            }
            return false;*/
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
                üye.RESİM = oku["Resim"].ToString();
                üye.KİMLİK = oku["Kimlik"].ToString();
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
                üye.RESİM = oku["Resim"].ToString();
                üye.KİMLİK = oku["Kimlik"].ToString();
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
        public static Üye KullanıcıAdından_Üye(string kullanıcı_adı)
        {
            IDbConnection vtbağ = new MySqlConnection(bağlantıDizesi);
            vtbağ.Open();
            string ek = "SELECT * FROM üyelik WHERE Kullanıcı_Adı = '" + kullanıcı_adı + "';";
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            Üye üye = new Üye();
            while (oku.Read())
            {
                üye.AD = oku["Ad"].ToString();
                üye.SOYADI = oku["Soyadı"].ToString();
                üye.KULLANICI_ADI = oku["Kullanıcı_Adı"].ToString();
                üye.E_POSTA = oku["E_Posta"].ToString();
                üye.ÜSTÜNLÜK = oku["Üstünlük"].ToString();
                üye.BAŞLANGIÇ = Convert.ToDateTime((string)oku["Başlangıç"]);
                üye.RESİM = oku["Resim"].ToString();
            }
            oku.Close();
            oku = null;
            komut.Dispose();
            komut = null;
            vtbağ.Close();
            vtbağ = null;
            return üye;
        }
        public static void Kullanıcı_Dizini_Oluştur(çÜye üye)
        {
            if (!Directory.Exists(üye.DizinYolu()))
            {
                Directory.CreateDirectory(üye.DizinYolu());
            }
        }

        /* Paylaşımlarla ilgili şeyler burada başlıyor. */
        public static void Paylaş(Paylaşım paylaşım)
        {
            // Farklı paylaşım türlerinin ayracı Eklenti olduğundan
            // bu fonksiyon tüm paylaşım türleri için kullanılabilmeli.
            string ek = "INSERT INTO paylaşımlar (Kimlik2, Başlık, İçerik, Eklenti, Paylaşan, " +
                        $"Oturum, Tarih) VALUES ('{paylaşım.KİMLİK_2}', '{paylaşım.BAŞLIK}', " +
                        $"'{paylaşım.İÇERİK}', '{paylaşım.EKLENTİ}', '{paylaşım.PAYLAŞAN}', '{paylaşım.OTURUM}', " +
                        $"'{paylaşım.TARİH.ToString("yyyyMMddHHmmss")}');";
            komutGönder(ek);
        }
        public static Paylaşım[] TümPaylaşımlar()
        {
        //SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Kimlik1 DESC, Tarih DESC;
        //SELECT * FROM paylaşımlar WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Kimlik1 DESC, Tarih DESC;
            IDbConnection vtbağ = new MySqlConnection(bağlantıDizesi);
            string ek = "SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Kimlik1 DESC, Tarih DESC;";
            int paylaşım_sayısı = 0;
            vtbağ.Open();
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                paylaşım_sayısı = int.Parse(komut.ExecuteScalar().ToString());
            }
            ek = "SELECT * FROM paylaşımlar WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Kimlik1 DESC, Tarih DESC;";
            komut.CommandText = ek;
            Paylaşım[] paylaşımlar = new Paylaşım[paylaşım_sayısı];
            int sayaç = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (oku.Read())
            {
                paylaşımlar[sayaç].KİMLİK_1 = long.Parse(oku["Kimlik1"].ToString());
                paylaşımlar[sayaç].KİMLİK_2 = oku["Kimlik2"].ToString();
                paylaşımlar[sayaç].BAŞLIK = oku["Başlık"].ToString();
                paylaşımlar[sayaç].İÇERİK = oku["İçerik"].ToString();
                paylaşımlar[sayaç].EKLENTİ = oku["Eklenti"].ToString();
                paylaşımlar[sayaç].PAYLAŞAN = oku["Paylaşan"].ToString();
                paylaşımlar[sayaç].OTURUM = oku["Oturum"].ToString();
                paylaşımlar[sayaç].TARİH = DateTime.ParseExact(oku["Tarih"].ToString(), "yyyyMMddHHmmss", TR);
                sayaç++;
            }
            oku.Close(); oku = null;
            komut.Dispose(); komut = null;
            vtbağ.Close(); vtbağ = null;
            return paylaşımlar;
        }
        public static Paylaşım[] BelirliKişininPaylaşımları(ÜyeBil kişi)
        {
        //SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;
        //SELECT * FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;
            IDbConnection vtbağ = new MySqlConnection(bağlantıDizesi);
            string ek = $"SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;";
            int paylaşım_sayısı = 0;
            vtbağ.Open();
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                paylaşım_sayısı = int.Parse(komut.ExecuteScalar().ToString());
            }
            ek = $"SELECT * FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;";
            komut.CommandText = ek;
            Paylaşım[] paylaşımlar = new Paylaşım[paylaşım_sayısı];
            int sayaç = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (oku.Read())
            {
                paylaşımlar[sayaç].KİMLİK_1 = long.Parse(oku["Kimlik1"].ToString());
                paylaşımlar[sayaç].KİMLİK_2 = oku["Kimlik2"].ToString();
                paylaşımlar[sayaç].BAŞLIK = oku["Başlık"].ToString();
                paylaşımlar[sayaç].İÇERİK = oku["İçerik"].ToString();
                paylaşımlar[sayaç].EKLENTİ = oku["Eklenti"].ToString();
                paylaşımlar[sayaç].PAYLAŞAN = oku["Paylaşan"].ToString();
                paylaşımlar[sayaç].OTURUM = oku["Oturum"].ToString();
                paylaşımlar[sayaç].TARİH = DateTime.ParseExact(oku["Tarih"].ToString(), "yyyyMMddHHmmss", TR);
                sayaç++;
            }
            oku.Close(); oku = null;
            komut.Dispose(); komut = null;
            vtbağ.Close(); vtbağ = null;
            return paylaşımlar;
        }
        public static Paylaşım[] BelirliKişininPaylaşımları(string kullanıcı_adı)
        {
        //SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;
        //SELECT * FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;
            IDbConnection vtbağ = new MySqlConnection(bağlantıDizesi);
            ÜyeBil kişi = KullanıcıAdından_ÜyeBil(kullanıcı_adı);
            string ek = $"SELECT COUNT(Kimlik2) FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;";
            int paylaşım_sayısı = 0;
            vtbağ.Open();
            IDbCommand komut = vtbağ.CreateCommand();
            komut.CommandText = ek;
            IDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                paylaşım_sayısı = int.Parse(komut.ExecuteScalar().ToString());
            }
            ek = $"SELECT * FROM paylaşımlar WHERE Paylaşan = '{kişi.KİMLİK}' ORDER BY Kimlik1 DESC, Tarih DESC;";
            komut.CommandText = ek;
            Paylaşım[] paylaşımlar = new Paylaşım[paylaşım_sayısı];
            int sayaç = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (oku.Read())
            {
                paylaşımlar[sayaç].KİMLİK_1 = long.Parse(oku["Kimlik1"].ToString());
                paylaşımlar[sayaç].KİMLİK_2 = oku["Kimlik2"].ToString();
                paylaşımlar[sayaç].BAŞLIK = oku["Başlık"].ToString();
                paylaşımlar[sayaç].İÇERİK = oku["İçerik"].ToString();
                paylaşımlar[sayaç].EKLENTİ = oku["Eklenti"].ToString();
                paylaşımlar[sayaç].PAYLAŞAN = oku["Paylaşan"].ToString();
                paylaşımlar[sayaç].OTURUM = oku["Oturum"].ToString();
                paylaşımlar[sayaç].TARİH = DateTime.ParseExact(oku["Tarih"].ToString(), "yyyyMMddHHmmss", TR);
                sayaç++;
            }
            oku.Close(); oku = null;
            komut.Dispose(); komut = null;
            vtbağ.Close(); vtbağ = null;
            return paylaşımlar;
        }
    } 
}
/*paylaşımcık += "<li>" + "<div class=\"geçici\">" + "<div>" +
                    "<div class=\"yeter\">" + "<h3>" + başlık[c] + "</h3>" + "</div>" +
                    "<div class=\"yeter\">" + "<a href=\"Başkası.aspx?k="+ paylaşan[c] 
                    + "\">" + paylaşan[c] + "</a></div></div>" + "<div>" + "<p>" + 
                    içerik[c].Replace("\n", "<br>") + "</p>" + "</div></div></li>";
*/