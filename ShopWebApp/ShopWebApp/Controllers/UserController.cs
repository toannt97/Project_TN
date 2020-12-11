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
            return Json(HttpContext.Session.Get<User>(Constant.SESSION_USER));
        }

        [HttpGet]
        public ActionResult SignInIndex()
        {
            try
            {
                return View(Constant.SIGN_IN, null);
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
                    return View(Constant.SIGN_IN, user);

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
                return View(Constant.ERROR);
            }
        }
        [HttpGet]
        public ActionResult SignUpIndex()
        {
            try
            {
                return View(Constant.SIGN_UP, null);
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
                    return PartialView(Constant.SIGN_UP, user);
                user.Password = Encrypt.SHA256Hash(user.Password);
                user.PhoneNumber = Constant.PREFIX_PHONE + user.PhoneNumber;
                var response = await _client.PostAsJsonAsync(Constant.API_ADD_USER, user);

                return SendResponseToUI(response);
            }
            catch (Exception)
            {
                return View(Constant.ERROR);
            }
        }

        [HttpGet]
        public ActionResult ChangePasswordIndex()
        {
            try
            {
                return View(Constant.CHANGE_PASSWORD, null);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordHandle([FromBody] UserChangePassword user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView(Constant.CHANGE_PASSWORD, user);

                if (HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER) == null)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_AUTHENTICATION, messageError = Constant.ERROR_CODE_AUTHENTICATION });
                }

                var userRequest = new
                {
                    Id = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER).Id,
                    CurrentPassword = Encrypt.SHA256Hash(user.CurrentPassword),
                    NewPassword = Encrypt.SHA256Hash(user.NewPassword),
                };

                var response = await _client.PostAsJsonAsync(Constant.API_CHANGE_PASSWORD, userRequest);

                return SendResponseToUI(response);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfileIndex()
        {
            try
            {
                var sessionUser = HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER);
                if (sessionUser == null)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_AUTHENTICATION, messageError = Constant.AUTHENTICATION_MESSAGE });
                }
                var response = await _client.GetAsync($"{Constant.API_USER}/{sessionUser.Id}");
                if((Int32)response.StatusCode == Constant.CODE_OK)
                {
                    var user = response.Content.ReadAsAsync<User>().Result;
                    var userResponse = new UserUpdateProfile
                    {
                        Address = user.Address,
                        Id = user.Id,
                        EmailAddress = user.EmailAddress,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName,
                    };
                    return View(Constant.UPDATE_PROFILE, userResponse);
                }

                return SendResponseToUI(response);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileHandle([FromBody] UserUpdateProfile user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(Constant.UPDATE_PROFILE, user);

                if (HttpContext.Session.Get<UserDTO>(Constant.SESSION_USER) == null)
                {
                    return Json(new { statusCode = Constant.ERROR_CODE_AUTHENTICATION, messageError = Constant.ERROR_CODE_AUTHENTICATION });
                }

                var response = await _client.PostAsJsonAsync(Constant.API_UPDATE_PROFILE, user);

                return SendResponseToUI(response);
            }
            catch (Exception)
            {
                return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, messageError = Constant.INTERNAL_MESSAGE });
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
                return View(Constant.RESET, null);
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
                return View(Constant.RESET, userRestore);
            }
            var response = await _client.PostAsJsonAsync(Constant.API_RESET_PASSWORD, userRestore);
            return SendResponseToUI(response);
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

        #region Private Methods
        private ActionResult SendResponseToUI(HttpResponseMessage response)
        {
            switch ((Int32)response.StatusCode)
            {
                case Constant.ERROR_CODE_BAD_REQUEST:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_BAD_REQUEST, errorMessage = Constant.BAD_REQUEST_MESSAGE });
                    }
                case Constant.ERROR_CODE_NOT_FOUND:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_NOT_FOUND, errorMessage = Constant.NOT_FOUND_MESSAGE });
                    }
                case Constant.ERROR_CODE_DUPLICATE_DATA_EMAIL:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_DUPLICATE_DATA_EMAIL, errorMessage = Constant.DUPLICATE_DATA_EMAIL_MESSAGE });
                    }
                case Constant.ERROR_CODE_DUPLICATE_DATA_USER_NAME:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_DUPLICATE_DATA_USER_NAME, errorMessage = Constant.DUPLICATE_DATA_USER_NAME_MESSAGE });
                    }
                case Constant.ERROR_CODE_SQL_CONNECTION:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_SQL_CONNECTION, errorMessage = Constant.SQL_CONNECTION_MESSAGE });
                    }
                case Constant.CODE_OK:
                    {
                        return Json(new { statusCode = Constant.CODE_OK });
                    }
                default:
                    {
                        return Json(new { statusCode = Constant.ERROR_CODE_INTERNAL, errorMessage = Constant.INTERNAL_MESSAGE });
                    }
            }
        }
        #endregion
    }
}
