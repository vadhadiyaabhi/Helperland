#pragma checksum "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d96f1480b77a63849683a2965ce86f988db81afe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SP_GetServiceDetails), @"mvc.1.0.view", @"/Views/SP/GetServiceDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d96f1480b77a63849683a2965ce86f988db81afe", @"/Views/SP/GetServiceDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96bb1ef7955c1397a49dbc9396c03a07262c68b9", @"/Views/_ViewImports.cshtml")]
    public class Views_SP_GetServiceDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ServiceRequest>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/not-included.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("me-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/included.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/forma-1-copy-10@2x.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("16"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("position:relative"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "SP", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AcceptService", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return jQueryAjaxPost(this);"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("12px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-inline service-complete-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CompleteServiceRequest", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/close-icon-small.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("<div>\r\n    <div class=\"fs-5 fw-bold\">");
#nullable restore
#line 15 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                         Write(String.Format("{0:dd/MM/yyyy}", Model.ServiceStartDate));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 15 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                   Write(Convert.ToString(Convert.ToDateTime(Model.ServiceStartDate).TimeOfDay).Substring(0,5));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 15 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                                                                                                              Write(Convert.ToString(Convert.ToDateTime(Model.ServiceStartDate.AddHours(Convert.ToDouble(Model.SubTotal))).TimeOfDay).Substring(0,5));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    <div><span class=\"small-title fs-6\">Duration: </span> ");
#nullable restore
#line 16 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                     Write(Model.SubTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Hrs</div>\r\n    <hr />\r\n    <div><span class=\"small-title fs-6\">Service Id: </span>");
#nullable restore
#line 18 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                      Write(Model.ServiceRequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    <div>\r\n        <span class=\"small-title fs-6\">Extra Services: </span>\r\n");
#nullable restore
#line 21 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
         foreach (ServiceRequestExtra extra in Model.ServiceRequestExtras)
        {

            

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
             if (extra.ServiceExtraId == 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>Inside cabinets,</span>\r\n");
#nullable restore
#line 27 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
            }
            else if (extra.ServiceExtraId == 2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>Inside fridge,</span>\r\n");
#nullable restore
#line 31 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
            }
            else if (extra.ServiceExtraId == 3)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>Inside oven,</span>\r\n");
#nullable restore
#line 35 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
            }
            else if (extra.ServiceExtraId == 4)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>\r\n                    Laundry wash & dry,\r\n                </span>\r\n");
#nullable restore
#line 41 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
            }
            else if (extra.ServiceExtraId == 5)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span>\r\n                    Interio window\r\n                </span>\r\n");
#nullable restore
#line 47 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
             


        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <div><span class=\"small-title fs-6\">Net Amount: </span><span class=\"fs-5 fw-bold\" style=\"color: #1d7a8c;\">");
#nullable restore
#line 52 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                                         Write(Model.TotalCost);

#line default
#line hidden
#nullable disable
            WriteLiteral(" € </span></div>\r\n\r\n    <hr />\r\n\r\n");
#nullable restore
#line 56 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
     foreach (ServiceRequestAddress serviceAddress in Model.ServiceRequestAddresses)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div><span class=\"small-title\">Service Address: </span> ");
#nullable restore
#line 58 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                           Write(serviceAddress.AddressLine1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 58 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                        Write(serviceAddress.AddressLine2);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 58 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                                                      Write(serviceAddress.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 58 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                                                                                                                 Write(serviceAddress.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 59 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div><span class=\"small-title fs-6\">Mobile: </span> +91 ");
#nullable restore
#line 60 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                       Write(Model.User.Mobile);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    <div><span class=\"small-title fs-6\">Email: </span> ");
#nullable restore
#line 61 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
                                                  Write(Model.User.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n    <hr />\r\n\r\n    <div><span class=\"small-title fs-6\">Comments: </span></div>\r\n");
#nullable restore
#line 66 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
     if (Model.HasPets)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d96f1480b77a63849683a2965ce86f988db81afe15709", async() => {
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
            WriteLiteral("\r\n        <span>I don\'t have pets at home</span>\r\n");
#nullable restore
#line 70 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d96f1480b77a63849683a2965ce86f988db81afe17087", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <span>I have pets at home</span>\r\n");
#nullable restore
#line 75 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <hr />\r\n");
#nullable restore
#line 78 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
     if (Model.Status == 1 || Model.Status == 5)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d96f1480b77a63849683a2965ce86f988db81afe18697", async() => {
                WriteLiteral("\r\n            <input type=\"hidden\" name=\"ServiceId\"");
                BeginWriteAttribute("value", " value=\"", 2876, "\"", 2907, 1);
#nullable restore
#line 81 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
WriteAttributeValue("", 2884, Model.ServiceRequestId, 2884, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            <button type=\"submit\" class=\"export-button py-1 px-3 fw-normal\" style=\"background-color: #3bc001; width:auto\">");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d96f1480b77a63849683a2965ce86f988db81afe19521", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" Accept</button>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 84 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 86 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
     if (Model.Status == 2)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 88 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
         if (Model.ServiceStartDate.AddHours(Convert.ToDouble(Model.SubTotal)) < DateTime.Now)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d96f1480b77a63849683a2965ce86f988db81afe22954", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\" name=\"ServiceId\"");
                BeginWriteAttribute("value", " value=\"", 3513, "\"", 3544, 1);
#nullable restore
#line 91 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
WriteAttributeValue("", 3521, Model.ServiceRequestId, 3521, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                <span type=\"submit\" class=\"completed export-button\" style=\"width:auto;\">");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d96f1480b77a63849683a2965ce86f988db81afe23748", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" Complete</span>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 94 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <span class=\"cancle-button d-inline-block\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3825, "\"", 3891, 6);
            WriteAttributeValue("", 3835, "CancleBySp(", 3835, 11, true);
#nullable restore
#line 96 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
WriteAttributeValue("", 3846, Model.ServiceRequestId, 3846, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3869, ",", 3869, 1, true);
            WriteAttributeValue(" ", 3870, "\'", 3871, 2, true);
#nullable restore
#line 96 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
WriteAttributeValue("", 3872, Model.User.Email, 3872, 17, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3889, "\')", 3889, 2, true);
            EndWriteAttribute();
            WriteLiteral(">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d96f1480b77a63849683a2965ce86f988db81afe27685", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" Cancle</span>\r\n");
#nullable restore
#line 97 "D:\Tatvasoft\Helperland\Helperland\Views\SP\GetServiceDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</div>\r\n\r\n<script type=\"text/javascript\">\r\n    $(\'.completed\').click(function () {\r\n        $(\'.service-complete-form\').submit();\r\n    })\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServiceRequest> Html { get; private set; }
    }
}
#pragma warning restore 1591