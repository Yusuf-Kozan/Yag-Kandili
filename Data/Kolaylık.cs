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
using Kilnevüg;
using Microsoft.JSInterop;

namespace İşlemler
{
    public class Oturumİşlemleri
    {
        public static void Oturum_Başlat(string kullanıcı_adı, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            string oturum_kimliği = OturumKimliği.YeniKimlik();
            string kullanıcı_kimliği = Esas.VeriTabanı.Üyelik.KullanıcınınKimliği(kullanıcı_adı);
            çerez.ÇerezYap("KULLANICI", kullanıcı_kimliği, 4);
            çerez.ÇerezYap("OTURUM", oturum_kimliği, 4);
            Esas.VeriTabanı.Oturum.OturumAç(oturum_kimliği, kullanıcı_kimliği);
        }
        public static void Oturum_Kapat(string kullanıcı_kimliği, string oturum_kimliği, IJSRuntime jsRuntime)
        {
            Çerezler çerez = new Çerezler(jsRuntime);
            çerez.ÇerezSil("KULLANICI");
            çerez.ÇerezSil("OTURUM");
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
                                                            tarih, lisans);
            Esas.VeriTabanı.Paylaşım.PaylaşımYap(yeni_paylaşım);
        }
    }
    public class Yorumİşlemleri
    {
        public static void YorumYap(string kullanıcı_kimliği, string paylaşım_kimliği, string içerik,
                                    DateTime ne_zaman, string oturum_kimliği)
        {
            string yorum_kimliği = TY_Kimlikleri.Kimlik("Yorum", kullanıcı_kimliği, paylaşım_kimliği,
                                    ne_zaman.ToString("yyyyMMddHHmmss"), içerik, oturum_kimliği);
            yeni_söz yorum = new yeni_söz(içerik, kullanıcı_kimliği, yorum_kimliği,
                                            ne_zaman, true, paylaşım_kimliği);
            Esas.VeriTabanı.Söyleşi.Söyle(yorum);
        }
    }
    public class Takipİşlemleri
    {
        public static void TakipEt(string takip_edecek, string takip_edilecek,
                                    short takip_düzeyi, DateTime tarih)
        {
            Takip yeni_takip = new Takip(takip_edecek,takip_edilecek,
                                        takip_düzeyi, tarih);
            Esas.VeriTabanı.Takip.TakipEt(yeni_takip);
        }
    }
}