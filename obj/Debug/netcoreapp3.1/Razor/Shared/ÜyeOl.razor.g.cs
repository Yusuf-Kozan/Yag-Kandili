#pragma checksum "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afa81d0a1c7ac1928d0a8dea204a6005806a25cb"
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
#line 11 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/_Imports.razor"
using Bileşenler;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
using Esas;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
using Kilnevüg;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
using İşlemler;

#line default
#line hidden
#nullable disable
    public partial class ÜyeOl : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "form");
            __builder.AddAttribute(2, "style", "display: block; text-align: center;");
            __builder.AddMarkupContent(3, "\n");
#nullable restore
#line 10 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
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
#line 12 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                Başlık

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\n");
#nullable restore
#line 13 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
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
#line 16 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                           Başlık

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\n");
#nullable restore
#line 17 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.AddContent(16, "    ");
            __builder.OpenElement(17, "p");
            __builder.AddAttribute(18, "style", "color: #cc0000;");
            __builder.AddContent(19, 
#nullable restore
#line 18 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                p

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\n    ");
            __builder.OpenElement(21, "div");
            __builder.OpenElement(22, "input");
            __builder.AddAttribute(23, "type", "text");
            __builder.AddAttribute(24, "placeholder", "Adınız");
            __builder.AddAttribute(25, "class", "MetinKutusu1");
            __builder.AddAttribute(26, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                             bilgiler[0]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[0] = __value, bilgiler[0]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\n    ");
            __builder.OpenElement(29, "div");
            __builder.OpenElement(30, "input");
            __builder.AddAttribute(31, "type", "text");
            __builder.AddAttribute(32, "placeholder", "Soyadınız");
            __builder.AddAttribute(33, "class", "MetinKutusu1");
            __builder.AddAttribute(34, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 20 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                bilgiler[1]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(35, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[1] = __value, bilgiler[1]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\n    ");
            __builder.OpenElement(37, "div");
            __builder.OpenElement(38, "input");
            __builder.AddAttribute(39, "type", "text");
            __builder.AddAttribute(40, "placeholder", "E-Posta Adresiniz");
            __builder.AddAttribute(41, "class", "MetinKutusu1");
            __builder.AddAttribute(42, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 21 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                        bilgiler[2]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(43, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[2] = __value, bilgiler[2]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(44, "\n    ");
            __builder.OpenElement(45, "div");
            __builder.OpenElement(46, "input");
            __builder.AddAttribute(47, "type", "text");
            __builder.AddAttribute(48, "placeholder", "Kullanıcı Adınız");
            __builder.AddAttribute(49, "class", "MetinKutusu1");
            __builder.AddAttribute(50, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 22 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                       bilgiler[3]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(51, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[3] = __value, bilgiler[3]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(52, "\n    ");
            __builder.OpenElement(53, "div");
            __builder.OpenElement(54, "input");
            __builder.AddAttribute(55, "type", "password");
            __builder.AddAttribute(56, "placeholder", "Parolanız");
            __builder.AddAttribute(57, "class", "MetinKutusu1");
            __builder.AddAttribute(58, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 23 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                    bilgiler[4]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(59, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[4] = __value, bilgiler[4]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(60, "\n    ");
            __builder.OpenElement(61, "div");
            __builder.OpenElement(62, "input");
            __builder.AddAttribute(63, "type", "password");
            __builder.AddAttribute(64, "placeholder", "Parolanız (Tekrar)");
            __builder.AddAttribute(65, "class", "MetinKutusu1");
            __builder.AddAttribute(66, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 24 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                                                             bilgiler[5]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(67, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => bilgiler[5] = __value, bilgiler[5]));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(68, "\n    \n    ");
            __builder.OpenElement(69, "div");
            __builder.OpenElement(70, "button");
            __builder.AddAttribute(71, "class", "D1");
            __builder.AddAttribute(72, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                      kandilYak

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(73, 
#nullable restore
#line 28 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
                                                  Düğme

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "/media/yusuf/Depo/yusuf/projeler/Yag-Kandili/Shared/ÜyeOl.razor"
       
    private string Başlık {get; set;} = "Bir Kandil Yak";
    private string Düğme {get; set;} = "Bir Kandil Yak";
    private string p {get; set;} = "Noktalı virgül ve yay ayraç kullanılamaz. Bütün alanlar doldurulmalıdır.";
    private string[] bilgiler = new string[6]; //ad, soyadı, e-posta, kullanıcı adı, parola, parola(tekrar)
    private void kandilYak()
    {
        if (Girdiler.tümMetinlerUygun(bilgiler))
        {
            if (bilgiler[4] == bilgiler[5])
            {
                bool kullanıcıVar = TabanlıVeri.Kullanıcı_Var(bilgiler[3]), ePostaVar = TabanlıVeri.ePosta_Var(bilgiler[2]); 
                if (!kullanıcıVar && !ePostaVar)
                {
                    Başlık = "Bir Kandil Yak";
                    çÜye yeniüye = new çÜye(bilgiler[0], bilgiler[1], bilgiler[3], bilgiler[4], bilgiler[2], DateTime.Now, "kullanıcı");
                    bilgiler = null;
                    Oturumİşlemleri.Üye_Oluştur(yeniüye);
                    Oturumİşlemleri.Oturum_Başlat(yeniüye.KULLANICI_ADI, JSRuntime);
                    NavigationManager.NavigateTo("/ana");
                }
                else if (kullanıcıVar)
                {
                    Başlık = "Bu Kullanıcı Adı Başkası Tarafından Kullanılıyor"; bilgiler[3] = null;
                }
                else if (ePostaVar)
                {
                    Başlık = "Bu E-Posta Adresi Başkası Tarafından Kullanılıyor"; bilgiler[2] = null;
                }
            }
            else
            {
                Başlık = "Girilen iki parola aynı değil";
                bilgiler[4] = null; bilgiler[5] = null;
            }
        }
        else
        {
            Başlık = "Şartlara Uygun Bilgi Verin";
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
