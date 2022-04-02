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
            işlem_kaydı.İŞLEM = "Kendi Kişisel Verilerini İndirme İsteği";
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
            
            İşlemKaydı yapılan = new İşlemKaydı();
            yapılan.KULLANICI_KİMLİĞİ = "Yağ Kandili";
            yapılan.TARİH = DateTime.Now;
            yapılan.İŞLEM_KİMLİĞİ = işlem_kimliği;
            yapılan.İŞLEM = "Kullanıcının isteği üzerine, kendi kişisel verilerini " +
                            "indirebileceği bağlantı kendisine e-posta yoluyla iletildi.\n" +
                            $"Kullanıcı Kimliği: {kullanıcı_kimliği}\n" +
                            $"Geçici Bağlantı Değişkeni: {geçici_bağlantı.BAĞLANTI_DEĞİŞKENİ}";
            KişiselVeri.İşlemYaz(yapılan);

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

            string işlem_kimliği = KV_İşlemKimliği.YeniKimlik();
            İşlemKaydı istek = new İşlemKaydı();
            istek.KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            istek.TARİH = DateTime.Now;
            istek.İŞLEM_KİMLİĞİ = işlem_kimliği;
            istek.İŞLEM = "Kendi Yaptığı Paylaşımın Diğer " +
                        "Kullanıcılardan Gizlenmesi İsteği\n" +
                        $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(istek);

            VeriTabanı.Paylaşım.PaylaşımıGizle(kimlik2);

            İşlemKaydı yapılan = new İşlemKaydı();
            yapılan.KULLANICI_KİMLİĞİ = "Yağ Kandili";
            yapılan.TARİH = DateTime.Now;
            yapılan.İŞLEM_KİMLİĞİ = işlem_kimliği;
            yapılan.İŞLEM = "Paylaşanın isteği üzerine ilgili paylaşım " +
                            "diğer kullanıcılardan gizlendi.\n" +
                            $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(yapılan);
            return true;
        }
        internal static bool GizliPaylaşımıAç(string kullanıcı_kimliği, string kimlik2)
        {
            //İşlem başarıyla yapılırsa TRUE, yapılamazsa FALSE değerini döndürür.
            string paylaşan = VeriTabanı.Paylaşım.PaylaşanınKimliği(kimlik2);
            if (kullanıcı_kimliği != paylaşan)
            {
                return false;
            }
            
            string işlem_kimliği = KV_İşlemKimliği.YeniKimlik();
            İşlemKaydı istek = new İşlemKaydı();
            istek.KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            istek.TARİH = DateTime.Now;
            istek.İŞLEM_KİMLİĞİ = işlem_kimliği;
            istek.İŞLEM = "Kendi Gizli Paylaşımının Diğer Kullanıcılar İçin " +
                        "Yeniden Erişilebilir Olması İsteği\n" +
                        $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(istek);

            VeriTabanı.Paylaşım.GizliPaylaşımıAç(kimlik2);

            İşlemKaydı yapılan = new İşlemKaydı();
            yapılan.KULLANICI_KİMLİĞİ = "Yağ Kandili";
            yapılan.TARİH = DateTime.Now;
            yapılan.İŞLEM_KİMLİĞİ = işlem_kimliği;
            yapılan.İŞLEM = "Paylaşanın isteği üzerine ilgili gizli paylaşım " +
                            "diğer kullanıcılar için erişilebilir duruma getirildi.\n" +
                            $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(yapılan);
            return true;
        }

        internal static bool PaylaşımıSil(string kullanıcı_kimliği, string kimlik2)
        {
            //İşlem başarıyla yapılırsa TRUE, yapılamazsa FALSE değerini döndürür.
            string paylaşan = VeriTabanı.Paylaşım.PaylaşanınKimliği(kimlik2);
            if (kullanıcı_kimliği != paylaşan)
            {
                return false;
            }
            
            string işlem_kimliği = KV_İşlemKimliği.YeniKimlik();
            İşlemKaydı istek = new İşlemKaydı();
            istek.KULLANICI_KİMLİĞİ = kullanıcı_kimliği;
            istek.TARİH = DateTime.Now;
            istek.İŞLEM_KİMLİĞİ = işlem_kimliği;
            istek.İŞLEM = "Kendi Yaptığı Paylaşımın Silinmesi İsteği\n" +
                        $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(istek);

            VeriTabanı.Paylaşım.PaylaşımıSil(kullanıcı_kimliği, kimlik2);

            İşlemKaydı yapılan = new İşlemKaydı();
            yapılan.KULLANICI_KİMLİĞİ = "Yağ Kandili";
            yapılan.TARİH = DateTime.Now;
            yapılan.İŞLEM_KİMLİĞİ = işlem_kimliği;
            yapılan.İŞLEM = "Paylaşanın isteği üzerine ilgili paylaşım silindi.\n" +
                            $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(yapılan);
            return true;
        }
    }
}