using System;
using System.Security.Cryptography;

namespace Esas.GeçiciBağlantı
{
    internal class BağlantıDeğişkeni
    {
        internal static string Yeni()
        {
            string gelişigüzel = GelişigüzelYazı();
            bool kimlik_kullanımda = false; //Veri tabanından denetleme komutu gelecek.

            while (kimlik_kullanımda)
            {
                gelişigüzel = GelişigüzelYazı();
                kimlik_kullanımda = false; //Veri tabanından denetleme komutu gelecek.
            }

            return gelişigüzel;
        }
        private static string GelişigüzelYazı()
        {
            RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
            byte[] ikili_yazı = new byte[64];
            üreteç.GetBytes(ikili_yazı);
            string yazı = Convert.ToBase64String(ikili_yazı);
            return yazı;
        }
    }
}