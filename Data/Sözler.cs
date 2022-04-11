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
    public struct söz
    {
        public string SÖZ;
        public string SÖYLEYEN;
        public string SÖYLEŞİ;
        public string TARİH;
        public string BAŞLATAN_PAYLAŞIM;
        public bool BU_İLK;
        public long GENEL_SIRA;

        public söz (string söz, string söyleyen,
                    string söyleşi, string tarih, bool bu_ilk,
                    string başlatan_paylaşım, long genel_sıra)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen;
            SÖYLEŞİ = söyleşi; TARİH = tarih; BU_İLK = bu_ilk;
            BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
            GENEL_SIRA = genel_sıra;
        }
        public söz (string söz, string söyleyen,
                    string söyleşi, DateTime tarih, bool bu_ilk,
                    string başlatan_paylaşım, long genel_sıra)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen;
            SÖYLEŞİ = söyleşi; TARİH = tarih.ToString("yyyyMMddHHmmss");
            BU_İLK = bu_ilk; BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
            GENEL_SIRA = genel_sıra;
        }
        public söz (string[] söz_bilgileri)
        {
            SÖZ = söz_bilgileri[0]; SÖYLEYEN = söz_bilgileri[1];
            TARİH = söz_bilgileri[2];
            SÖYLEŞİ = söz_bilgileri[3]; BAŞLATAN_PAYLAŞIM = söz_bilgileri[4];
            GENEL_SIRA = long.Parse(söz_bilgileri[5]);
            BU_İLK = bool.Parse(söz_bilgileri[6]);
        }

        public DateTime DönüştürülmüşTarih()
        {
            CultureInfo TR = new CultureInfo("tr-TR");
            return DateTime.ParseExact(TARİH, "yyyyMMddHHmmss", TR);
        }
    }
    public struct yeni_söz
    {
        public string SÖZ;
        public string SÖYLEYEN;
        public string SÖYLEŞİ;
        public string TARİH;
        public string BAŞLATAN_PAYLAŞIM;
        public bool BU_İLK;

        public yeni_söz(string söz, string söyleyen,
                        string söyleşi, string tarih, bool bu_ilk,
                        string başlatan_paylaşım)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen;
            SÖYLEŞİ = söyleşi; TARİH = tarih; BU_İLK = bu_ilk;
            BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
        }
        public yeni_söz(string söz, string söyleyen,
                        string söyleşi, DateTime tarih, bool bu_ilk,
                        string başlatan_paylaşım)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen;
            SÖYLEŞİ = söyleşi; TARİH = tarih.ToString("yyyyMMddHHmmss");
            BU_İLK = bu_ilk; BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
        }
        public yeni_söz (string[] söz_bilgileri)
        {
            SÖZ = söz_bilgileri[0]; SÖYLEYEN = söz_bilgileri[1];
            TARİH = söz_bilgileri[2];
            SÖYLEŞİ = söz_bilgileri[3]; BAŞLATAN_PAYLAŞIM = söz_bilgileri[4];
            BU_İLK = bool.Parse(söz_bilgileri[5]);
        }

        public DateTime DönüştürülmüşTarih()
        {
            CultureInfo TR = new CultureInfo("tr-TR");
            return DateTime.ParseExact(TARİH, "yyyyMMddHHmmss", TR);
        }
    }

    public struct köklü_söz
    {
        // Başlatan paylaşımı ve ilk sözü de içerdiği için "köklü" ön adı kullanıldı.
        // Diğer söz yapılarında bu kısımlar yer gösterici kimlikleri içerir.

        public paylaşım BAŞLATAN_PAYLAŞIM;
        public söz İLK_SÖZ;
        public bool BU_İLK;
        public string SÖZ, SÖYLEYEN, SÖYLEŞİ;
        public long GENEL_SIRA;
        public DateTime TARİH;
    }
}