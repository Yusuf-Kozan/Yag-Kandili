using System;

namespace Esas.GeçiciBağlantı
{
    internal class GeçiciBağlantı
    {
        internal string BAĞLANTI_DEĞİŞKENİ {get; set;}
        internal string HEDEF_KULLANICI {get; set;}
        internal DateTime BAŞLANGIÇ_TARİHİ {get; set;}
        internal DateTime BİTİŞ_TARİHİ {get; set;}
        internal string İÇERİK_TÜRÜ {get; set;}
        internal string SAĞLANACAK_BELGE {get; set;}

        internal GeçiciBağlantı() {}
    }
}