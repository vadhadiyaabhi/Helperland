#pragma checksum "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eeebeafdf780e2ee8930ebfef228dfa258b3b2ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Register_email_confirm), @"mvc.1.0.view", @"/Views/Register/email-confirm.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eeebeafdf780e2ee8930ebfef228dfa258b3b2ac", @"/Views/Register/email-confirm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96bb1ef7955c1397a49dbc9396c03a07262c68b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Register_email_confirm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmailConfirm>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-block mx-auto"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ResendConfirmEmail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
  
    Layout = "../Shared/_EmptyLayout.cshtml";
    ViewBag.Title = "Helperland - Email Confirmation";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!--<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Helperland - Email Confirmation</title>-->

    <!-- Link for fav icon-->
    <!--<link href=""~/images/favicon_img.png"" rel=""shortcut icon"" />-->

    <!-- bootstrap css bundle -->
    <!--<link href=""~/lib/twitter-bootstrap/css/bootstrap.min.css"" rel=""stylesheet"" />-->
    <!-- custom CSS -->
    <!--<link href=""~/css/site2.css"" rel=""stylesheet"" />
    <link href=""~/css/common.css"" rel=""stylesheet"" />
    <link href=""~/css/Modal.css"" rel=""stylesheet"" />
</head>

<body>-->
    <div");
            BeginWriteAttribute("class", " class=\"", 854, "\"", 862, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n        <div class=\"container\" style=\"max-width: 550px; padding-top: 80px;\">\r\n");
#nullable restore
#line 29 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
             if (Model.EmailConfirmed == true)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""alert-success"">
                    Your email has been verified successfully, <br />
                    And your account has been activated <br />
                    Click here to <a class=""redirect-to-login"">LogIn</a>
                </div>
");
#nullable restore
#line 37 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
            }
            else if (Model.userNotExist != true)
            {
                if (Model.Invalid == true)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""alert-danger"">
                        Oops!! Your link is invalid Or get expired<br />
                        Click on below button to get the link again. <br />
                        Please note that, <span class=""fw-bold"">Only latest email link will work.</span>
                    </div>
");
#nullable restore
#line 47 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
                }
                else if (Model.EmailSent == true)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""text-center alert-success"">Confirmation email has been sent to your registerd account.<br />
                        <span class=""fw-bold"">Please note that, Link will expire within 1 Hour</span>
                    </div>
                    <p class=""text-center"">Click on the link to activate your account by confirming your email address.</p>
");
#nullable restore
#line 54 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eeebeafdf780e2ee8930ebfef228dfa258b3b2ac8399", async() => {
                WriteLiteral("\r\n                    <div class=\"text-center\">Did not get any email or deleted by mistake ?</div>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "eeebeafdf780e2ee8930ebfef228dfa258b3b2ac8779", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 58 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Email);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    <button type=\"submit\" class=\"resend-button my-3 mx-auto\">Resend Confirmation Link</button>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 61 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"alert-danger\">\r\n                    Oops!! Your link is invalid Or get expired<br />\r\n                </div>\r\n");
#nullable restore
#line 67 "D:\Tatvasoft\Helperland\Helperland\Views\Register\email-confirm.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

        </div>
    </div>

    <!-- bootstrap bundle JS -->
    <!--<script src=""~/lib/twitter-bootstrap/js/bootstrap.bundle.min.js""></script>-->

    <!-- jquery cdnjs -->
    <!--<script src=""~/lib/jquery/jquery.min.js""></script>-->
    <!-- Custom Script file -->
    <!--<script src=""~/js/site.js"" asp-append-version=""true""></script>
</body>
</html>-->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmailConfirm> Html { get; private set; }
    }
}
#pragma warning restore 1591
