namespace Kilnevüg
{
    public static class Parolalar
    {
        public static string ar2id4_32_2_32(string parola, string tuz)
        {
            //Argon2id ile parola karma fonksiyonu
            // 4 tekrar, 32 MiB bellek, 2 eş zamanlı işlem, 32 B çıktı
            byte[] bParola = System.Text.Encoding.Unicode.GetBytes(parola);
            byte[] bTuz = System.Text.Encoding.Unicode.GetBytes(tuz);
            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            string sonuç = argon2id.ComputeHash(bParola, 4, (32*1024), 2, 32, bTuz).HashString;
            return sonuç;
        }
    }
}