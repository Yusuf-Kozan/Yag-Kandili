#pragma checksum "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "516975baaf4d03320ecf9a3dc35ad7fc26b8cbba"
// <auto-generated/>
#pragma warning disable 1591
namespace sayfa
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
#line 3 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
using Kilnevüg;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
using Bileşenler;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(AnaTaslak))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/ana")]
    public partial class Ana : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 12 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
     if (oturum_açık)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(0, "        ");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "ana-beden");
            __builder.AddMarkupContent(3, "\n            ");
            __builder.OpenElement(4, "h1");
            __builder.AddContent(5, 
#nullable restore
#line 15 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
                 a

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\n            ");
            __builder.OpenElement(7, "h1");
            __builder.AddContent(8, 
#nullable restore
#line 16 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
                 b

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\n            ");
            __builder.OpenElement(10, "h2");
            __builder.AddContent(11, 
#nullable restore
#line 17 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
                 DateTime.Now.ToString("ddMMyyyyHHmmss")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\n            ");
            __builder.OpenElement(13, "h2");
            __builder.AddContent(14, 
#nullable restore
#line 18 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
                 oturum_açık.ToString()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\n        ");
            __builder.OpenComponent<sayfa.EnHerŞey>(17);
            __builder.AddAttribute(18, "aÜye", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Esas.ÜyeBil>(
#nullable restore
#line 20 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
                        üye

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(19, "\n");
#nullable restore
#line 21 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(20, "        ");
            __builder.AddMarkupContent(21, "<h1>Oturum açık değil</h1>\n");
#nullable restore
#line 25 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
    }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 29 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Pages/Ana.razor"
       
    private string a, b; //a == kullanıcı adı && b == kilmik
    private bool oturum_açık;
    public static ÜyeBil üye {get; set;}
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            var c = await JSruntime.InvokeAsync<string>("Çerezİşleri.ÇerezOku", "kullanıcı_adı");
            a = c.ToString(); c = null;
            var d = await JSruntime.InvokeAsync<string>("Çerezİşleri.ÇerezOku", "parola");
            b = d.ToString(); d = null;
            devam(a, b);
            StateHasChanged();
        }
        catch
        {
            NavigationManager.NavigateTo("/");
        }
    }
    private void devam(string kullanıcı_adı, string kilmik)
    {
        oturum_açık = İşlemler.Oturumİşlemleri.Oturum_Uygun(kullanıcı_adı, kilmik);
        if (!oturum_açık)
        {
            NavigationManager.NavigateTo("/");
        }
        üye = TabanlıVeri.Kilmikten_ÜyeBil(kilmik);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSruntime { get; set; }
    }
}
#pragma warning restore 1591
