using System;
using Esas;
using Kilnevüg;
using YK_Arşiv;

namespace İşlemler
{
    public class Oturumİşlemleri
    {
        public static void Üye_Oluştur(çÜye üye)
        {
            Kullanıcı_Kimliği kimlik = new Kullanıcı_Kimliği(üye);
            TabanlıVeri.Üye_Ekle(üye, kimlik.Kimlik);
            TabanlıVeri.Kullanıcı_Dizini_Oluştur(üye);
            YK_Arşiv.kullanıcı.Oluştur(üye, kimlik.Kimlik);
        }
    }
}