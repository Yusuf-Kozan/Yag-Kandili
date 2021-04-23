using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Oturum
    {
        public static void OturumAç(string oturum_kimliği)
        {
            // Kullanıcı_Adı, Kilmik, Tarih, Son_Tarih, Kapandı
            ZöçKilmik ÇözülmüşOturumKimliği = Kilnevüg.Kilnevüg.ÇözülmüşKimlik(oturum_kimliği);
            string komut_metni = $"INSERT INTO {TabloAdı()}(Kullanıcı_Adı, Kilmik, Tarih, Son_Tarih, Kapandı) "+
                        $"VALUES (@kullanıcı_adı, @oturum_kimliği, '{ÇözülmüşOturumKimliği.hirat.ToString()}',"+
                        $" '{ÇözülmüşOturumKimliği.hirat.AddHours(4).ToString()}', '0');";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", ÇözülmüşOturumKimliği.daluk);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        public static void OturumKapat(string oturum_kimliği)
        {
            string komut_metni = $"UPDATE {TabloAdı()} SET Kapandı = '1', Kapanma_Tarihi = '{DateTime.Now.ToString()}' "+
                        $"WHERE Kilmik = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }

        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[2].StartsWith("-"))
            {
                return belge_içeriği[2].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}