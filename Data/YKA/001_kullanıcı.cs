using System;
using System.IO;
using Esas;
using Kilnevüg;

namespace YK_Arşiv
{
    public class kullanıcı
    {
        public static void Oluştur(çÜye üye, string kimlik)
        {
            string[] belge = new string[] {
                "YKA+0+001",
                "",
                kimlik,
                "Kullanıcı_Adı:"+üye.KULLANICI_ADI,
                "Ad:"+üye.AD,
                "Soyadı:"+üye.SOYADI,
                "E_Posta:"+üye.E_POSTA,
                "Başlangıç:"+üye.BAŞLANGIÇ.ToString(),
                "Resim:resimler/kulllanıcı/0069ff.png"
            };
            string dosyaAdı = üye.KULLANICI_ADI + ".yka";
            string esasYol = System.IO.Path.Combine(üye.DizinYolu(), dosyaAdı);
            System.IO.File.WriteAllLines(esasYol, belge);
        }
    }
}