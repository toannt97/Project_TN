using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Constants;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;
using ShopWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        private List<Category> _categories;
        public CartController()
        {
        }

        // GET: CartController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await Load();
            if (HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER) != null)
            {
                var idUserCurrent = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER).Id;
                var response = await _client.GetAsync($"{Constant.API_CART}/{idUserCurrent}");
                var cartItems = response.Content.ReadAsAsync<IEnumerable<CartItemViewModel>>().Result.ToList();
                return View("Index", cartItems);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Product");
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] ProductCartDTO product)
        {
            var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
            if (sessionUser == null)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_AUTHENTICATION, messageError = Constant.AUTHENTICATION_MESSAGE });
            }
            product.UserId = sessionUser.Id;
            var response = await _client.PostAsJsonAsync(Constant.API_CART, product);

            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.ADD_ERROR_MESSAGE });
                    }
                case Constant.CODE_OK:
                    {
                        sessionUser.ItemsInCart += 1;
                        HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, sessionUser);
                        return Json(new { statusCode = Constant.CODE_OK, messageError = Constant.OK_MESSAGE });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
        }

        // GET: CartController/GetNumOfCartItems
        [HttpGet]
        public async Task<ActionResult> GetNumOfCartItems()
        {
            var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
            if (sessionUser == null)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_AUTHENTICATION, messageError = Constant.AUTHENTICATION_MESSAGE });
            }
            
            var response = await _client.GetAsync($"{Constant.API_GET_NUM_OF_CART_ITEMS}/{sessionUser.Id}");
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.ADD_ERROR_MESSAGE });
                    }   
                case Constant.CODE_OK:
                    {
                        var result = response.Content.ReadAsAsync<NumOfCartItems>().Result;
                        sessionUser.ItemsInCart = result.Quantity;
                        HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, sessionUser);
                        return Json(new { statusCode = Constant.CODE_OK, messageError = Constant.OK_MESSAGE, data = result.Quantity });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
        }
        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task Load()
        {
            var response = await _client.GetAsync(Constant.API_CATEGORY);
            _categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result.ToList();
            ViewBag.Category = _categories;
            ViewBag.User = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
        }
    }
}
