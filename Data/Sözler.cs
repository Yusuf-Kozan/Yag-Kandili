using System;

namespace Esas
{
    public struct söz
    {
        public string SÖZ, SÖYLEYEN, OTURUM, SÖYLEŞİ,
            TARİH, BAŞLATAN_PAYLAŞIM;
        public bool BU_İLK;
        public long GENEL_SIRA;

        public söz (string söz, string söyleyen, string oturum,
                    string söyleşi, string tarih, bool bu_ilk,
                    string başlatan_paylaşım, long genel_sıra)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen; OTURUM = oturum;
            SÖYLEŞİ = söyleşi; TARİH = tarih; BU_İLK = bu_ilk;
            BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
            GENEL_SIRA = genel_sıra;
        }
        public söz (string söz, string söyleyen, string oturum,
                    string söyleşi, DateTime tarih, bool bu_ilk,
                    string başlatan_paylaşım, long genel_sıra)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen; OTURUM = oturum;
            SÖYLEŞİ = söyleşi; TARİH = tarih.ToString("yyyyMMddHHmmss");
            BU_İLK = bu_ilk; BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
            GENEL_SIRA = genel_sıra;
        }
    }
    public struct yeni_söz
    {
        public string SÖZ, SÖYLEYEN, OTURUM, SÖYLEŞİ,
            TARİH, BAŞLATAN_PAYLAŞIM;
        public bool BU_İLK;

        public yeni_söz(string söz, string söyleyen, string oturum,
                        string söyleşi, string tarih, bool bu_ilk,
                        string başlatan_paylaşım)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen; OTURUM = oturum;
            SÖYLEŞİ = söyleşi; TARİH = tarih; BU_İLK = bu_ilk;
            BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
        }
        public yeni_söz(string söz, string söyleyen, string oturum,
                        string söyleşi, DateTime tarih, bool bu_ilk,
                        string başlatan_paylaşım)
        {
            SÖZ = söz; SÖYLEYEN = söyleyen; OTURUM = oturum;
            SÖYLEŞİ = söyleşi; TARİH = tarih.ToString("yyyyMMddHHmmss");
            BU_İLK = bu_ilk; BAŞLATAN_PAYLAŞIM = başlatan_paylaşım;
        }
    }
}