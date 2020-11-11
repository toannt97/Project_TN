using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Contants;
using ShopWebApp.Models.DataModels;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        private List<Supplier> _suppliers;
        private List<Product> _products;
        private List<Category> _categories;
        //private User _user;
        public ProductController()
        {
            _suppliers = new List<Supplier>();
            _products = new List<Product>();
            _categories = new List<Category>();
        }
        public async Task<IActionResult> Index(int? supplierId, int pageNo = 0)
        {
            if (!supplierId.HasValue)
            {
                supplierId = 0;
            }
            var response = await _client.GetAsync(Constants.API_SUPPLIER);
            _suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result.ToList();
            response = await _client.GetAsync(Constants.API_PRODUCT);
            _products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result.ToList();
            response = await _client.GetAsync(Constants.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();

            foreach (var product in _products)
            {
                foreach (var supplier in _suppliers)
                {
                    if (supplier.Id == product.SupplierId)
                        product.SupplierName = supplier.Name;
                }
            }

            var productResult = _products.Where(p => (supplierId == 0) || (p.SupplierId == supplierId))
                .Skip(pageNo * Program.PAGE_SIZE)
                .Take(Program.PAGE_SIZE);
            var totals = productResult.Count();
            ViewBag.TotalPage = Math.Ceiling(1.0 * totals / Program.PAGE_SIZE);
            ViewBag.PageNumber = pageNo;
            ViewBag.Suppliers = _suppliers.ToList();
            ViewBag.Category = _categories;
            ViewBag.domainUrl = Program.domainUrl;
            //ViewBag.UserLogin = _user;
            return View(productResult);
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await _client.GetAsync(Constants.API_SUPPLIER);
            _suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result.ToList();

            response = await _client.GetAsync(Constants.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();

            response = await _client.GetAsync($"{Constants.API_PRODUCT}/{id}");
            var product =  response.Content.ReadAsAsync<Product>().Result;
            
            foreach (var item in _suppliers)
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
                foreach (var brand in _suppliers)
                {
                    if (brand.Id == itemProduct.SupplierId)
                        itemProduct.SupplierName = brand.Name;
                }
            }
            var productsRelated = products.Where(p => p.SupplierId == product.SupplierId).SingleOrDefault();
            ViewBag.productsRelated = productsRelated;
            ViewBag.Suppliers = _suppliers.ToList();
            ViewBag.Category = _categories;
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