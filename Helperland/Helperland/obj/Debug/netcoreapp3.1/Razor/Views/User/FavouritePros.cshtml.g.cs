#pragma checksum "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6df4c229103ff272badd55f5e54bf751acecc0e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_FavouritePros), @"mvc.1.0.view", @"/Views/User/FavouritePros.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Tatvasoft\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Tatvasoft\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Tatvasoft\Helperland\Helperland\Views\_ViewImports.cshtml"
using Helperland.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6df4c229103ff272badd55f5e54bf751acecc0e", @"/Views/User/FavouritePros.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96bb1ef7955c1397a49dbc9396c03a07262c68b9", @"/Views/_ViewImports.cshtml")]
    public class Views_User_FavouritePros : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FavouriteProsViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/avatar-hat.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("avatar-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/star-ratting-filled.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/star-ratting-empty.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
  
    ViewBag.Title = "Helperland - Favourite Pros";
    ViewBag.Header = "";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\" my-setting my-sp-setting dashboard-table row-table\">\r\n    <div class=\"d-flex px-4 justify-content-between flex-wrap\">\r\n");
#nullable restore
#line 9 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
         foreach (FavouriteProsViewModel fav in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"user-box my-3 py-2\">\r\n                <div class=\"text-center\">\r\n                    <div class=\"my-2 text-center\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d6df4c229103ff272badd55f5e54bf751acecc0e5819", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</div>\r\n                    <div class=\"fw-bold mt-3\">");
#nullable restore
#line 14 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                         Write(fav.TargetUserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <div class=\"mb-3\">\r\n                        <span class=\"rating-star\">\r\n");
#nullable restore
#line 17 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                               int i = 0; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                             for (i = 0; i < @Math.Round(fav.Ratings); i++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d6df4c229103ff272badd55f5e54bf751acecc0e7855", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 21 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                             while (i != 5)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d6df4c229103ff272badd55f5e54bf751acecc0e9435", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 25 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                i++;
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <span class=\"ratting-number\">");
#nullable restore
#line 28 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                                    Write(fav.Ratings.ToString("F"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </span>\r\n                    </div>\r\n                    <div class=\"my-3\">");
#nullable restore
#line 31 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                 Write(fav.Cleanings);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Cleanings</div>\r\n                    <div class=\"my-3\"");
            BeginWriteAttribute("id", " id=\"", 1458, "\"", 1470, 1);
#nullable restore
#line 32 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
WriteAttributeValue("", 1463, fav.Id, 1463, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <span");
            BeginWriteAttribute("class", " class=\"", 1503, "\"", 1570, 3);
            WriteAttributeValue("", 1511, "block-button", 1511, 12, true);
            WriteAttributeValue(" ", 1523, "fav-button", 1524, 11, true);
#nullable restore
#line 33 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
WriteAttributeValue(" ", 1534, fav.IsFavourite ? "blocked" : "", 1535, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("onclick", " onclick=\"", 1571, "\"", 1599, 3);
            WriteAttributeValue("", 1581, "favourite(", 1581, 10, true);
#nullable restore
#line 33 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
WriteAttributeValue("", 1591, fav.Id, 1591, 7, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1598, ")", 1598, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 33 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                                                                                                           Write(fav.IsFavourite ? "Remove" : "Favourite");

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        <span");
            BeginWriteAttribute("class", " class=\"", 1682, "\"", 1747, 3);
            WriteAttributeValue("", 1690, "block-button", 1690, 12, true);
            WriteAttributeValue(" ", 1702, "blk-button", 1703, 11, true);
#nullable restore
#line 34 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
WriteAttributeValue(" ", 1713, fav.IsBlocked ? "blocked" : "", 1714, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("onclick", " onclick=\"", 1748, "\"", 1772, 3);
            WriteAttributeValue("", 1758, "block(", 1758, 6, true);
#nullable restore
#line 34 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
WriteAttributeValue("", 1764, fav.Id, 1764, 7, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1771, ")", 1771, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 34 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
                                                                                                                     Write(fav.IsBlocked ? "Unblock" : "Block");

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 38 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
         if (Model.Count() == 0) 
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"text-danger m-2\">No Service Provider has worked with you as of now</div>\r\n");
#nullable restore
#line 42 "D:\Tatvasoft\Helperland\Helperland\Views\User\FavouritePros.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>

<script type=""text/javascript"">
    function block(id) {
        console.log(""block funtion"");
        var value;
        if ($(""#"" + id + "" .blk-button"").html() == ""Block"") {
            value = 'False';
        }
        else {
            value = 'True';
        }
        $(""#"" + id + "" .blk-button"").html(""Processing"").load(`/User/BlockUnblock/${id}/${value}`);
        $(""#"" + id + "" .blk-button"").toggleClass(""blocked"");
    }
    function favourite(id) {
        console.log(""Fav funtion"");
        var value;
        if ($(""#"" + id + "" .fav-button"").html() == ""Favourite"") {
            value = 'False';
        }
        else {
            value = 'True';
        }
        $(""#"" + id + "" .fav-button"").html(""Processing"").load(`/User/FavUnfav/${id}/${value}`);
        $(""#"" + id + "" .fav-button"").toggleClass(""blocked"");
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FavouriteProsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
