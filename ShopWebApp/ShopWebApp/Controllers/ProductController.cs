using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Models;
using ShopWebApp.Models.DataModels;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;

        public async Task<IActionResult> Index(int? supplierId, int pageNo = 0)
        {
            if (!supplierId.HasValue)
            {
                supplierId = 0;
            }

            var response = await _client.GetAsync("suppliers");
            var suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result;
            response = await _client.GetAsync("products");
            var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            response = await _client.GetAsync("categories");
            var category = response.Content.ReadAsAsync<IEnumerable<Category>>().Result; 

            foreach (var product in products)
            {
                foreach (var supplier in suppliers)
                {
                    if (supplier.Id == product.SupplierId)
                        product.SupplierName = supplier.Name;
                }
            }
            var productResult = products.Where(p => (supplierId == 0) || (p.SupplierId == supplierId))
                .Skip(pageNo * Program.PAGE_SIZE)
                .Take(Program.PAGE_SIZE);
            var totals = productResult.Count();
            ViewBag.TotalPage = Math.Ceiling(1.0 * totals / Program.PAGE_SIZE);
            ViewBag.PageNumber = pageNo;
            ViewBag.Suppliers = suppliers.ToList();
            ViewBag.SupplierId = supplierId;
            ViewBag.Category = category;
            ViewBag.domainUrl = Program.domainUrl;
            return View(productResult);
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await _client.GetAsync("suppliers");
            var brands = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result;
            string api_str = "Products" + "/" + id;
            response = await _client.GetAsync(api_str);
            var product = response.Content.ReadAsAsync<Product>().Result;
            foreach (var item in brands)
            {
                if (item.Id == product.SupplierId)
                    product.SupplierName = item.Name;
            }
            if (product == null)
            {
                return NotFound();
            }
            response = await _client.GetAsync("Products");
            var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var itemProduct in products)
            {
                foreach (var brand in brands)
                {
                    if (brand.Id == itemProduct.SupplierId)
                        itemProduct.SupplierName = brand.Name;
                }
            }
            List<Product> result = products.Where(p => p.SupplierId == product.SupplierId).Take(3).ToList();
            ViewBag.DSHangNgauNhien = result;
            List<Supplier> listBrand = brands.ToList();
            ViewBag.Hangs = listBrand;
            ViewBag.domainUrl = Program.domainUrl;
            return View("Detail",product);
        }
        //public IActionResult Search(string Keyword)
        //{
        //    // HTTP GET
        //    HttpClient client = new HttpClient();
        //    //client.BaseAddress = new Uri(Program.localhost);
        //    HttpResponseMessage response = client.GetAsync("api/TechnologyFirms").Result;
        //    var hangs = response.Content.ReadAsAsync<IEnumerable<Hang>>().Result;
        //    response = client.GetAsync("api/Products").Result;
        //    var sanPhams = response.Content.ReadAsAsync<IEnumerable<SanPham>>().Result;
        //    foreach (var item_Sanpham in sanPhams)
        //    {
        //        foreach (var item_Hang in hangs)
        //        {
        //            if (item_Hang.Id == item_Sanpham.FirmId)
        //                item_Sanpham.FirmName = item_Hang.FirmName;
        //        }
        //    }
        //    if (Keyword == null)
        //        Keyword = "";
        //    var dsSanPham = sanPhams.Where(p => p.product.Contains(Keyword)).Take(5).ToList();
        //    //ViewBag.domainUrl = Program.domainUrl;
        //    return PartialView("SearchPartial", dsSanPham);
        //}
    }
}