#pragma checksum "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\Account\AccessDenied.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e500aa41b552c56b6c7e7b1de13e8db11a3a96c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AccessDenied), @"mvc.1.0.view", @"/Views/Account/AccessDenied.cshtml")]
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
#line 1 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\_ViewImports.cshtml"
using src;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\_ViewImports.cshtml"
using src.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\_ViewImports.cshtml"
using src.Models.AccountViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\_ViewImports.cshtml"
using src.Models.ManageViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e500aa41b552c56b6c7e7b1de13e8db11a3a96c", @"/Views/Account/AccessDenied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4121359f8267c0306bde2e30b028a04a2aa9e7a", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AccessDenied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\Account\AccessDenied.cshtml"
  
    ViewData["Title"] = "Access denied";
    Layout = "~/Views/Shared/_LayoutAdminlteBlank.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"register-box-body bg-blue\">\n    <h2 class=\"text-danger\">");
#nullable restore
#line 6 "C:\Users\DELL\source\repos\Settlement_Dispute_Portal_Latest\Settlement_Dispute_Portal\Settlement_Dispute_Portal\src\Views\Account\AccessDenied.cshtml"
                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n    <p class=\"text-danger\">You do not have access to this resource.</p>\n</div>\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
