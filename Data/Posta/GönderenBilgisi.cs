using System;
using System.IO;
namespace Esas.Posta
{
    public struct GönderenBilgisi
    {
        public string SUNUCU,
                    PORT,
                    ADRES,
                    PAROLA;

        public GönderenBilgisi(string sunucu, string port,
                                string adres, string parola)
        {
            SUNUCU = sunucu; PORT = port;
            ADRES = adres; PAROLA = parola;
        }
        
        public void AyarBelgesiniOku()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/ep1");

            if (belge_içeriği[0].Length <= 8 || belge_içeriği[1].Length <= 6
                || belge_içeriği[2].Length <= 7 || belge_içeriği[3].Length <= 8)
                // "sunucu: ", "port: ", "adres: ", "parola: "
            {
                return;
            }

            SUNUCU = belge_içeriği[0].Substring(8);
            PORT = belge_içeriği[1].Substring(6);
            ADRES = belge_içeriği[2].Substring(7);
            PAROLA = belge_içeriği[3].Substring(8);
        }
    }
}