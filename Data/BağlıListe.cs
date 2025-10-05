/*
Copyright (C) 2025 Yusuf Kozan

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
namespace Esas
{
    /**
        <summary>Genel amaçlı tek yönlü bağlı liste</summary>
    */
    public class BağlıListe<T>
    {
        /**
            <summary>Düğümde tutulan veri</summary>
        */
        private T veri;
        /**
            <summary>Düğümde tutulan veriye erişim sağlar.</summary>
        */
        public T Veri {
            get
            {
                return this.veri;
            }
            set
            {
                this.veri = value;
            }
            
        }

        /**
            <summary>Listedeki bir sonraki düğüm</summary>
        */
        private BağlıListe<T> sonraki;
        /**
            <summary>Listedeki bir sonraki düğüme erişim sağlar.</summary>
        */
        public BağlıListe<T> Sonraki {
            get
            {
                return this.sonraki;
            }
            set
            {
                this.sonraki = value;
            }
        }

        /**
            <summary>Bağlı listedeki düğüm sayısı.</summary>
            <remarks>
                Tek yönlü olduğu için çağrılan düğüm kök kabul edilir.
            </remarks>
        */
        public int Length {
            get
            {
                BağlıListe<T> kafa = this;
                int nicelik = 1;
                while (kafa.Sonraki != null)
                {
                    kafa = kafa.Sonraki;
                    nicelik++;
                }
                return nicelik;
            }
        }

        /**
            <summary>Bağlı listenin son düğümü.</summary>
        */
        public BağlıListe<T> UçDüğüm {
            get
            {
                BağlıListe<T> kafa = this;
                while (kafa.Sonraki != null)
                {
                    kafa = kafa.Sonraki;
                }
                return kafa;
            }
        }

        /**
            <summary>Yeni bağlı liste düğümü yaratır.</summary>
            <remarks>Yeni düğüm hiçbir yere bağlı değildir.</remarks>

            <param name="veri">Yeni düğümde tutulacak veri</param>
        */
        public BağlıListe(T veri)
        {
            this.Veri = veri;
            this.Sonraki = null;
        }

        /**
            <summary>Listenin sonuna yeni veri ekler.</summary>

            <param name="kök">Ekleme yapılan liste</param>
            <param name="yeni_veri">Listenin sonuna eklenecek veri</param>
        */
        public static void DüğümEkle(BağlıListe<T> kök, T yeni_veri)
        {
            BağlıListe<T> kafa = kök.UçDüğüm;
            kafa.Sonraki = new BağlıListe<T>(yeni_veri);
        }

        /**
            <summary>Listenin sonuna başka liste ekler.</summary>

            <param name="kök">Ekleme yapılan liste</param>
            <param name="yeni_düğüm">Listenin sonuna eklenecek liste</param>
        */
        public static void DüğümEkle(BağlıListe<T> kök, BağlıListe<T> yeni_düğüm)
        {
            BağlıListe<T> kafa = kök.UçDüğüm;
            kafa.Sonraki = yeni_düğüm;
        }
    }
}