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
using ShopWebApp.Models;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;

namespace ShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        #region Private fields
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        private List<Supplier> _suppliers;
        private List<Category> _categories;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductController()
        {
            _suppliers = new List<Supplier>();
            _categories = new List<Category>();
        }
        #endregion

        #region Public Method
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
                

                return View(products);
            }
            catch (SqlException)
            { 
                return View("Error", new ErrorViewModel {ErrorId=Contant.ERROR_CODE_SQL_CONNECTION, ErrorMessage=Contant.SQL_CONNECTION_MESSAGE});
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { ErrorId = Contant.ERROR_CODE_INTERNAL, ErrorMessage = Contant.INTERNAL_MESSAGE });
            }
        }

        public async Task<IActionResult> GetDetail(int id)
        {
            await Load();

            var response = await _client.GetAsync($"{Contant.API_PRODUCT}/{id}");
            var product = response.Content.ReadAsAsync<Product>().Result;

            if (product != null)
            {
                response = await _client.GetAsync($"{Contant.API_GET_PRODUCTS_RELATED}/{product.Id}/{product.SupplierID}/{Contant.DETAILED_PRODUCT_QUANTITY}");
                if((Int32)response.StatusCode == Contant.CODE_OK)
                {
                    var productsRelated = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    ViewBag.productsRelated = productsRelated;
                }
            }
            
            return View("Detail", product);
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
        #endregion

        #region Private Method
        /// <summary>
        /// Get necessary datas and pass to view: Navigation menu, Suppliers part, Tags part
        /// </summary>
        /// <returns></returns>
        private async Task Load()
        {
            var response = await _client.GetAsync(Contant.API_SUPPLIER);
            _suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result.ToList();
             response = await _client.GetAsync(Contant.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();
            ViewBag.Suppliers = _suppliers.ToList();
            ViewBag.Category = _categories;
            ViewBag.User = HttpContext.Session.Get<UserDTO>(Contant.SESSION_USER);
        }
        #endregion
    }
}