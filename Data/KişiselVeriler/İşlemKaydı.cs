using System;
using System.Globalization;

namespace Esas.KişiselVeriler
{
    internal class İşlemKaydı
    {
        internal string KULLANICI_KİMLİĞİ {get; set;}
        internal string İŞLEM {get; set;}
        internal DateTime TARİH {get; set;}
        internal string İŞLEM_KİMLİĞİ {get; set;}
        
        internal İşlemKaydı() {}
        internal İşlemKaydı(string kullanıcı_kimliği, string işlem,
                            DateTime tarih, string işlem_kimliği)
        {
            KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            İŞLEM = işlem;
            TARİH = tarih;
            İŞLEM_KİMLİĞİ = işlem_kimliği;
        }
        internal İşlemKaydı(string kullanıcı_kimliği, string işlem,
                            string tarih, string işlem_kimliği)
        {
            KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            İŞLEM = işlem;
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
            İŞLEM_KİMLİĞİ = işlem_kimliği;
        }
    }
}