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

namespace Esas.GeçiciBağlantı
{
    internal class GeçiciBağlantı
    {
        internal string BAĞLANTI_DEĞİŞKENİ {get; set;}
        internal string HEDEF_KULLANICI {get; set;}
        internal DateTime BAŞLANGIÇ_TARİHİ {get; set;}
        internal DateTime BİTİŞ_TARİHİ {get; set;}
        internal string İÇERİK_TÜRÜ {get; set;}
        internal string SAĞLANACAK_BELGE {get; set;}

        internal GeçiciBağlantı() {}

        internal GeçiciBağlantı(string hedef_kullanıcı, DateTime başlangıç,
                    DateTime bitiş, string içerik_türü, string belge)
        {
            BAĞLANTI_DEĞİŞKENİ = BağlantıDeğişkeni.Yeni();
            HEDEF_KULLANICI = hedef_kullanıcı;
            BAŞLANGIÇ_TARİHİ = başlangıç;
            BİTİŞ_TARİHİ = bitiş;
            İÇERİK_TÜRÜ = içerik_türü;
            SAĞLANACAK_BELGE = belge;
        }
        internal GeçiciBağlantı(string hedef_kullanıcı, DateTime başlangıç,
                    DateTime bitiş, string içerik_türü)
        {
            BAĞLANTI_DEĞİŞKENİ = BağlantıDeğişkeni.Yeni();
            HEDEF_KULLANICI = hedef_kullanıcı;
            BAŞLANGIÇ_TARİHİ = başlangıç;
            BİTİŞ_TARİHİ = bitiş;
            İÇERİK_TÜRÜ = içerik_türü;
            SAĞLANACAK_BELGE = null;
        }
    }
}