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
    }

}
