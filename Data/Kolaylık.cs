using System;
using Esas;
using Kilnevüg;
using YK_Arşiv;
using Microsoft.JSInterop;

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
        public static void Oturum_Başlat(string kullanıcı_adı, IJSRuntime jsRuntime)
        {
            string kilmik = Kilnevüg.Kilnevüg.YeniEşsizKimlik(kullanıcı_adı);
            TabanlıVeri.OturumAç(kilmik);
            Çerezler çerez = new Çerezler(jsRuntime);
            çerez.ÇerezYap("kullanıcı_adı", kullanıcı_adı, 12);
            çerez.ÇerezYap("parola", kilmik, 12);
        }
    }
}