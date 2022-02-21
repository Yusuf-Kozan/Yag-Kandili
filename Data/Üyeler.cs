using System;
using System.Globalization;
using Kilnevüg;
namespace Esas
{
    public class ÜyeBil
    {
        public string AD { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string PAROLA { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }
        public string KİMLİK { get; set; }

        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, string resim)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            RESİM = resim;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç, string resim)
        {
            AD = ad;
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
            KULLANICI_ADI = üye.KULLANICI_ADI;
            PAROLA = üye.PAROLA;
            E_POSTA = üye.E_POSTA;
            ÜSTÜNLÜK = üye.ÜSTÜNLÜK;
            BAŞLANGIÇ = üye.BAŞLANGIÇ;
            RESİM = üye.RESİM;
        }
        public ÜyeBil() { }

        public override string ToString()
        {
            return "Ad: " + AD +
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
            string[] küme = new string[]{AD, KULLANICI_ADI, PAROLA, E_POSTA, ÜSTÜNLÜK, BAŞLANGIÇ.ToString()};
            return küme;
        }
    }

    public struct Üye
    {
        public string AD {get; set;}
        public string KULLANICI_ADI {get; set;}
        public string PAROLA {get; set;}
        public string E_POSTA {get; set;}
        public string ÜSTÜNLÜK {get; set;}
        public DateTime BAŞLANGIÇ {get; set;}
        public string RESİM {get; set;}

        public Üye(string ad, string kullanıcı_adı, string parola,
                    string e_posta, string üstünlük, DateTime başlangıç_tarihi, string resim)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcı_adı;
            PAROLA = parola;
            E_POSTA = e_posta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç_tarihi;
            RESİM = resim;
        }

        public string DizinYolu()
        {
            string üst_dizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üst_dizin, KULLANICI_ADI);
        }
        public override string ToString()
        {
            return "Ad: " + AD +
            " | Kullanıcı Adı: " + KULLANICI_ADI + " | E-Posta: " + E_POSTA + " | Üstünlük: "
            + ÜSTÜNLÜK + " | Başlangıç: " + BAŞLANGIÇ.ToString();
        }
        public string[] MetinKümesiYap()
        {
            string[] küme = new string[]{AD, KULLANICI_ADI, PAROLA, E_POSTA, ÜSTÜNLÜK, BAŞLANGIÇ.ToString("yyyyMMddHHmmss"), RESİM};
            return küme;
        }
    }
    public struct parolasız_üye
    {
        public string AD { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }
        public string KİMLİK { get; set; }

        public parolasız_üye (string[] üye_bilgileri)
        {
            AD = üye_bilgileri[0];
            KULLANICI_ADI = üye_bilgileri[1];
            E_POSTA = üye_bilgileri[2];
            ÜSTÜNLÜK = üye_bilgileri[3];
            CultureInfo TR = new CultureInfo("tr-TR");
            BAŞLANGIÇ = DateTime.ParseExact(üye_bilgileri[4], "yyyyMMddHHmmss", TR);
            RESİM = üye_bilgileri[5];
            KİMLİK = üye_bilgileri[6];
        }
        public string DizinYolu()
        {
            string üst_dizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üst_dizin, KULLANICI_ADI);
        }
    }
}
