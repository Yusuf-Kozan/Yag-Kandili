using System;
using Kilnevüg;

namespace Esas
{
    public class yazıPaylaş
    {
        public string BAŞLIK;
        public string İÇERİK;
        public string PAYLAŞAN;
        public string KİLMİK;
        public DateTime TARİH;

        public yazıPaylaş() { }

        public yazıPaylaş(string başlık, string içerik, string paylaşan, string kilmik, DateTime tarih)
        {
            BAŞLIK = başlık;
            İÇERİK = içerik;
            PAYLAŞAN = paylaşan;
            KİLMİK = kilmik;
            TARİH = tarih;
        }

        public yazıPaylaş(yazpay pay)
        {
            BAŞLIK = pay.BAŞLIK;
            İÇERİK = pay.İÇERİK;
            PAYLAŞAN = pay.PAYLAŞAN;
            KİLMİK = pay.KİLMİK;
            TARİH = pay.TARİH;
        }

        public yazpay ToYazpay() {
            yazpay a;
            a.BAŞLIK = BAŞLIK;
            a.İÇERİK = İÇERİK;
            a.PAYLAŞAN = PAYLAŞAN;
            a.KİLMİK = KİLMİK;
            a.TARİH = TARİH;

            return a;
        }
    }

    public struct yazpay
    {
        public string BAŞLIK;
        public string İÇERİK;
        public string PAYLAŞAN;
        public string KİLMİK;
        public DateTime TARİH;
    }
}