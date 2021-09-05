using System;
using System.Globalization;

namespace Esas
{
    public class Takip
    {
        public string TAKİP_EDEN {get; set;}
        public string TAKİP_EDİLEN {get; set;}
        public short TAKİP_DÜZEYİ {get; set;}
        public DateTime TARİH {get; set;}
        public string OTURUM {get; set;}

        public Takip() {}
        public Takip(string takip_eden, string takip_edilen, short takip_düzeyi,
                    DateTime tarih, string oturum)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            TARİH = tarih;
            OTURUM = oturum;
        }
        public Takip(string takip_eden, string takip_edilen, short takip_düzeyi,
                    string tarih, string oturum)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
            OTURUM = oturum;
        }

        public string DönüştürülmüşTarih()
        {
            return TARİH.ToString("yyyyMMddHHmmss");
        }
    }

    public class Beğeni
    {
        public string KİM {get; set;} //Beğenenin kullanıcı kimliği
        public string NEYİ {get; set;} //Beğenilen paylaşımın Kimlik2'si
        public int NE_KADAR {get; set;}
        public DateTime NE_ZAMAN {get; set;} //Beğenme düğmesine basılan anın zaman damgası
        public string OTURUM {get; set;} //Beğenenin o andaki oturum kimliği

        public Beğeni() {}
        public Beğeni(string kim, string neyi, int ne_kadar, string ne_zaman, string oturum)
        {
            CultureInfo TR = new CultureInfo("tr-TR");
            KİM = kim;
            NEYİ = neyi;
            NE_KADAR = ne_kadar;
            NE_ZAMAN = DateTime.ParseExact(ne_zaman, "yyyyMMddHHmmss", TR);
            OTURUM = oturum;
        }
        public Beğeni(string kim, string neyi, int ne_kadar, DateTime ne_zaman, string oturum)
        {
            KİM = kim;
            NEYİ = neyi;
            NE_KADAR = ne_kadar;
            NE_ZAMAN = ne_zaman;
            OTURUM = oturum;
        }
    }

    public class Yorum
    {
        public string KİM {get; set;} //Yorum yapanın kullanıcı kimliği
        public string NEYE {get; set;} //Hakkında yorum yapılan şeyin kimliği
        public string İÇERİK {get; set;} //Yorum metni
        public DateTime NE_ZAMAN {get; set;} //Yorumun paylaşıldığı anın zaman damgası
        public string OTURUM {get; set;} //Yorum yapanın o andaki oturum kimliği
        public string KİMLİK {get; set;} //Yorumun kimliği

        public Yorum() {}
        public Yorum(string kim, string neye, string içerik, string ne_zaman, string oturum, string kimlik)
        {
            CultureInfo TR = new CultureInfo("tr-TR");
            KİM = kim;
            NEYE = neye;
            İÇERİK = içerik;
            NE_ZAMAN = DateTime.ParseExact(ne_zaman, "yyyyMMddHHmmss", TR);
            OTURUM = oturum;
            KİMLİK = kimlik;
        }
        public Yorum(string kim, string neye, string içerik, DateTime ne_zaman, string oturum, string kimlik)
        {
            KİM = kim;
            NEYE = neye;
            İÇERİK = içerik;
            NE_ZAMAN = ne_zaman;
            OTURUM = oturum;
            KİMLİK = kimlik;
        }
    }
}