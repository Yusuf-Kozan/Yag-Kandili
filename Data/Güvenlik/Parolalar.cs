using System;
using System.Security.Cryptography;

namespace Kilnevüg
{
    public static class Parolalar
    {
        public static string argon2id8_32_2_32(string parola, byte[] tuz)
        {
            //Argon2id ile parola karma fonksiyonu
            // 8 tekrar, 32 MiB bellek, 2 eş anlı işlem, 32 B çıktı
            byte[] byte_parola = System.Text.Encoding.Unicode.GetBytes(parola);
            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            byte[] karma = argon2id.ComputeHash(byte_parola, 8, (32*1024), 2, 32, tuz).HashBytes;

            string sonuç = Convert.ToBase64String(karma);

            string tuz_metni = Convert.ToBase64String(tuz);
            sonuç = $"{sonuç}₺8,{(32*1024).ToString()},2,32₺{tuz_metni}";
            return sonuç;
        }
        public static bool ParolaDoğru(string karma, string girilen_parola)
        {
            //Argon2id ile parola doğrulama fonksiyonu
            string[] karım_bilgileri = karma.Split('₺');
                // 0: karılmış parola, 1: değişkenler
                // 2: tuz

            string[] karım_değişkenleri = karım_bilgileri[1].Split(',');
                // 0: tekrar, 1: bellek(KB), 2: eş anlı işlem sayısı
                // 3: çıktı boyutu(B)
            int tekrar = int.Parse(karım_değişkenleri[0]);
            int bellek = int.Parse(karım_değişkenleri[1]);
            int eş_anlılık = int.Parse(karım_değişkenleri[2]);
            int çıktı_boyutu = int.Parse(karım_değişkenleri[3]);
            
            byte[] tuz = Convert.FromBase64String(karım_bilgileri[2]);
            byte[] bilinen_karma = Convert.FromBase64String(karım_bilgileri[0]);

            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            bool sonuç = argon2id.VerifyHash(
                bilinen_karma,
                System.Text.Encoding.Unicode.GetBytes(girilen_parola),
                tekrar, bellek, eş_anlılık, çıktı_boyutu,
                tuz
            ).Success;

            return sonuç;
        }

        public static byte[] YeniTuz()
        {
            RandomNumberGenerator rastgele_sayı = RandomNumberGenerator.Create();
            byte[] sonuç = new byte[32];
            rastgele_sayı.GetBytes(sonuç);
            return sonuç;
        }
    }
}