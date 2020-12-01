using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingWebAPI.Common;
using ShopWebApp.Common;
using ShopWebApp.Constants;
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
            var user = HttpContext.Session.Get(Constant.SESSION_USER);
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignIn user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_LoginView", user);
                
                user.Password = Encrypt.SHA256Hash(user.Password);
                var response = await _client.PostAsJsonAsync(Constant.API_USER, user);
                
                if ((Int32)response.StatusCode == Constant.ERROR_CODE_NOT_FOUND)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError=Constant.NOT_FOUND_MESSAGE});
                }

                var result = response.Content.ReadAsAsync<UserDTO>().Result;
                
                HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, result);
                return Json(new { userName = result.UserName, itemsInCart = result.ItemsInCart, statusCode = Constant.CODE_OK});

            }catch(Exception)
            {
                return View("Error");
            } 
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_RegisterView", user);
                user.Password = Encrypt.SHA256Hash(user.Password);
                user.PhoneNumber = Constant.PREFIX_PHONE + user.PhoneNumber;
                var response = await _client.PostAsJsonAsync(Constant.API_ADD_USER, user);

                if ((Int32)response.StatusCode == Constant.ERROR_CODE_DUPLICATE_DATA)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_DUPLICATE_DATA, messageError = Constant.DUPLICATE_DATA_MESSAGE });
                }

                return Json(new { statusCode = Constant.CODE_OK });
            }
            catch (Exception)
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
