using System;
using Esas;
using Esas.VeriTabanı;
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

            /*
                Bura indirilebilir belgeyi kullanıcıya gönderme kısmı olacak.
            */
            return "İşlem tamamlanamadı.";
        }
    }
}