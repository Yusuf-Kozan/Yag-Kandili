#pragma checksum "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d6b3a3c709c8a61759cd2578e3be35d841f0129"
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
#line 1 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
using Kilnevüg;

#line default
#line hidden
#nullable disable
    public partial class BIKTIM : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "form");
            __builder.AddAttribute(2, "style", "display: block; text-align: center;");
            __builder.AddMarkupContent(3, "\n");
#nullable restore
#line 9 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
     if(Başlık == "Bir Kandil Yak")
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(4, "        ");
            __builder.OpenElement(5, "h4");
            __builder.AddAttribute(6, "class", "formBaşlık");
            __builder.AddAttribute(7, "style", "color:rgb(0,120,0); font-size: x-large;");
            __builder.AddContent(8, 
#nullable restore
#line 11 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                Başlık

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\n");
#nullable restore
#line 12 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(10, "        ");
            __builder.OpenElement(11, "h4");
            __builder.AddAttribute(12, "class", "formBaşlık");
            __builder.AddAttribute(13, "style", "color:#4169e1; font-size: x-large;");
            __builder.AddContent(14, 
#nullable restore
#line 15 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                           Başlık

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\n");
#nullable restore
#line 16 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(16, "    <p style=\"color: #cc0000;\"></p>\n");
#nullable restore
#line 18 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
     if (sayaç == 0) 
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(17, "    ");
            __builder.OpenElement(18, "div");
            __builder.OpenElement(19, "input");
            __builder.AddAttribute(20, "type", "text");
            __builder.AddAttribute(21, "placeholder", "Adınız");
            __builder.AddAttribute(22, "class", "MetinKutusu1");
            __builder.AddAttribute(23, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 20 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                             Üye.AD

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Üye.AD = __value, Üye.AD));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\n    ");
            __builder.OpenElement(26, "div");
            __builder.OpenElement(27, "input");
            __builder.AddAttribute(28, "type", "text");
            __builder.AddAttribute(29, "placeholder", "Soyadınız");
            __builder.AddAttribute(30, "class", "MetinKutusu1");
            __builder.AddAttribute(31, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 21 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                Üye.SOYADI

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(32, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Üye.SOYADI = __value, Üye.SOYADI));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\n");
#nullable restore
#line 22 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }
    else if (sayaç == 1)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(34, "    ");
            __builder.OpenElement(35, "div");
            __builder.OpenElement(36, "input");
            __builder.AddAttribute(37, "type", "text");
            __builder.AddAttribute(38, "placeholder", "E-Posta Adresiniz");
            __builder.AddAttribute(39, "class", "MetinKutusu1");
            __builder.AddAttribute(40, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 25 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                        Üye.E_POSTA

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(41, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Üye.E_POSTA = __value, Üye.E_POSTA));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(42, "\n    ");
            __builder.OpenElement(43, "div");
            __builder.OpenElement(44, "input");
            __builder.AddAttribute(45, "type", "text");
            __builder.AddAttribute(46, "placeholder", "Kullanıcı Adınız");
            __builder.AddAttribute(47, "class", "MetinKutusu1");
            __builder.AddAttribute(48, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 26 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                       Üye.KULLANICI_ADI

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(49, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Üye.KULLANICI_ADI = __value, Üye.KULLANICI_ADI));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\n");
#nullable restore
#line 27 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }
    else if (sayaç == 2)
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(51, "    ");
            __builder.OpenElement(52, "div");
            __builder.OpenElement(53, "input");
            __builder.AddAttribute(54, "type", "password");
            __builder.AddAttribute(55, "placeholder", "Parolanız");
            __builder.AddAttribute(56, "class", "MetinKutusu1");
            __builder.AddAttribute(57, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 30 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                    Üye.PAROLA

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(58, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Üye.PAROLA = __value, Üye.PAROLA));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(59, "\n    ");
            __builder.OpenElement(60, "div");
            __builder.OpenElement(61, "input");
            __builder.AddAttribute(62, "type", "password");
            __builder.AddAttribute(63, "placeholder", "Parolanız (Tekrar)");
            __builder.AddAttribute(64, "class", "MetinKutusu1");
            __builder.AddAttribute(65, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 31 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                                                             diğerParola

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(66, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => diğerParola = __value, diğerParola));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(67, "\n");
#nullable restore
#line 32 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddContent(68, "        ");
            __builder.AddMarkupContent(69, "<h5>HEY</h5>\n");
#nullable restore
#line 36 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.AddContent(70, "    ");
            __builder.OpenElement(71, "div");
            __builder.OpenElement(72, "button");
            __builder.AddAttribute(73, "class", "D1");
            __builder.AddAttribute(74, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 37 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                      kandilYak

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(75, 
#nullable restore
#line 37 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
                                                  Düğme

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(76, "\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 40 "/media/yusuf/Depo/yusuf/projeler/YağKandili/Shared/BIKTIM.razor"
       
    private string Başlık {get; set;} = "Bir Kandil Yak";
    private string Düğme {get; set;} = "İlerle";
    private string p {get; set;} = "";
    private ÜyeBil Üye {get; set;}
    //private Üye Üye {get; set;}
    private string diğerParola {get; set;}
    private int sayaç = 0; // 0->Ad, Soyadı | 1-> E Posta, Kullanıcı Adı | 2->Parola
    private void kandilYak()
    {
        if (Düğme == "İlerle")
        {
            if (sayaç == 0)
            {
                string[] metinler = new string[]{Üye.AD, Üye.SOYADI};
                if (!Girdiler.someAreNullOrWhiteSpace(metinler) && Girdiler.tümMetinlerUygun(metinler))
                {}
                else
                {

                }
            }
            else if (sayaç == 1)
            {}
            else
            {}
        }
        else if (Düğme == "Bir Kandil Yak")
        {
            if (sayaç == 2)
            {}
            else
            {}
        }
        else
        {
            sayaç = 0; Başlık = "Bir Kandil Yak"; Düğme = "İlerle";
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
