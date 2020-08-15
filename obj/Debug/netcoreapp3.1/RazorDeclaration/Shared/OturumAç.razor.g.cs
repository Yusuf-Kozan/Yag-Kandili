#pragma checksum "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/OturumAç.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f20456ed352f7f9013f6e2aec70beb912008caa8"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Bileşenler
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using YağKandili;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using YağKandili.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/OturumAç.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/OturumAç.razor"
using Kilnevüg;

#line default
#line hidden
#nullable disable
    public partial class OturumAç : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 23 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/OturumAç.razor"
       
    [Parameter]
    public string yön {get; set;}

    private string Başlık = "Giriş Yap";
    private string[] girBil = new string[2]; //0 = kulad, 1 = parol
    private void cGiriş()
    {
        string kulad = girBil[0];
        string parol = girBil[1];
        if (!Girdiler.allIsNullOrWhiteSpace(girBil))
        {
            if (!Girdiler.tümMetinlerUygun(girBil))
            {
                Başlık = "Kullanıcı Adınızı veya Parolanızı Hatalı Girdiniz";
            }
            else if (TabanlıVeri.KPDoğru(kulad,parol))
            {
                Başlık = "Giriş Yap";
                string kilmik = Kilnevüg.YeniEşsizKimlik(kulad);
                TabanlıVeri.OturumAç(kilmik);
                Çerezler çerez = new Çerezler(JSRuntime);
                çerez.ÇerezYap(kulad, kilmik, 12);
                if(String.IsNullOrWhiteSpace(yön))
                    NavigationManager.NavigateTo("/ana");
                else
                    NavigationManager.NavigateTo(yön);
            }
            else
            {
                Başlık = "Kullanıcı Adınızı veya Parolanızı Hatalı Girdiniz";
            }
        }
        else
        {
            Başlık = "Kullanıcı Adınızı veya Parolanızı Girmediniz";
        }
        kulad = null;
        parol = null;
    }

    private void cParolaSıfırla()
    {}

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
