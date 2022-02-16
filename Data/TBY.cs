using System;
using System.Globalization;

namespace Esas
{
    public class Beğeni
    {
        public string KİM {get; set;} //Beğenenin kullanıcı kimliği
        public string NEYİ {get; set;} //Beğenilen paylaşımın Kimlik2'si
        public int NE_KADAR {get; set;}
        public DateTime NE_ZAMAN {get; set;} //Beğenme düğmesine basılan anın zaman damgası

        public Beğeni() {}
        public Beğeni(string kim, string neyi, int ne_kadar, string ne_zaman)
        {
            CultureInfo TR = new CultureInfo("tr-TR");
            KİM = kim;
            NEYİ = neyi;
            NE_KADAR = ne_kadar;
            NE_ZAMAN = DateTime.ParseExact(ne_zaman, "yyyyMMddHHmmss", TR);
        }
        public Beğeni(string kim, string neyi, int ne_kadar, DateTime ne_zaman)
        {
            KİM = kim;
            NEYİ = neyi;
            NE_KADAR = ne_kadar;
            NE_ZAMAN = ne_zaman;
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