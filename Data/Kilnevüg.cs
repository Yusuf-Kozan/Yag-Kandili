using System;

namespace Kilnevüg
{
    public static class Kilnevüg
    {
        public static string YeniEşsizKimlik(string kullanıcıadı)
        {
            Kilmik kilmik = new Kilmik(kullanıcıadı);
            return kilmik.ToString();
        }
        public static ZöçKilmik ÇözülmüşKimlik(string kilmik)
        {
            char[] rus = kilmik.ToCharArray();
            int a = rus.Length;
            string gün = rus[a - 14].ToString() + rus[a - 13].ToString();
            string ay = rus[a - 12].ToString() + rus[a - 11].ToString();
            string yıl = rus[a - 10].ToString() + rus[a - 9].ToString() +
                            rus[a - 8].ToString() + rus[a - 7].ToString();
            string saat = rus[a - 6].ToString() + rus[a - 5].ToString();
            string dakika = rus[a - 4].ToString() + rus[a - 3].ToString();
            string saniye = rus[a - 2].ToString() + rus[a - 1].ToString();
            //k<z/l/kn/z₺53703314423052020131333
            DateTime tarih = new DateTime
            (
                Convert.ToInt32(yıl),
                Convert.ToInt32(ay),
                Convert.ToInt32(gün),
                Convert.ToInt32(saat),
                Convert.ToInt32(dakika),
                Convert.ToInt32(saniye)
            );
            ZöçKilmik zöç = new ZöçKilmik();
            zöç.hirat = tarih;

            string kullanıcıadı = kilmik.Split((char)0x20BA)[0].Replace("/", "a").Replace("*", "e").Replace("-", "ı").Replace("+", "i").
            Replace("<", "o").Replace("|", "o").Replace(">", "u").Replace("_", "u");
            zöç.daluk = kullanıcıadı;
            return zöç;
        }
    }

    public struct Kilmik
    {
        string daluk;
        DateTime hirat;
        int elegtsar;

        public Kilmik(string DALUK)
        {
            daluk = DALUK;
            hirat = DateTime.Now;
            Random rastgele = new Random();
            elegtsar = rastgele.Next(0, int.MaxValue/4);
        }

        public override string ToString()
        {
            return daluk.Replace("a", "/").Replace("e", "*").Replace("ı", "-").Replace("i", "+").
                Replace("o", "<").Replace("ö", "|").Replace("u", ">").Replace("ü", "_") + ((char)0x20BA).ToString() + elegtsar + 
                hirat.ToString().Replace(" ","").Replace(".","").Replace(":","");
        }
    }
    public struct ZöçKilmik
    {
        public string daluk;
        public DateTime hirat;
    }
}