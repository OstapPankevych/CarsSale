using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.WebUi.Models;

namespace CarsSale.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginViewModel user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!_userService.IsUserValid(user.Login, user.Password))
            {
                ModelState.AddModelError("", "Wrong Login or Password!");
                return View("Login");
            }

            var dbUser = _userService.Get(user.Login);
            AddOuthCookies(dbUser, user.Remember);

            var url = !string.IsNullOrEmpty(returnUrl)
                ? returnUrl
                : FormsAuthentication.DefaultUrl;
            return Redirect(url);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }

        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitRegistry(AccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _userService.CreateUser(new User
            {
                Birthday = account.Birthday,
                Phone = account.Phone,
                Password = account.Password,
                Name = account.Name,
                Login = account.Login,
                Email = account.Email
            });

            return View("~/Views/Home/Index");
        }

        #region Helpers

        private void AddOuthCookies(User user, bool remember)
        {
            FormsAuthentication.SetAuthCookie(user.Login, remember);

            var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Role.Name);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }

        #endregion
    }
}