using System;
using System.IO;
namespace Esas.VeriTabanı
{
    public class Bağlantı
    {
        public static string bağlantı_dizesi = $"Server={MySQL_SunucuAdresi()[0]};Port={MySQL_SunucuAdresi()[1]};" + 
                                        $"Database={VeriTabanınınAdı()};User ID={MySQL_KullanıcıAdıİleParola()[0]};" + 
                                        $"Password={MySQL_KullanıcıAdıİleParola()[1]};Pooling=false;";
        private static string[] MySQL_SunucuAdresi()
        {
            // Sunucu ve Port
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt1");
            if (belge_içeriği.Length < 4 &&
                belge_içeriği[0].StartsWith(">") && belge_içeriği[1].StartsWith(">"))
            {
                string[] adres_bilgileri = new string[2];
                adres_bilgileri[0] = belge_içeriği[0].Substring(1);//sunucu
                adres_bilgileri[1] = belge_içeriği[1].Substring(1);//port
                return adres_bilgileri;
            }
            else
            {
                return new string[]{String.Empty, String.Empty};
            }
        }
        private static string[] MySQL_KullanıcıAdıİleParola()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt1");
            if (belge_içeriği[2].StartsWith(">") && belge_içeriği[3].StartsWith(">"))
            {
                string[] kullanıcıAdıİleParola = new string[2];
                kullanıcıAdıİleParola[0] = belge_içeriği[2].Substring(1);//kullanıcı adı
                if (belge_içeriği[3].Length > 1) //parola girilmiş
                {
                    kullanıcıAdıİleParola[1] = belge_içeriği[3].Substring(1);//parola
                }
                else //parola yok
                {
                    kullanıcıAdıİleParola[1] = String.Empty;
                }
                return kullanıcıAdıİleParola;
            }
            else
            {
                return new string[]{String.Empty, String.Empty};
            }
        }
        private static string VeriTabanınınAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği[0].StartsWith(">"))
            {
                return belge_içeriği[0].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}