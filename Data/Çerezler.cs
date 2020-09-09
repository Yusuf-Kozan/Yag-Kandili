using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Esas
{
    class Çerezler
    {
        private readonly IJSRuntime jsRuntime;

        private string çDeğeri;
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
        private async void ÇerezOku(string çerezAdı)
        {
            var a = await jsRuntime.InvokeAsync<string>("Çerezİşleri.ÇerezOku", çerezAdı);
            çDeğeri = a.ToString();
        }
        public async void ÇerezSil(string çerezAdı)
        {
            await jsRuntime.InvokeVoidAsync("Çerezİşleri.ÇerezSil", çerezAdı);
        }
        public bool ÇerezVar(string çerezAdı)
        {
            ÇerezOku(çerezAdı);
            if (!String.IsNullOrWhiteSpace(çDeğeri))
                return true;
            return false;
        }
    }
}