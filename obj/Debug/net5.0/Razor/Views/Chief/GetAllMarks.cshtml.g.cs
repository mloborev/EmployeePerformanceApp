#pragma checksum "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "396eaed44a3515d8baff99caf32ce384722b6b75"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Chief_GetAllMarks), @"mvc.1.0.view", @"/Views/Chief/GetAllMarks.cshtml")]
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
#line 1 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\_ViewImports.cshtml"
using EmployeePerformanceApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\_ViewImports.cshtml"
using EmployeePerformanceApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"396eaed44a3515d8baff99caf32ce384722b6b75", @"/Views/Chief/GetAllMarks.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b63e252036820faa1301c80e6c794133978c1257", @"/Views/_ViewImports.cshtml")]
    public class Views_Chief_GetAllMarks : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GetAllMarksViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "396eaed44a3515d8baff99caf32ce384722b6b753493", async() => {
                WriteLiteral("Home");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<table class=\"table\">\r\n    <tr>\r\n        <td>Parameter</td>\r\n        <td>Mark Value</td>\r\n        <td>Mark Description</td>\r\n        <td>User</td>\r\n        <td>Assessor</td>\r\n        <td>Assessment Date</td>\r\n    </tr>\r\n");
#nullable restore
#line 14 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
         foreach (Mark mark in Model.Marks)
        {
            if (mark.User.DepartmentId != Model.CurrentUserDepartmentId)
                continue;

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 19 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.Parameter.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 20 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.MarkValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 21 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.MarkDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 22 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.User.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 22 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
                                  Write(mark.User.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 23 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.AssessorSurname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 23 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
                                     Write(mark.AssessorName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
               Write(mark.AssesmentDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 26 "C:\Users\maxim\source\repos\EmployeePerformanceApp\EmployeePerformanceApp\Views\Chief\GetAllMarks.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GetAllMarksViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
