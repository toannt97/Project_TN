using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Constants;
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
        private double _totalPage = 0;
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

        #region Public Methods
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

                var response = await _client.GetAsync($"{Constant.API_PRODUCT}/{pageNo} /{Constant.PAGE_SIZE}");
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result.ToList();
                await GetTotalPage();
                ViewBag.TotalPage = _totalPage;
                ViewBag.PageNumber = pageNo;

                return View(products);
            }
            catch (SqlException)
            {
                return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_SQL_CONNECTION, ErrorMessage = Constant.SQL_CONNECTION_MESSAGE });
            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_INTERNAL, ErrorMessage = Constant.INTERNAL_MESSAGE });
            }
        }

        public async Task<IActionResult> GetDetail(int? productId, int pageNo = 0, int categoryId = 0, int supplierId = 0)
        {
            await Load();
            try {
                if (productId != null) {
                    var response = await _client.GetAsync($"{Constant.API_PRODUCT}/{productId}");
                    switch ((Int32)response.StatusCode)
                    {
                        case Constant.ERROR_CODE_NOT_FOUND:
                            return View("Detail", null);
                        case Constant.ERROR_CODE_INTERNAL:
                            return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_INTERNAL, ErrorMessage = Constant.INTERNAL_MESSAGE });
                        default :
                            {
                                var product = response.Content.ReadAsAsync<Product>().Result;
                                if (product != null)
                                {
                                    var productsRelated = await GetRelatedProducts(product);
                                    ViewBag.productsRelated = productsRelated;
                                }
                                return View("Detail", product);
                            }
                    }
                }
                else {
                    var response = await _client.GetAsync($"{Constant.API_PRODUCT}/{supplierId}/{categoryId}/{pageNo}/{Constant.PAGE_SIZE}");
                    ViewBag.categoryCurrentId = categoryId;
                    ViewBag.supplierCurrenId = supplierId;
                    switch ((Int32)response.StatusCode)
                    {
                        case Constant.ERROR_CODE_NOT_FOUND:
                            return View("DetailWithCondition", null);
                        case Constant.ERROR_CODE_INTERNAL:
                            return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_INTERNAL, ErrorMessage = Constant.INTERNAL_MESSAGE });
                        default:
                            {
                                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result.ToList();
                                await GetTotalPage(categoryId, supplierId);
                                ViewBag.TotalPage = _totalPage;
                                ViewBag.PageNumber = pageNo;
                                return View("DetailWithCondition", products);
                            }
                    }
                }
            } catch (SqlException) {
                return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_SQL_CONNECTION, ErrorMessage = Constant.SQL_CONNECTION_MESSAGE });
            }
            catch (Exception) {
                return View("Error", new ErrorViewModel { ErrorId = Constant.ERROR_CODE_INTERNAL, ErrorMessage = Constant.INTERNAL_MESSAGE });
            }
        }

        public IActionResult Search(string Keyword)
        {
            // HTTP GET
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(Program.localhost);
            //HttpResponseMessage response = client.GetAsync("api/TechnologyFirms").Result;
            //var hangs = response.Content.ReadAsAsync<IEnumerable<Hang>>().Result;
            //response = client.GetAsync("api/Products").Result;
            //var sanPhams = response.Content.ReadAsAsync<IEnumerable<SanPham>>().Result;
            //foreach (var item_Sanpham in sanPhams)
            //{
            //    foreach (var item_Hang in hangs)
            //    {
            //        if (item_Hang.Id == item_Sanpham.FirmId)
            //            item_Sanpham.FirmName = item_Hang.FirmName;
            //    }
            //}
            //if (Keyword == null)
            //    Keyword = "";
            //var dsSanPham = sanPhams.Where(p => p.product.Contains(Keyword)).Take(5).ToList();
            //ViewBag.domainUrl = Program.domainUrl;
            return PartialView("SearchPartial", null);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get necessary datas and pass to view: Navigation menu, Suppliers part, Tags part
        /// </summary>
        /// <returns></returns>
        private async Task Load()
        {
            var response = await _client.GetAsync(Constant.API_SUPPLIER);
            _suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result.ToList();
            response = await _client.GetAsync(Constant.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();
            ViewBag.Suppliers = _suppliers.ToList();
            ViewBag.Category = _categories;
            ViewBag.User = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
        }

        private async Task GetTotalPage(int categoryId = 0, int supllierId = 0)
        {
                var response = await _client.GetAsync($"{ Constant.API_TOTAL_PRODUCT}/{categoryId}/{supllierId}");
                var totalRecords = response.Content.ReadAsAsync<int>().Result;
                _totalPage =  Math.Ceiling(1.0 * totalRecords / Constant.PAGE_SIZE);
            
        }

        private async Task<IEnumerable<Product>> GetRelatedProducts(Product product)
        {
            var response = await _client.GetAsync($"{Constant.API_GET_PRODUCTS_RELATED}/{product.Id}/{product.SupplierID}/{product.CategoryID}/{Constant.DETAILED_PRODUCT_QUANTITY}");
            if ((Int32)response.StatusCode == Constant.CODE_OK)
            {
                var productsRelated = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                return productsRelated;
            }
            return new List<Product>();
        }
        #endregion
    }
}