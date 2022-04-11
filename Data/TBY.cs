/*
Copyright (C) 2022 Yusuf Kozan

---

Bu belge Yağ Kandili'nin bir parçasıdır.

Yağ Kandili bir özgür yazılımdır: GNU Affero Genel Kamu Lisansı'nın 3.
ya da (isteğinize göre) sonraki bir sürümünün Free Software Foundation
tarafından yayınlandığı durumunun koşulları altında Yağ Kandili'ni
dağıtabilir veya Yağ Kandili üzerinde değişiklik yapabilirsiniz.

Yağ Kandili kullanışlı olması umuduyla dağıtılmaktadır ancak HİÇBİR
GARANTİ VERMEMEKTEDİR, zımni PAZARLANABİLİRLİK veya BELİRLİ BİR
AMACA UYGUNLUK garantisi bile. Daha çok ayrıntı için GNU Affero
Genel Kamu Lisansı'na bakın.

Yağ Kandili'nin yanında GNU Affero Genel Kamu Lisansı'nın bir kopyasını
almış olmalısınız. Almadıysanız, <https://www.gnu.org/licenses/>
adresine bakın.

Yağ Kandili'nin lisansıyla ilgili daha çok bilgi için /Lisans
dizinine bakın.

---

This file is part of Yağ Kandili.

Yağ Kandili is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Yağ Kandili is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Yağ Kandili. If not, see <https://www.gnu.org/licenses/>.

For more information about the license of Yağ Kandili, see
/Lisans directory.
*/
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