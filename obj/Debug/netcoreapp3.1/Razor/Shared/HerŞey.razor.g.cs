#pragma checksum "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/HerŞey.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f92367b242e6c0134055ab9d5afc1180570b35d"
// <auto-generated/>
#pragma warning disable 1591
namespace Bileşenler
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using YağKandili;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using YağKandili.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Bileşenler;

#line default
#line hidden
#nullable disable
    public partial class HerŞey : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "style", 
#nullable restore
#line 3 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/HerŞey.razor"
             üstBiçim

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(2, "\n    ");
            __builder.OpenComponent<Bileşenler.Kim>(3);
            __builder.AddAttribute(4, "kişi", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Esas.ÜyeBil>(
#nullable restore
#line 4 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/HerŞey.razor"
                Kim

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(5, "\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 7 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/HerŞey.razor"
       
    [Parameter]
    public string Nerede {private get; set;} = "/ana";
    [Parameter]
    public static string yGenişlik {private get; set;} = "60";
    [Parameter]
    public static string yYükseklik {private get; set;} = "100";
    [Parameter]
    public ÜyeBil Kim {private get; set;}

    private string üstBiçim = $"height: {yYükseklik}%; width: {yGenişlik}%;" +
    $"margin-left: {((100 - Convert.ToInt32(yGenişlik))/2).ToString()}%;" +
    $"margin-right: {((100 - Convert.ToInt32(yGenişlik))/2).ToString()}%;" +
    " padding-top: 1%;";

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
