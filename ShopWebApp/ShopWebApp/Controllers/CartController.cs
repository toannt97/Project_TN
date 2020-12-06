using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Constants;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;
using ShopWebApp.Models.ViewModels;
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
        public CartController() {
        }

        // GET: CartController
        public async Task<ActionResult> Index()
        {
            await Load();
            if (HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER) != null)
            {
                var idUserCurrent = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER).Id ;
                var response = await _client.GetAsync($"{Constant.API_CART}/{idUserCurrent}");
                var cartItems = response.Content.ReadAsAsync<IEnumerable<CartItemViewModel>>().Result.ToList();
                return View("Index", cartItems);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Product");
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
