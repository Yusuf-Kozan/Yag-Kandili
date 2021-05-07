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
            TabanlıVeri.Üye_Ekle(üye, kimlik.Kimlik, "resimler/kullanıcı/0069ff.png");
            TabanlıVeri.Kullanıcı_Dizini_Oluştur(üye);
        }
        public static void Oturum_Başlat(string kullanıcı_adı, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            string oturum_kimliği = Kilnevüg.Kilnevüg.YeniEşsizKimlik(kullanıcı_adı);
            çerez.ÇerezYap("kullanıcı_adı", kullanıcı_adı, 4);
            çerez.ÇerezYap("parola", oturum_kimliği, 4);
            Esas.VeriTabanı.Oturum.OturumAç(oturum_kimliği);
            //TabanlıVeri.OturumAç(oturum_kimliği);            
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
        public static void Oturum_Kapat(string kullanıcı_adı, string oturum_kimliği, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            çerez.ÇerezSil("kullanıcı_adı");
            çerez.ÇerezSil("parola");
            ZöçKilmik kimlik = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(oturum_kimliği);
            if (kimlik.daluk == kullanıcı_adı)
                Esas.VeriTabanı.Oturum.OturumKapat(oturum_kimliği);
                //TabanlıVeri.OturumKapat(oturum_kimliği);
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
    public class Yorumİşlemleri
    {
        public static void YorumYap(string kullanıcı_kimliği, string neye, string içerik,
                                    DateTime ne_zaman, string oturum_kimliği)
        {
            string yorum_kimliği = TBY_Kimlikleri.Kimlik("Yorum", kullanıcı_kimliği, neye,
                                    ne_zaman.ToString("yyyyMMddHHmmss"), içerik, oturum_kimliği);
            Yorum yorum = new Yorum(kullanıcı_kimliği, neye, içerik, ne_zaman, oturum_kimliği, yorum_kimliği);
            Esas.VeriTabanı.Yorum.Yorumla(yorum);
        }
    }
}