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
    public class OrderController : Controller
    {
        #region Private Fields
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        #endregion
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
            if (sessionUser == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Product");
            }

            var response = await _client.GetAsync($"{Constant.API_GET_ODER_BY_USER}/{sessionUser.Id}");
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return View("Index", new List<Order>());
                    }
                case Constant.CODE_OK:
                    {
                        var result = response.Content.ReadAsAsync<List<Order>>().Result;
                        return View("Index", result);
                    }
                case Constant.ERROR_CODE_SQL_CONNECTION:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_SQL_CONNECTION, messageError = Constant.SQL_CONNECTION_MESSAGE });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
        }

        public async Task<ActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
            if (sessionUser == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Product");
            }
            var response = await _client.GetAsync($"{Constant.API_GET_ODER_DETAILS_BY_ORDER}/{orderId}/{sessionUser.Id}");
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return View("Index", new List<Order>());
                    }
                case Constant.CODE_OK:
                    {
                        var result = response.Content.ReadAsAsync<List<OrderDetailsDTO>>().Result;
                        return View("Payment", result);
                    }
                case Constant.ERROR_CODE_SQL_CONNECTION:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_SQL_CONNECTION, messageError = Constant.SQL_CONNECTION_MESSAGE });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
        }

        //public async Task<ActionResult> PaymentComplete(int orderId)
        //{
        //    var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
        //    var response = await _client.GetAsync($"{Constant.API_PAYMENT_COMPLETED}/{orderId}/{sessionUser.Id}");
        //}

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
    }
}
