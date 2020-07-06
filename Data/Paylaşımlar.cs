using System;
using Kilnevüg;

namespace Esas
{
    public class yazıPaylaş
    {
        public string BAŞLIK;
        public string İÇERİK;
        public string PAYLAŞAN;
        public DateTime TARİH;

        public yazıPaylaş() { }

        public yazıPaylaş(string başlık, string içerik, string paylaşan, DateTime tarih)
        {
            BAŞLIK = başlık;
            İÇERİK = içerik;
            PAYLAŞAN = paylaşan;
            TARİH = tarih;
        }

        public yazıPaylaş(yazpay pay)
        {
            BAŞLIK = pay.BAŞLIK;
            İÇERİK = pay.İÇERİK;
            PAYLAŞAN = pay.PAYLAŞAN;
            TARİH = pay.TARİH;
        }

        public yazpay ToYazpay() {
            yazpay a;
            a.BAŞLIK = BAŞLIK;
            a.İÇERİK = İÇERİK;
            a.PAYLAŞAN = PAYLAŞAN;
            a.TARİH = TARİH;

            return a;
        }
    }

    public struct yazpay
    {
        public string BAŞLIK;
        public string İÇERİK;
        public string PAYLAŞAN;
        public DateTime TARİH;
    }
}