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
            /*char[] rus = kilmik.ToCharArray();
            int a = rus.Length;
            string gün = rus[a - 14].ToString() + rus[a - 13].ToString();
            string ay = rus[a - 12].ToString() + rus[a - 11].ToString();
            string yıl = rus[a - 10].ToString() + rus[a - 9].ToString() +
                            rus[a - 8].ToString() + rus[a - 7].ToString();
            string saat = rus[a - 6].ToString() + rus[a - 5].ToString();
            string dakika = rus[a - 4].ToString() + rus[a - 3].ToString();
            string saniye = rus[a - 2].ToString() + rus[a - 1].ToString();*/
            //k<z/l/kn/z₺53703314423052020131333
            int a = kilmik.Length;
            string gün = string.Concat(kilmik[a - 14], kilmik[a - 13]);
            string ay = string.Concat(kilmik[a-12], kilmik[a-11]);
            string yıl = string.Concat(string.Concat(kilmik[a-10], kilmik[a-9]), kilmik[a-8], kilmik[a-7]);
            string saat = string.Concat(kilmik[a-6], kilmik[a-5]);
            string dakika = string.Concat(kilmik[a-4], kilmik[a-3]);
            string saniye = string.Concat(kilmik[a-2], kilmik[a-1]);
            DateTime tarih = new DateTime
            (
                Convert.ToInt32(yıl),
                Convert.ToInt32(ay),
                Convert.ToInt32(gün),
                Convert.ToInt32(saat),
                Convert.ToInt32(dakika),
                Convert.ToInt32(saniye)
            );
            ZöçKilmik zöç;
            zöç.hirat = tarih;

            string kullanıcıadı = kilmik.Split((char)0x20BA)[0].Replace("(0061)", "a").Replace("(0065)", "e").Replace("(0131)", "ı").Replace("(0069)", "i").
            Replace("(006F)", "o").Replace("(00F6)", "ö").Replace("(0075)", "u").Replace("(00FC)", "ü");
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
            return daluk.Replace("a", "(0061)").Replace("e", "(0065)").Replace("ı", "(0131)").Replace("i", "(0069)").
                Replace("o", "(006F)").Replace("ö", "(00F6)").Replace("u", "(0075)").Replace("ü", "(00FC)") + ((char)0x20BA).ToString() + elegtsar + 
                hirat.ToString("ddMMyyyyHHmmss");
        }
    }
    public struct ZöçKilmik
    {
        public string daluk;
        public DateTime hirat;
    }
}