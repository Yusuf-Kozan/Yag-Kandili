using System;
using System.Security.Cryptography;

namespace Esas.GeçiciBağlantı
{
    internal class BağlantıDeğişkeni
    {
        internal static string Yeni()
        {
            string gelişigüzel = GelişigüzelYazı();
            bool kimlik_kullanımda = Esas.VeriTabanı.GeçiciBağlantı.BuBağlantıKullanımda(gelişigüzel);

            while (kimlik_kullanımda)
            {
                gelişigüzel = GelişigüzelYazı();
                kimlik_kullanımda = Esas.VeriTabanı.GeçiciBağlantı.BuBağlantıKullanımda(gelişigüzel);
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