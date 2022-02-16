using System;
using System.Globalization;

namespace Esas
{
    public class Takip
    {
        public string TAKİP_EDEN {get; set;}
        public string TAKİP_EDİLEN {get; set;}
        public short TAKİP_DÜZEYİ {get; set;}
        // 1- bildirimli , 2- bildirimsiz
        public DateTime TARİH {get; set;}

        public Takip() {}
        public Takip(string takip_eden, string takip_edilen,
                    short takip_düzeyi, DateTime tarih)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            TARİH = tarih;
        }
        public Takip(string takip_eden, string takip_edilen,
                    short takip_düzeyi, string tarih)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
        }

        public string TarihMetni()
        {
            return TARİH.ToString("yyyyMMddHHmmss");
        }
    }

    public struct takip
    {
        public string TAKİP_EDEN, TAKİP_EDİLEN;
        public short TAKİP_DÜZEYİ;
        // 1- bildirimli , 2- bildirimsiz
        public DateTime TARİH;

        public takip(string takip_eden, string takip_edilen,
                    short takip_düzeyi, DateTime tarih)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            TARİH = tarih;
        }
        public takip(string takip_eden, string takip_edilen,
                    short takip_düzeyi, string tarih)
        {
            TAKİP_EDEN = takip_eden;
            TAKİP_EDİLEN = takip_edilen;
            TAKİP_DÜZEYİ = takip_düzeyi;
            CultureInfo TR = new CultureInfo("tr-TR");
            TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
        }

        public string TarihMetni()
        {
            return TARİH.ToString("yyyyMMddHHmmss");
        }
    }
}