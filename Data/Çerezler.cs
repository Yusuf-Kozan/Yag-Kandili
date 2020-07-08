using System;
using Microsoft.JSInterop;

namespace Esas
{
    class Çerezler
    {
        private readonly IJSRuntime jsRuntime;

        public Çerezler(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async void ÇerezYap(string ad, string değer, int kaçSaat)
        {
            var değişken = new object[3];
            değişken[0] = ad; değişken[1] = değer; değişken[2] = kaçSaat;
            await jsRuntime.InvokeVoidAsync("Çerezİşleri.ÇerezYap", değişken);
        }
    }
}