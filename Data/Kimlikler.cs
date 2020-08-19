using System;
using Esas;

namespace Kilnevüg
{
    public struct Kullanıcı_Kimliği
    {
        public string Kullanıcı_Adı;
        public string Üstünlük;
        public DateTime Başlangıç;
        public string Dosya_Yolu;
        public string Kimlik;
        public Kullanıcı_Kimliği(string kullanıcı_adı, string üstünlük, DateTime başlangıç, string dosya_yolu)
        {
            this.Kullanıcı_Adı = kullanıcı_adı;
            this.Üstünlük = üstünlük;
            this.Başlangıç = başlangıç;
            this.Dosya_Yolu = dosya_yolu;
            this.Kimlik = kimlikYap(kullanıcı_adı, üstünlük, başlangıç, dosya_yolu);
        }
        public string kimlikYap(string kullanıcı_adı, string üstünlük, DateTime başlangıç, string dosya_yolu)
        {
            string üst = null;
            if (üstünlük == "kullanıcı") {üst = "k";} else if (üstünlük == "yönetici") {üst = "y";} else {üst = "a";}
            string yeniKullanıcıKimliği = rastgele.sayılar(8).ToString() + "(" + kullanıcı_adı + rastgele.harfler(5) + üst
            + ")" + Başlangıç.ToString().Replace(" ","") + rastgele.sayılar(16).ToString() + rastgele.harfler(3);
            return yeniKullanıcıKimliği;
        }
    }
    public struct rastgele
    {
        private string abc = "abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
        public string harfler(int sayı)
        {
            Random random = new Random();
            string sonuç = null;
            for (int i = 0; i < sayı; i++)
            {
                sonuç += abc[random.Next(0,57)];   
            }
            return sonuç;
        }
        public int sayılar(int bölen)
        {
            Random random = new Random();
            return random.Next("0",(int.MaxValue/bölen));
        }
    }
}