#pragma checksum "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b858417c5e189ec6eb4b1697d1ad7df45243d714"
// <auto-generated/>
#pragma warning disable 1591
namespace YağKandili.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using YağKandili;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/kozan/Projects/YKb/YağKandili/_Imports.razor"
using YağKandili.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
using Bileşenler;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Giriş : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddMarkupContent(1, "\n    ");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "style", "text-align: center; position: sticky;");
            __builder.AddMarkupContent(4, "\n        ");
            __builder.OpenElement(5, "button");
            __builder.AddAttribute(6, "class", "D3 gir2");
            __builder.AddAttribute(7, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 8 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
                                          gir

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(8, "Giriş Yap");
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\n        ");
            __builder.OpenElement(10, "button");
            __builder.AddAttribute(11, "class", "D1 gir2");
            __builder.AddAttribute(12, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 9 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
                                          yak

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(13, "Bir Kandil Yak");
            __builder.CloseElement();
            __builder.AddMarkupContent(14, "\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\n");
#nullable restore
#line 11 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
     if (düğme == 0)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(16, "        <div></div>\n");
#nullable restore
#line 14 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
    }
    else if (düğme == 1)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(17, "        ");
            __builder.OpenComponent<Bileşenler.OturumAç>(18);
            __builder.AddAttribute(19, "yön", "/ana");
            __builder.CloseComponent();
            __builder.AddMarkupContent(20, "\n");
#nullable restore
#line 18 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
    }
    else if (düğme == 2)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(21, "        <div></div>\n");
#nullable restore
#line 22 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 25 "/home/kozan/Projects/YKb/YağKandili/Pages/Giriş.razor"
       
    private static int düğme {get; set;} = 0;
    private void gir()
    {
        if(düğme != 1)
            düğme = 1;
    }
    private void yak()
    {
        if(düğme != 2)
            düğme = 2;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
