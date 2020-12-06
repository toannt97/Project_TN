﻿using System;
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
using ShopWebApp.Models.ViewModels;

namespace ShopWebApp.Controllers
{
    public class UserController : Controller
    {
        #region Private fields
        private readonly HttpClient _client = HttpClientAccessor.HttpClient;
        #endregion

        #region Constructor
        #endregion

        #region Public methods
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

        [HttpGet]
        public ActionResult SignInIndex()
        {
            try
            {
                return View("SignIn", null);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignInHandle([FromBody] UserSignIn user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("SignIn", user);

                user.Password = Encrypt.SHA256Hash(user.Password);
                var response = await _client.PostAsJsonAsync(Constant.API_USER, user);

                if ((Int32)response.StatusCode == Constant.ERROR_CODE_NOT_FOUND)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.NOT_FOUND_MESSAGE });
                }

                var result = response.Content.ReadAsAsync<UserDTO>().Result;

                HttpContext.Session.Set<UserDTO>(Constant.SESSION_USER, result);
                return Json(new { userName = result.UserName, itemsInCart = result.ItemsInCart, statusCode = Constant.CODE_OK });

            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult SignUpIndex()
        {
            try
            {
                return View("SignUp", null);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUpHandle([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("SignUp", user);
                user.Password = Encrypt.SHA256Hash(user.Password);
                user.PhoneNumber = Constant.PREFIX_PHONE + user.PhoneNumber;
                var response = await _client.PostAsJsonAsync(Constant.API_ADD_USER, user);

                switch ((Int32)response.StatusCode)
                {
                    case Constant.ERROR_CODE_DUPLICATE_DATA_EMAIL:
                        {
                            return Json(new { statusCode = Constant.ERROR_CODE_DUPLICATE_DATA_EMAIL, messageError = Constant.DUPLICATE_DATA_EMAIL_MESSAGE });
                        }
                    case Constant.ERROR_CODE_DUPLICATE_DATA_USER_NAME:
                        {
                            return Json(new { statusCode = Constant.ERROR_CODE_DUPLICATE_DATA_USER_NAME, messageError = Constant.DUPLICATE_DATA_USER_NAME_MESSAGE });
                        }
                    case Constant.CODE_OK:
                        {
                            return Json(new { statusCode = Constant.CODE_OK });

                        }
                    default:
                        {
                            return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                        }

                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            try
            {
                HttpContext.Session.Remove(Constant.SESSION_USER);
                return Json(new { statusCode = Constant.CODE_OK });
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ResetPasswordIndex()
        {
            try
            {
                return View("Reset", null);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }

        [HttpPost]
        public async Task<ActionResult> ResetPasswordHandle([FromBody] UserRestore userRestore)
        {
            if (!ModelState.IsValid)
            {
                return View("Reset", userRestore);
            }
            var response = await _client.PostAsJsonAsync(Constant.API_RESET_PASSWORD, userRestore);
            switch ((Int32)response.StatusCode)
            {
                case Constant.CODE_OK:
                    {
                        return Json(new { statusCode = Constant.CODE_OK });
                    }
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, messageError = Constant.NOT_FOUND_MESSAGE });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
                    }
            }
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
        #endregion
    }
}
