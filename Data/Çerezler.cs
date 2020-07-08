using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

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
        public async Task<string> ÇerezOku(string çerezAdı)
        {
            return await jsRuntime.InvokeAsync<string>("Çerezİşleri.ÇerezOku", çerezAdı);
        }
        public async void ÇerezSil(string çerezAdı)
        {
            await jsRuntime.InvokeVoidAsync("Çerezİşleri.ÇerezSil", çerezAdı);
        }
    }
}