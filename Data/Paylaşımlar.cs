using System;
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
        public string OTURUM {get; set;} // Paylaşımın yapıldığı oturumun kimliği
        public DateTime TARİH {get; set;} // Paylaşımın yapıldığı tarih

        public Paylaşım (long kimlik1, string kimlik2, string başlık, string içerik, string eklenti,
                        string paylaşan, string oturum, DateTime tarih)
        {
                KİMLİK_1 = kimlik1;
                KİMLİK_2 = kimlik2;
                BAŞLIK = başlık;
                İÇERİK = içerik;
                EKLENTİ = eklenti;
                PAYLAŞAN = paylaşan;
                OTURUM = oturum;
                TARİH = tarih;
        }
    }
    public struct paylaşım
    {
        public long KİMLİK_1;
        public string KİMLİK_2, BAŞLIK, İÇERİK, EKLENTİ, PAYLAŞAN, OTURUM;
        public DateTime TARİH;

        public paylaşım (long kimlik1, string kimlik2, string başlık, string içerik, string eklenti,
                        string paylaşan, string oturum, DateTime tarih)
        {
                KİMLİK_1 = kimlik1;
                KİMLİK_2 = kimlik2;
                BAŞLIK = başlık;
                İÇERİK = içerik;
                EKLENTİ = eklenti;
                PAYLAŞAN = paylaşan;
                OTURUM = oturum;
                TARİH = tarih;
        }
    }
}