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

        [HttpPost]
        public async Task<ActionResult> CheckOutCart()
        {
            var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
            if (sessionUser == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Product");
            }

            var response = await _client.GetAsync($"{Constant.API_CART_CHECK_OUT}/{sessionUser.Id}");
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.ADD_ERROR_MESSAGE });
                    }
                case Constant.CODE_OK:
                    {
                        var result = response.Content.ReadAsAsync<IEnumerable<CartItemInvalidDTO>>().Result;
                        HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, sessionUser);
                        return Json(new { statusCode = Constant.ERROR_CODE_CART_ITEM_INVALID, messageError = Constant.CART_ITEM_INVALID_MESSAGE, data = result });
                    }
                case Constant.EMPTY_LIST:
                    {
                        return Json(new { statusCode = Constant.CODE_OK, messageError = Constant.OK_MESSAGE });
                    }
                case Constant.USER_NOT_EXIST:
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Product");
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem([FromBody] CartItemDTO cartItem)
        {
            try
            {
                var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
                if (sessionUser == null)
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Product");
                }
                cartItem.UserId = sessionUser.Id;
                var response = await _client.PutAsJsonAsync(Constant.API_CART, cartItem);
                switch ((Int32)response.StatusCode)
                {
                    case Constant.ERROR_CODE_NOT_FOUND:
                        {
                            return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.REQUEST_RELOAD_MESSAGE });
                        }
                    case Constant.CODE_OK:
                        {
                            var result = response.Content.ReadAsAsync<IEnumerable<CartItemViewModel>>().Result.ToList();
                            var quantity = result.Sum(s => s.Quantity);
                            sessionUser.ItemsInCart = quantity;
                            HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, sessionUser);
                            return View("Index", result);
                        }
                    default:
                        {
                            return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                        }
                }
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
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
