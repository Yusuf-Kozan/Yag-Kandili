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
using Kilnevüg;

namespace Esas
{
    public class Paylaşım
    {
        public long KİMLİK_1 {get; set;} // Veri tabanı tarafından otomatik artırılan sayılı kimlik
        public string KİMLİK_2 {get; set;} // Yağ Kandili'nin oluşturmaya çalışacağı eşsizimsi kimlik
        public string BAŞLIK {get; set;} // Paylaşımın başlığı
        public string İÇERİK {get; set;} // Paylaşımın içerik kısmı (Asıl olay burada dönecek.)
        public string EKLENTİ {get; set;} // Paylaşıma yerleştirilecek köprü, resim, video vb. şeyler
        public string PAYLAŞAN {get; set;} // Paylaşımı yapan kişinin kullanıcı kimliği
        public DateTime TARİH {get; set;} // Paylaşımın yapıldığı tarih
        public string LİSANS {get; set;} // Paylaşımın kullanım koşulları

        public Paylaşım()
        {}
        public Paylaşım (long kimlik1, string kimlik2, string başlık, string içerik, string eklenti,
                        string paylaşan, DateTime tarih)
        {
                KİMLİK_1 = kimlik1;
                KİMLİK_2 = kimlik2;
                BAŞLIK = başlık;
                İÇERİK = içerik;
                EKLENTİ = eklenti;
                PAYLAŞAN = paylaşan;
                TARİH = tarih;
        }
        public Paylaşım (string[] paylaşım_bilgileri)
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