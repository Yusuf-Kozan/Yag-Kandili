using System;
using Esas;
using Kilnevüg;
using Microsoft.JSInterop;

namespace İşlemler
{
    public class Oturumİşlemleri
    {
        public static void Üye_Oluştur(çÜye üye)
        {
            Kullanıcı_Kimliği kimlik = new Kullanıcı_Kimliği(üye);
            TabanlıVeri.Üye_Ekle(üye, kimlik.Kimlik, "resimler/kulllanıcı/0069ff.png");
            TabanlıVeri.Kullanıcı_Dizini_Oluştur(üye);
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
        public static void Oturum_Kapat(string kullanıcı_adı, string kilmik, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            çerez.ÇerezSil("kullanıcı_adı");
            çerez.ÇerezSil("parola");
            ZöçKilmik kimlik = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(kilmik);
            if (kimlik.daluk == kullanıcı_adı)
                TabanlıVeri.OturumKapat(kilmik);
        }
    }
    public class Paylaşımİşlemleri
    {
        public static void Paylaşım_Yap(string başlık, string içerik, string eklenti,
                                        string oturum_kimliği, DateTime tarih)
        {
            ÜyeBil paylaşan = TabanlıVeri.Kilmikten_ÜyeBil(oturum_kimliği);
            Paylaşım yeni_paylaşım = new Paylaşım();
            yeni_paylaşım.KİMLİK_2 = Paylaşım_Kimliği.Kimlik_2(başlık, içerik, paylaşan.KİMLİK, oturum_kimliği, tarih);
            yeni_paylaşım.BAŞLIK = başlık; yeni_paylaşım.İÇERİK = içerik;
            yeni_paylaşım.EKLENTİ = eklenti; yeni_paylaşım.PAYLAŞAN = paylaşan.KİMLİK;
            yeni_paylaşım.OTURUM = oturum_kimliği; yeni_paylaşım.TARİH = tarih;
            TabanlıVeri.Paylaş(yeni_paylaşım);
        }
    }
}