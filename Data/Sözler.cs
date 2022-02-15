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