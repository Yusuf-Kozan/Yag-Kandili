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

        internal GeçiciBağlantı(string hedef_kullanıcı, DateTime başlangıç,
                    DateTime bitiş, string içerik_türü, string belge)
        {
            BAĞLANTI_DEĞİŞKENİ = BağlantıDeğişkeni.Yeni();
            HEDEF_KULLANICI = hedef_kullanıcı;
            BAŞLANGIÇ_TARİHİ = başlangıç;
            BİTİŞ_TARİHİ = bitiş;
            İÇERİK_TÜRÜ = içerik_türü;
            SAĞLANACAK_BELGE = belge;
        }
        internal GeçiciBağlantı(string hedef_kullanıcı, DateTime başlangıç,
                    DateTime bitiş, string içerik_türü)
        {
            BAĞLANTI_DEĞİŞKENİ = BağlantıDeğişkeni.Yeni();
            HEDEF_KULLANICI = hedef_kullanıcı;
            BAŞLANGIÇ_TARİHİ = başlangıç;
            BİTİŞ_TARİHİ = bitiş;
            İÇERİK_TÜRÜ = içerik_türü;
            SAĞLANACAK_BELGE = null;
        }
    }
}