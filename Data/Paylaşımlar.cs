/*
Copyright (C) 2022,2025 Yusuf Kozan

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
using Kilnevüg;

namespace Esas
{
    /**
        <summary>
            Alışılagelmiş paylaşım nesnesi.
        </summary>
        <remarks>
            Kimlik1'i var. Paylaşanı string türünde.
        </remarks>
    */
    internal class Paylaşım : PaylaşımÖzü
    {
        /**
            <summary>
                Veri tabanı tarafından otomatik artırılan sayılı kimlik
            </summary>
        */
        private protected long kimlik1;
        /**
            <summary>Paylaşan kişinin kullanıcı kimliği</summary>
        */
        private protected string paylaşan;
        
        /**
            <summary>Boş bir paylaşım nesnesi oluşturur.</summary>
        */
        internal Paylaşım()
        {}
        /**
            <summary>Bir string dizisinden paylaşım nesnesi oluşturur.</summary>

            <param name="paylaşım_bilgileri">
                Paylaşımın bilgilerinin sınıftaki tanım sırasıyla yer aldığı
                bir string dizisi
            </param>
        */
        internal Paylaşım (string[] paylaşım_bilgileri)
        {
            KİMLİK_1 = long.Parse(paylaşım_bilgileri[0]);
            KİMLİK_2 = paylaşım_bilgileri[1];
            BAŞLIK = paylaşım_bilgileri[2];
            İÇERİK = paylaşım_bilgileri[3];
            EKLENTİ = paylaşım_bilgileri[4];
            PAYLAŞAN = paylaşım_bilgileri[5];
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(paylaşım_bilgileri[6], "yyyyMMddHHmmss", TR);
            LİSANS = paylaşım_bilgileri[7];
        }

        /**
            <summary>Otomatik artırılan sayılı kimliğe erişim sağlar.</summary>
        */
        internal long KİMLİK_1 {
            get
            {
                return kimlik1;
            }
            set
            {
                kimlik1 = value;
            }
        }
        /**
            <summary>Paylaşan kişinin kullanıcı kimliğine erişim sağlar.</summary>
        */
        internal string PAYLAŞAN {
            get
            {
                return paylaşan;
            }
            set
            {
                paylaşan = value;
            }
        }
    }

    /**
        <summary>
            Paylaşanı ÜyeBil türünde olan
            alışılagelmiş paylaşım nesnesi
        </summary>
        <remarks>
            Kimlik1'i var. Paylaşanı ÜyeBil türünde.
        </remarks>
    */
    internal class ÜyeliPaylaşım : PaylaşımÖzü
    {
        /**
            <summary>
                Veri tabanı tarafından otomatik artırılan sayılı kimlik
            </summary>
        */
        private protected long kimlik1;
        /**
            <summary>Paylaşan kişinin üye nesnesi</summary>
        */
        private protected ÜyeBil paylaşan;
        
        /**
            <summary>Boş bir paylaşım nesnesi oluşturur.</summary>
        */
        internal ÜyeliPaylaşım()
        {}

        /**
            <summary>Otomatik artırılan sayılı kimliğe erişim sağlar.</summary>
        */
        internal long KİMLİK_1 {
            get
            {
                return kimlik1;
            }
            set
            {
                kimlik1 = value;
            }
        }
        /**
            <summary>Paylaşan kişinin kullanıcı kimliğine erişim sağlar.</summary>
        */
        internal ÜyeBil PAYLAŞAN {
            get
            {
                return paylaşan;
            }
            set
            {
                paylaşan = value;
            }
        }
    }


    /**
        <summary>
            Paylaşanı bile olmayan bir öz paylaşım.
        </summary>
        <remarks>
            Kimlik1, Paylaşan, değer vb. özellikleri farklı
            farklı yollarla uyarlarken her şeyi tekrar
            tanımlamanın önüne geçmek için.
        </remarks>
    */
    internal abstract class PaylaşımÖzü
    {
        
        /**
            <summary>Yağ Kandili'nin atadığı eşsiz kimlik</summary>
        */
        private protected string kimlik2;
        /**
            <summary>Paylaşımın başlık bölümü</summary>
        */
        private protected string başlık;
        /**
            <summary>Paylaşımın metin içeriği</summary>
        */
        private protected string içerik;
        /**
            <summary>Paylaşımla ilgili ek bilgiler</summary>
        */
        private protected string eklenti;
        /**
            <summary>Paylaşımın yapıldığı tarih</summary>
        */
        private protected DateTime tarih;
        /**
            <summary>Paylaşımın kullanım koşulları</summary>
        */
        private protected string lisans;


        /**
            <summary>Yağ Kandili'nin atadığı kimliğe erişim sağlar.</summary>
        */
        internal string KİMLİK_2 {
            get
            {
                return kimlik2;
            }
            set
            {
                kimlik2 = value;
            }
        }
        /**
            <summary>Paylaşımın başlığına erişim sağlar.</summary>
        */
        internal string BAŞLIK {
            get
            {
                return başlık;
            }
            set
            {
                başlık = value;
            }
        }
        /**
            <summary>Paylaşımın metin içeriğine erişim sağlar.</summary>
        */
        internal string İÇERİK {
            get
            {
                return içerik;
            }
            set
            {
                içerik = value;
            }
        }
        /**
            <summary>Paylaşımla ilgili ek bilgilere erişim sağlar.</summary>
        */
        internal string EKLENTİ {
            get
            {
                return eklenti;
            }
            set
            {
                eklenti = value;
            }
        }

        /**
            <summary>Paylaşımın yapıldığı tarihe erişim sağlar.</summary>
        */
        internal DateTime TARİH {
            get
            {
                return tarih;
            }
            set
            {
                tarih = value;
            }
        }
        /**
            <summary>Paylaşımın kullanım koşullarına erişim sağlar.</summary>
        */
        internal string LİSANS {
            get
            {
                return lisans;
            }
            set
            {
                lisans = value;
            }
        }
    }
    
    public struct paylaşım
    {
        public long KİMLİK_1;
        public string KİMLİK_2, BAŞLIK, İÇERİK, EKLENTİ, PAYLAŞAN, LİSANS;
        public DateTime TARİH;

        public paylaşım (long kimlik1, string kimlik2, string başlık, string içerik, string eklenti,
                        string paylaşan, DateTime tarih, string lisans)
        {
                KİMLİK_1 = kimlik1;
                KİMLİK_2 = kimlik2;
                BAŞLIK = başlık;
                İÇERİK = içerik;
                EKLENTİ = eklenti;
                PAYLAŞAN = paylaşan;
                TARİH = tarih;
                LİSANS = lisans;
        }
        public paylaşım (string[] paylaşım_bilgileri)
        {
            KİMLİK_1 = long.Parse(paylaşım_bilgileri[0]);
            KİMLİK_2 = paylaşım_bilgileri[1];
            BAŞLIK = paylaşım_bilgileri[2];
            İÇERİK = paylaşım_bilgileri[3];
            EKLENTİ = paylaşım_bilgileri[4];
            PAYLAŞAN = paylaşım_bilgileri[5];
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(paylaşım_bilgileri[6], "yyyyMMddHHmmss", TR);
            LİSANS = paylaşım_bilgileri[7];
        }
    }
    public struct yeni_paylaşım
    {
        public string KİMLİK_2, BAŞLIK, İÇERİK, EKLENTİ, PAYLAŞAN, LİSANS;
        public DateTime TARİH;

        public yeni_paylaşım (string kimlik2, string başlık, string içerik, string eklenti,
                        string paylaşan, DateTime tarih, string lisans)
        {
                KİMLİK_2 = kimlik2;
                BAŞLIK = başlık;
                İÇERİK = içerik;
                EKLENTİ = eklenti;
                PAYLAŞAN = paylaşan;
                TARİH = tarih;
                LİSANS = lisans;
        }
    }
    public struct değerli_paylaşım
    {
        public long KİMLİK_1;
        public string KİMLİK_2, BAŞLIK, İÇERİK, EKLENTİ, PAYLAŞAN, LİSANS;
        public DateTime PAYLAŞIM_TARİHİ, DEĞERLENDİRME_TARİHİ;
        public string DEĞERLENDİREN;
        public int DEĞER;

        public değerli_paylaşım(string[][] paylaşım_bilgileri)
        {
            CultureInfo TR = new CultureInfo("tr-TR");

            KİMLİK_1 = long.Parse(paylaşım_bilgileri[0][0]);
            KİMLİK_2 = paylaşım_bilgileri[0][1];
            BAŞLIK = paylaşım_bilgileri[0][2];
            İÇERİK = paylaşım_bilgileri[0][3];
            EKLENTİ = paylaşım_bilgileri[0][4];
            PAYLAŞAN = paylaşım_bilgileri[0][5];
            PAYLAŞIM_TARİHİ = DateTime.ParseExact(
                paylaşım_bilgileri[0][6],
                "yyyyMMddHHmmss",
                TR );
            LİSANS = paylaşım_bilgileri[0][7];

            DEĞERLENDİREN = paylaşım_bilgileri[1][0];
            DEĞER = int.Parse(paylaşım_bilgileri[1][2]);
            DEĞERLENDİRME_TARİHİ = DateTime.ParseExact(
                paylaşım_bilgileri[1][3],
                "yyyyMMddHHmmss",
                TR );
        }
    }
}