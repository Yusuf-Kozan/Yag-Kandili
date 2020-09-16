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
            Çerezler çerez = new Çerezler(jsRuntime);
            string kilmik = Kilnevüg.Kilnevüg.YeniEşsizKimlik(kullanıcı_adı);
            çerez.ÇerezYap("kullanıcı_adı", kullanıcı_adı, 4);
            çerez.ÇerezYap("parola", kilmik, 4);
            TabanlıVeri.OturumAç(kilmik);            
        }
        public static bool Oturum_Uygun(string kullanıcı_adı, string kilmik)
        {
            if (!String.IsNullOrWhiteSpace(kullanıcı_adı) && !String.IsNullOrWhiteSpace(kilmik))
            {
                if (Girdiler.metinUygun(kullanıcı_adı))
                {
                    if (Girdiler.kilmikOlabilir(kilmik))
                    {
                        return TabanlıVeri.KK_Doğru(kullanıcı_adı, kilmik);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}