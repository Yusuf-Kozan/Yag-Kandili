#pragma checksum "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/GirişTaslak.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ecb74132a008d469fc91f26bd6fedcfd3ef80f0"
// <auto-generated/>
#pragma warning disable 1591
namespace YağKandili.Shared
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
#line 10 "/media/yusuf/Depo/yusuf/projeler/YağKandili/_Imports.razor"
using Esas;

#line default
#line hidden
#nullable disable
    public partial class GirişTaslak : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<header class=\"giriş-header\">\n    <img alt=\"Yağ Kandili logosu\" src=\"/logo/yRenkli.png\">\n</header>\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "giriş-body");
            __builder.AddMarkupContent(3, "\n    ");
            __builder.AddContent(4, 
#nullable restore
#line 9 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/GirişTaslak.razor"
     Body

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(5, "\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/GirişTaslak.razor"
       
    private void dön()
    {
        NavigationManager.NavigateTo("/");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
