using System;
using Esas;
using Kilnevüg;
using Microsoft.JSInterop;

namespace İşlemler
{
    public class Oturumİşlemleri
    {
        public static void Oturum_Başlat(string kullanıcı_adı, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            string oturum_kimliği = Kilnevüg.Kilnevüg.YeniEşsizKimlik(kullanıcı_adı);
            string kullanıcı_kimliği = Esas.VeriTabanı.Üyelik.KullanıcınınKimliği(kullanıcı_adı);
            çerez.ÇerezYap("kullanıcı", kullanıcı_kimliği, 4);
            çerez.ÇerezYap("oturum", oturum_kimliği, 4);
            Esas.VeriTabanı.Oturum.OturumAç(oturum_kimliği, kullanıcı_kimliği);
        }
        public static void Oturum_Kapat(string kullanıcı_kimliği, string oturum_kimliği, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            çerez.ÇerezSil("kullanıcı");
            çerez.ÇerezSil("oturum");
            if (Esas.VeriTabanı.Oturum.BuOturumAçık(oturum_kimliği, kullanıcı_kimliği))
                Esas.VeriTabanı.Oturum.OturumKapat(oturum_kimliği);
        }
    }
    public class Üyelikİşlemleri
    {
        public static void ÜyeOluştur(Üye üye)
        {
            string kullanıcı_kimliği = KullanıcıKimliği.Kimlik(üye);
            Esas.VeriTabanı.Üyelik.ÜyeEkle(üye, kullanıcı_kimliği);
        }
    }
    public class Paylaşımİşlemleri
    {
        public static void Paylaşım_Yap(string başlık, string içerik, string eklenti,
                                        string oturum_kimliği, DateTime tarih, string lisans)
        {
            string kullanıcı_kimliği = Esas.VeriTabanı.Oturum.Oturumunİyesi(oturum_kimliği);
            ÜyeBil paylaşan = Esas.VeriTabanı.Üyelik.ÜyeBilgileri(kullanıcı_kimliği);
            string kimlik2 = PaylaşımKimliği.Kimlik2(başlık, içerik, paylaşan.KİMLİK, oturum_kimliği, tarih);
            yeni_paylaşım yeni_paylaşım = new yeni_paylaşım(kimlik2, başlık, içerik, eklenti, paylaşan.KİMLİK,
                                                            oturum_kimliği, tarih, lisans);
            Esas.VeriTabanı.Paylaşım.PaylaşımYap(yeni_paylaşım);
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