using System;
using Kilnevüg;
namespace Esas
{
    public class ÜyeBil
    {
        public string AD { get; set; }
        public string SOYADI { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string PAROLA { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }

        public ÜyeBil(string ad, string soyadı, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç)
        {
            AD = ad;
            SOYADI = soyadı;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç;
        }
        public ÜyeBil(string ad, string soyadı, string kullanıcıAdı, string parola, string ePosta, string üstünlük)
        {
            AD = ad;
            SOYADI = soyadı;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
        }
        public ÜyeBil(string ad, string soyadı, string kullanıcıAdı, string parola, string ePosta, string üstünlük, string resim)
        {
            AD = ad;
            SOYADI = soyadı;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            RESİM = resim;
        }
        public ÜyeBil(string ad, string soyadı, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç, string resim)
        {
            AD = ad;
            SOYADI = soyadı;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç;
            RESİM = resim;
        }
        public ÜyeBil(Üye üye)
        {
            AD = üye.AD;
            SOYADI = üye.SOYADI;
            KULLANICI_ADI = üye.KULLANICI_ADI;
            PAROLA = üye.PAROLA;
            E_POSTA = üye.E_POSTA;
            ÜSTÜNLÜK = üye.ÜSTÜNLÜK;
            BAŞLANGIÇ = üye.BAŞLANGIÇ;
            RESİM = üye.RESİM;
        }
        public ÜyeBil(çÜye üye)
        {
            AD = üye.AD;
            SOYADI = üye.SOYADI;
            KULLANICI_ADI = üye.KULLANICI_ADI;
            PAROLA = üye.PAROLA;
            E_POSTA = üye.E_POSTA;
            BAŞLANGIÇ = üye.BAŞLANGIÇ;
            ÜSTÜNLÜK = üye.ÜSTÜNLÜK;
        }
        public ÜyeBil() { }

        public override string ToString()
        {
            return "Ad: " + AD + " | Soyadı: " + SOYADI +
            " | Kullanıcı Adı: " + KULLANICI_ADI + " | E-Posta: " + E_POSTA + " | Üstünlük: "
            + ÜSTÜNLÜK + " | Başlangıç: " + BAŞLANGIÇ.ToString();
        }
        public string DizinYolu()
        {
            string üstDizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üstDizin, KULLANICI_ADI);
        }
        public string[] MetinKümesiYap()
        {
            string[] küme = new string[]{AD, SOYADI, KULLANICI_ADI, PAROLA, E_POSTA, ÜSTÜNLÜK, BAŞLANGIÇ.ToString()};
            return küme;
        }
    }

    public struct Üye
    {
        public string AD {get; set;}
        public string SOYADI;
        public string KULLANICI_ADI;
        public string PAROLA;
        public string E_POSTA;
        public string ÜSTÜNLÜK;
        public DateTime BAŞLANGIÇ;
        public string RESİM;

        public string DizinYolu()
        {
            string üstDizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üstDizin, KULLANICI_ADI);
        }
        public override string ToString()
        {
            return "Ad: " + AD + " | Soyadı: " + SOYADI +
            " | Kullanıcı Adı: " + KULLANICI_ADI + " | E-Posta: " + E_POSTA + " | Üstünlük: "
            + ÜSTÜNLÜK + " | Başlangıç: " + BAŞLANGIÇ.ToString();
        }
        /*public string[] MetinKümesiYap()
        {
            string[] küme = new string{AD, SOYADI, KULLANICI_ADI, PAROLA, E_POSTA, ÜSTÜNLÜK, BAŞLANGIÇ.ToString()};
            return küme;
        }*/
    }
    public struct çÜye
    {
        //Üye bilgisi çekirdeği: Sonraki sürümlerde kullanılacak olan Üstünlük ve Resim değişkenlerini taşımıyor.
        public string AD {get; set;}
        public string SOYADI;
        public string KULLANICI_ADI;
        public string PAROLA;
        public string E_POSTA;
        public DateTime BAŞLANGIÇ;
        public string ÜSTÜNLÜK;

        public string DizinYolu()
        {
            string üstDizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üstDizin, KULLANICI_ADI);
        }
        public override string ToString()
        {
            return "Ad: " + AD + " | Soyadı: " + SOYADI +
            " | Kullanıcı Adı: " + KULLANICI_ADI + " | E-Posta: " + E_POSTA +
            " | Üstünlük: " + ÜSTÜNLÜK + " | Başlangıç: " + BAŞLANGIÇ.ToString();
        }
        public string[] MetinKümesiYap()
        {
            string[] küme = new string[]{AD, SOYADI, KULLANICI_ADI, PAROLA, E_POSTA, ÜSTÜNLÜK, BAŞLANGIÇ.ToString()};
            return küme;
        }
    }

}
