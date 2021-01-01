namespace Kilnevüg
{
    public static class Parolalar
    {
        public static string azDahaGüvenliParola(string parola, string tuz)
        {
            //Argon2id ile parola karma fonksiyonu
            // 4 tekrar, 16 MB bellek, 1 eş zamanlı işlem, 32 B çıktı
            byte[] bParola = System.Text.Encoding.Unicode.GetBytes(parola);
            byte[] bTuz = System.Text.Encoding.Unicode.GetBytes(tuz);
            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            string sonuç = argon2id.ComputeHash(bParola, 4, (16*1024), 1, 32, bTuz).HashString;
            return sonuç;
        }
    }
}