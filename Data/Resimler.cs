using System;
using System.IO;
using Kilnevüg;
using System.Drawing;

namespace Esas
{
    public class Resim
    {
        public static bool UzantıUygun(string dosya_yolu)
        {
            string jpeg = ".jpeg", jpg = ".jpg", png = ".png";
            if (dosya_yolu.EndsWith(jpeg) || dosya_yolu.EndsWith(jpg) || dosya_yolu.EndsWith(png))
                return true;
            return false;
            //Resim dosyasının bir JPEG veya PNG uzantısının olup olmadığı denetlenip,
            //varsa "true" yoksa "false" değeri döndürülüyor.
        }
        public static bool ResimGeçerli(string dosya_yolu)
        {
            try
            {
                using (var resim = Image.FromFile(dosya_yolu))
                {
                    if (resim.Height < 2 && resim.Width < 2)
                        return false;
                    return true;
                }
                //Belirtilen resim dosyasından görüntü oluşturmayı deniyor
                //2x2' den büyükse "true", değilse "false" değeri döndürülüyor
            }
            catch
            {
                return false;
                //Herhangi bir hata meydana gelirse, "false" değeri dödürülüyor.
            }
        }
        public static bool ResimUygun(string dosya_yolu)
        {
            if (UzantıUygun(dosya_yolu) && ResimGeçerli(dosya_yolu))
                return true;
            return false; 
        }
    }
}