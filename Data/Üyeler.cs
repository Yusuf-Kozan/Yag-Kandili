/*
Copyright (C) 2022 Yusuf Kozan

---

Bu belge Yağ Kandili'nin bir parçasıdır.

Yağ Kandili bir özgür yazılımdır: GNU Affero Genel Kamu Lisansı'nın 3.
ya da (isteğinize göre) sonraki bir sürümünün Free Software Foundation
tarafından yayınlandığı durumunun koşulları altında Yağ Kandili'ni
dağıtabilir veya Yağ Kandili üzerinde değişiklik yapabilirsiniz.

Yağ Kandili kullanışlı olması umuduyla dağıtılmaktadır ancak HİÇBİR
GARANTİ VERMEMEKTEDİR, zımni PAZARLANABİLİRLİK veya BELİRLİ BİR
AMACA UYGUNLUK garantisi bile. Daha çok ayrıntı için GNU Affero
Genel Kamu Lisansı'na bakın.

Yağ Kandili'nin yanında GNU Affero Genel Kamu Lisansı'nın bir kopyasını
almış olmalısınız. Almadıysanız, <https://www.gnu.org/licenses/>
adresine bakın.

Yağ Kandili'nin lisansıyla ilgili daha çok bilgi için /Lisans
dizinine bakın.

---

This file is part of Yağ Kandili.

Yağ Kandili is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Yağ Kandili is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Yağ Kandili. If not, see <https://www.gnu.org/licenses/>.

For more information about the license of Yağ Kandili, see
/Lisans directory.
*/
using System;
using System.Globalization;
using Kilnevüg;
namespace Esas
{
    public class ÜyeBil
    {
        public string AD { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string PAROLA { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }
        public string KİMLİK { get; set; }

        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, string resim)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            RESİM = resim;
        }
        public ÜyeBil(string ad, string kullanıcıAdı, string parola, string ePosta, string üstünlük, DateTime başlangıç, string resim)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcıAdı;
            PAROLA = parola;
            E_POSTA = ePosta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç;
            RESİM = resim;
        }
        public ÜyeBil(Üye üye)
        {
            AD = üye.AD;
            KULLANICI_ADI = üye.KULLANICI_ADI;
            PAROLA = üye.PAROLA;
            E_POSTA = üye.E_POSTA;
            ÜSTÜNLÜK = üye.ÜSTÜNLÜK;
            BAŞLANGIÇ = üye.BAŞLANGIÇ;
            RESİM = üye.RESİM;
        }
        public ÜyeBil() { }

        public string DizinYolu()
        {
            string üstDizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üstDizin, KULLANICI_ADI);
        }
    }

    public struct Üye
    {
        public string AD {get; set;}
        public string KULLANICI_ADI {get; set;}
        public string PAROLA {get; set;}
        public string E_POSTA {get; set;}
        public string ÜSTÜNLÜK {get; set;}
        public DateTime BAŞLANGIÇ {get; set;}
        public string RESİM {get; set;}

        public Üye(string ad, string kullanıcı_adı, string parola,
                    string e_posta, string üstünlük, DateTime başlangıç_tarihi, string resim)
        {
            AD = ad;
            KULLANICI_ADI = kullanıcı_adı;
            PAROLA = parola;
            E_POSTA = e_posta;
            ÜSTÜNLÜK = üstünlük;
            BAŞLANGIÇ = başlangıç_tarihi;
            RESİM = resim;
        }

        public string DizinYolu()
        {
            string üst_dizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üst_dizin, KULLANICI_ADI);
        }
    }
    public struct parolasız_üye
    {
        public string AD { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }
        public string KİMLİK { get; set; }

        public parolasız_üye (string[] üye_bilgileri)
        {
            AD = üye_bilgileri[0];
            KULLANICI_ADI = üye_bilgileri[1];
            E_POSTA = üye_bilgileri[2];
            ÜSTÜNLÜK = üye_bilgileri[3];
            CultureInfo TR = new CultureInfo("tr-TR");
            BAŞLANGIÇ = DateTime.ParseExact(üye_bilgileri[4], "yyyyMMddHHmmss", TR);
            RESİM = üye_bilgileri[5];
            KİMLİK = üye_bilgileri[6];
        }
        public string DizinYolu()
        {
            string üst_dizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üst_dizin, KULLANICI_ADI);
        }
    }
    public class ParolasızÜye
    {
        public string AD { get; set; }
        public string KULLANICI_ADI { get; set; }
        public string E_POSTA { get; set; }
        public string ÜSTÜNLÜK { get; set; }
        public DateTime BAŞLANGIÇ { get; set; }
        public string RESİM { get; set; }
        public string KİMLİK { get; set; }

        public ParolasızÜye (string[] üye_bilgileri)
        {
            AD = üye_bilgileri[0];
            KULLANICI_ADI = üye_bilgileri[1];
            E_POSTA = üye_bilgileri[2];
            ÜSTÜNLÜK = üye_bilgileri[3];
            CultureInfo TR = new CultureInfo("tr-TR");
            BAŞLANGIÇ = DateTime.ParseExact(üye_bilgileri[4], "yyyyMMddHHmmss", TR);
            RESİM = üye_bilgileri[5];
            KİMLİK = üye_bilgileri[6];
        }
        public string DizinYolu()
        {
            string üst_dizin = @"./Kullanıcılar";
            return System.IO.Path.Combine(üst_dizin, KULLANICI_ADI);
        }
    }
}
