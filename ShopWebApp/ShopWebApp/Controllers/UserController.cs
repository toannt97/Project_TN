using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWebApp.Common;
using ShopWebApp.Models.DataModels;
using ShopWebApp.Models.DTO;
using ShopWebApp.Models.Tool;

namespace ShopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        // GET: UserController/GetCurrentUser
        public ActionResult GetCurrentUser()
        {
            var user = HttpContext.Session.Get("_user");
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignIn user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_LoginView", user);
                var response = await _client.PostAsJsonAsync(Contants.Constants.API_USER, user);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Json(new { statusCode = response.StatusCode, messageError=""});
                }
                var result = response.Content.ReadAsAsync<UserDTO>().Result;
                
                HttpContext.Session.Set("_user", result);
                return Json(new { userName = result.UserName, statusCode = response.StatusCode});

            }catch(Exception ex)
            {
                return View("Error");
            } 
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
