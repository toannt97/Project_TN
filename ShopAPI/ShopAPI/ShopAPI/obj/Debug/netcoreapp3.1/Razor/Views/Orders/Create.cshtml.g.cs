#pragma checksum "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6222340a397c743119cfd0ffb19b1160556a7d3f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_Create), @"mvc.1.0.view", @"/Views/Orders/Create.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6222340a397c743119cfd0ffb19b1160556a7d3f", @"/Views/Orders/Create.cshtml")]
    public class Views_Orders_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShopAPI.Models.Order>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Create.cshtml"
  
    ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Create</h1>

<h4>Order</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Create"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""CustomerId"" class=""control-label""></label>
                <select asp-for=""CustomerId"" class =""form-control"" asp-items=""ViewBag.CustomerId""></select>
            </div>
            <div class=""form-group"">
                <label asp-for=""UpdateDate"" class=""control-label""></label>
                <input asp-for=""UpdateDate"" class=""form-control"" />
                <span asp-validation-for=""UpdateDate"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""DeliveryDate"" class=""control-label""></label>
                <input asp-for=""DeliveryDate"" class=""form-control"" />
                <span asp-validation-for=""DeliveryDate"" class=""text-danger""></span>
            </d");
            WriteLiteral(@"iv>
            <div class=""form-group"">
                <label asp-for=""Status"" class=""control-label""></label>
                <input asp-for=""Status"" class=""form-control"" />
                <span asp-validation-for=""Status"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Create"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 46 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Create.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShopAPI.Models.Order> Html { get; private set; }
    }
}
#pragma warning restore 1591
