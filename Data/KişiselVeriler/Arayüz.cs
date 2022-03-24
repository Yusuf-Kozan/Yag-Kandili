using System;
using Esas;
using Esas.VeriTabanı;
using Esas.GeçiciBağlantı;
using Kilnevüg;

namespace Esas.KişiselVeriler
{
    internal class İşlemler
    {
        internal static string KV_İste(string kullanıcı_kimliği, string parola)
        {
            string parola_bilgisi = Üyelik.KullanıcınınParolaBilgileri(kullanıcı_kimliği);
            bool parola_doğru = Parolalar.ParolaDoğru(parola_bilgisi, parola);
            if (!parola_doğru)
            {
                return "İşlem tamamlanamadı.";
            }

            string işlem_kimliği = KV_İşlemKimliği.YeniKimlik();
            İşlemKaydı işlem_kaydı = new İşlemKaydı();
            işlem_kaydı.KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            işlem_kaydı.İŞLEM = "Kendi Kişisel Verilerini İsteme";
            işlem_kaydı.TARİH = DateTime.Now;
            işlem_kaydı.İŞLEM_KİMLİĞİ = işlem_kimliği;
            KişiselVeri.İşlemYaz(işlem_kaydı);

            string kv_konumu = BelgeyeYaz.VeriBelgeliğiOluştur(kullanıcı_kimliği);

            
            DateTime baş_tarih = DateTime.Now;
            DateTime son_tarih = baş_tarih.AddHours(24);
            GeçiciBağlantı.GeçiciBağlantı geçici_bağlantı = new GeçiciBağlantı.GeçiciBağlantı
                                            (
                                                kullanıcı_kimliği,
                                                baş_tarih,
                                                son_tarih,
                                                "kişisel veri",
                                                kv_konumu
                                            );
            VeriTabanı.GeçiciBağlantı.BağlantıEkle(geçici_bağlantı);

            // Geçici bağlantıyı içeren e-posta
            Posta.GönderenBilgisi gönderen = new Posta.GönderenBilgisi();
            gönderen.AyarBelgesiniOku();
            ÜyeBil alıcı = Üyelik.ÜyeBilgileri(kullanıcı_kimliği);
            Posta.PostaGönder.TekKullanıcıyaGönder
                            (
                                gönderen,
                                alıcı,
                                "İstediğiniz Belge | Yağ Kandili",
                                "İstediğiniz belgeye önümüzdeki 24 saat boyunca " +
                                $"https://yağkandili.com.tr/g/{geçici_bağlantı.BAĞLANTI_DEĞİŞKENİ}" +
                                " adresinden erişebilirsiniz."
                            );
            

            return "Erişim bağlantısı e-posta yoluyla gönderildi.";
        }
        
        internal static bool PaylaşımıGizle(string kullanıcı_kimliği, string kimlik2)
        {
            //İşlem başarıyla yapılırsa TRUE, yapılamazsa FALSE değerini döndürür.
            string paylaşan = VeriTabanı.Paylaşım.PaylaşanınKimliği(kimlik2);
            if (kullanıcı_kimliği != paylaşan)
            {
                return false;
            }
            else
            {
                VeriTabanı.Paylaşım.PaylaşımıGizle(kimlik2);
                return true;
            }
        }
    }
}