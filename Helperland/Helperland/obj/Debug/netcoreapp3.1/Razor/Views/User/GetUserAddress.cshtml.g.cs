#pragma checksum "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7cf9f8428e56344fc144e2c84d20f0a37aa3c411"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_GetUserAddress), @"mvc.1.0.view", @"/Views/User/GetUserAddress.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cf9f8428e56344fc144e2c84d20f0a37aa3c411", @"/Views/User/GetUserAddress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96bb1ef7955c1397a49dbc9396c03a07262c68b9", @"/Views/_ViewImports.cshtml")]
    public class Views_User_GetUserAddress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UserAddress>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
  
    Layout = null;
    int i = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 8 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
 foreach (UserAddress address in Model)
{
    string AddId = "add" + i;

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"d-flex align-items-center my-3\">\r\n        <input type=\"radio\" name=\"AddressId\" class=\"addId\"");
            BeginWriteAttribute("value", " value=\"", 263, "\"", 289, 1);
#nullable restore
#line 12 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
WriteAttributeValue("", 271, address.AddressId, 271, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 290, "\"", 301, 1);
#nullable restore
#line 12 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
WriteAttributeValue("", 295, AddId, 295, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n        <div class=\"mx-3\">\r\n            <label");
            BeginWriteAttribute("for", " for=\"", 353, "\"", 365, 1);
#nullable restore
#line 14 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
WriteAttributeValue("", 359, AddId, 359, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <div>\r\n                    <span class=\"small-title fs-6\">Address: </span> ");
#nullable restore
#line 16 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
                                                               Write(address.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 16 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
                                                                                     Write(address.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 16 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
                                                                                                            Write(address.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 16 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
                                                                                                                          Write(address.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div>\r\n                    <span class=\"small-title fs-6\">Phone number: </span> ");
#nullable restore
#line 19 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
                                                                    Write(address.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </label>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 24 "D:\Tatvasoft\Helperland\Helperland\Views\User\GetUserAddress.cshtml"
    i++;
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UserAddress>> Html { get; private set; }
    }
}
#pragma warning restore 1591
