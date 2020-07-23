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
    }
}