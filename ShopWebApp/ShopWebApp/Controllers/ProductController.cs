using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Contants;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        private List<Supplier> _suppliers;
        private List<Category> _categories;
        public ProductController()
        {
            _suppliers = new List<Supplier>();
            _products = new List<Product>();
            _categories = new List<Category>();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="pageNo">Current page</param>
        /// <returns>All products</returns>
        public async Task<IActionResult> Index(int pageNo = 0)
        {
            try
            {
                await Load();

                var response = await _client.GetAsync($"{Contant.API_PRODUCT}/{pageNo} /{Contant.PAGE_SIZE}");
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result.ToList();

                ViewBag.TotalPage = Math.Ceiling(1.0 * products[0].TotalPage / Contant.PAGE_SIZE);
                ViewBag.PageNumber = pageNo;
                ViewBag.Suppliers = _suppliers.ToList();
                ViewBag.Category = _categories;
                ViewBag.User = HttpContext.Session.Get<UserDTO>("_user");

                return View(products);
            }
            catch (SqlException)
            { 
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            //await Load();

            //var response = await _client.GetAsync($"{Contant.API_PRODUCT}/{id}");
            //var product = response.Content.ReadAsAsync<Product>().Result;

            //if (product == null)
            //{
            //    return NotFound();
            //}

            //foreach (var item in _suppliers)
            //{
            //    if (item.Id == product.SupplierId)
            //        product.SupplierName = item.Name;
            //}

            //response = await _client.GetAsync("Products");
            //var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //foreach (var itemProduct in products)
            //{
            //    foreach (var brand in _suppliers)
            //    {
            //        if (brand.Id == itemProduct.SupplierId)
            //            itemProduct.SupplierName = brand.Name;
            //    }
            //}

            //var productsRelated = products.Where(p => p.SupplierId == product.SupplierId && p.Id != id).ToList();

            //ViewBag.productsRelated = productsRelated;
            //ViewBag.Suppliers = _suppliers.ToList();
            //ViewBag.Category = _categories;
            //return View("Detail", product);
            return View("Detail", null);
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

        private async Task Load()
        {
            var response = await _client.GetAsync(Contant.API_SUPPLIER);
            _suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result.ToList();
             response = await _client.GetAsync(Contant.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();
        }
    }
}