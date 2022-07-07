/*
Copyright (C) 2022 Yusuf Kozan

---

Bu belge Yağ Kandili'nin bir parçasıdır.

Yağ Kandili bir özgür yazılımdır: GNU Affero Genel Kamu Lisansı'nın 3.
ya da (isteğinize göre) sonraki bir sürümünün Free Software Foundation
tarafından yayınlandığı durumunun koşulları altında Yağ Kandili'ni
dağıtabilir veya Yağ Kandili üzerinde değişiklik yapabilirsiniz.

Yağ Kandili kullanışlı olması umuduyla dağıtılmaktadır ancak HİÇBİR
GARANTİ VERMEMEKTEDİR, zımni PAZARLANABİLİRLİK veya BELİRLİ BİR
AMACA UYGUNLUK garantisi bile. Daha çok ayrıntı için GNU Affero
Genel Kamu Lisansı'na bakın.

Yağ Kandili'nin yanında GNU Affero Genel Kamu Lisansı'nın bir kopyasını
almış olmalısınız. Almadıysanız, <https://www.gnu.org/licenses/>
adresine bakın.

Yağ Kandili'nin lisansıyla ilgili daha çok bilgi için /Lisans
dizinine bakın.

---

This file is part of Yağ Kandili.

Yağ Kandili is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Yağ Kandili is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Yağ Kandili. If not, see <https://www.gnu.org/licenses/>.

For more information about the license of Yağ Kandili, see
/Lisans directory.
*/
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
            Posta.GönderenSMTPBilgisi gönderen_smtp = new Posta.GönderenSMTPBilgisi();
            gönderen_smtp.AyarBelgesiniOku();
            Posta.GönderenIMAPBilgisi gönderen_imap = new Posta.GönderenIMAPBilgisi();
            gönderen_imap.AyarBelgesiniOku();
            ÜyeBil alıcı = Üyelik.ÜyeBilgileri(kullanıcı_kimliği);
            Posta.PostaGönder.TekKullanıcıyaGönder
                            (
                                gönderen_smtp,
                                gönderen_imap,
                                alıcı,
                                "İstediğiniz Belge | Yağ Kandili",
                                "İstediğiniz belgeye önümüzdeki 24 saat boyunca " +
                                $"https://yagkandili.com.tr/g/{geçici_bağlantı.BAĞLANTI_DEĞİŞKENİ}" +
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
                            $"Kullanıcı Kimliği: {kullanıcı_kimliği}\n" +
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
                            $"Kullanıcı Kimliği: {kullanıcı_kimliği}\n" +
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
                            $"Kullanıcı Kimliği: {kullanıcı_kimliği}\n" +
                            $"Paylaşım Kimliği: {kimlik2}";
            KişiselVeri.İşlemYaz(yapılan);
            return true;
        }
    }
}