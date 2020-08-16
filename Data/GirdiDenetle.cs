using System;

namespace Kilnevüg
{
    public class Girdiler
    {
        public static bool metinUygun(string metin)
        {
            char[] mtn = metin.ToCharArray();
            int hataSayısı = 0;
            for (int i = 0; i < mtn.Length; i++)
            {
                if ( (mtn[i] == ';') || (mtn[i] == '(') || (mtn[i] == ')') )
                {
                    hataSayısı++;
                    return false;
                }
            }
            if (hataSayısı == 0)
                return true;
            return false;
        }
        public static bool tümMetinlerUygun(string[] metinler)
        {
            //Tüm metinler uygunsa true, herhangi biri uygun değilse false değerini döndürür.
            int hataSayısı = 0;
            for (int i = 0; i < metinler.Length; i++)
            {
                if (!metinUygun(metinler[i]))
                {
                    hataSayısı++;
                    return false;
                }
            }
            if (hataSayısı == 0)
                return true;
            return false;
        }
        public static bool allAreNullOrWhiteSpace(string[] metinler)
        {
            //Tüm metinler boşsa true, herhangi biri doluysa false değerini döndürür.
            for (int i = 0; i < metinler.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(metinler[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool someAreNullOrWhiteSpace(string[] metinler)
        {
            //Herhangi bir metin boşsa true, tüm metinler doluysa false değerini döndürür.
            int sayaç = 0;
            for (int i = 0; i < metinler.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(metinler[i]))
                {
                    sayaç++;
                }
            }
            if (sayaç != 0)
            {
                return false;
            }
            return true;
        }
    }
}