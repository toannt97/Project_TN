#pragma checksum "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab1fa0396ee91835f36a2e0ffbedff7214a1ae3c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_Index), @"mvc.1.0.view", @"/Views/Orders/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab1fa0396ee91835f36a2e0ffbedff7214a1ae3c", @"/Views/Orders/Index.cshtml")]
    public class Views_Orders_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ShopAPI.Models.Order>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<p>\r\n    <a asp-action=\"Create\">Create New</a>\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CreateDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.UpdateDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DeliveryDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 28 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Customer));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 34 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.CreateDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.UpdateDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.DeliveryDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 46 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 49 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Customer.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1389, "\"", 1412, 1);
#nullable restore
#line 52 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
WriteAttributeValue("", 1404, item.Id, 1404, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a> |\r\n                <a asp-action=\"Details\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1465, "\"", 1488, 1);
#nullable restore
#line 53 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
WriteAttributeValue("", 1480, item.Id, 1480, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a> |\r\n                <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1543, "\"", 1566, 1);
#nullable restore
#line 54 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
WriteAttributeValue("", 1558, item.Id, 1558, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Delete</a>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 57 "E:\Project_TN_ThayTru\Projec_TN\ShopAPI\ShopAPI\ShopAPI\Views\Orders\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ShopAPI.Models.Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
