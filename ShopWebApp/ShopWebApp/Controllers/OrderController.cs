using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Constants;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;
using System;
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
            if(sessionUser == null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Product");
            }
            
            var response = await _client.GetAsync($"{ Constant.API_GET_ODER_BY_USER }/{sessionUser.Id}");
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return View("Index", null);
                    }
                case Constant.CODE_OK:
                    {
                        var result = response.Content.ReadAsAsync<Order>().Result;

                        return View("Index", null);
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }

        }

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
