using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity;
using CarsSale.DataAccess.Services;
using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.WebUi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CarsSale.WebUi.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserService _userService;

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        //public AccountController(IUserService userService)
        public AccountController(UserService userService)
        {
            //_userService = userService;
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

            //if (!_userService.IsUserValid(user.Login, user.Password))
            //{
            //    ModelState.AddModelError("", "Wrong Login or Password!");
            //    return View("Login");
            //}

            //var dbUser = _userService.Get(user.Login);
            //AddOuthCookies(dbUser, user.Remember);

            var dbUser = UserManager.Find(user.Login, user.Password);
            if (dbUser == null)
            {
                ModelState.AddModelError("", "Wrong Login or Password!");
                return View("Login");
            }

            SignIn(dbUser, user.Remember);

            var url = !string.IsNullOrEmpty(returnUrl)
                ? returnUrl
                : FormsAuthentication.DefaultUrl;

            return Redirect(url);
        }

        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            //return Redirect(FormsAuthentication.DefaultUrl);

            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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
                ModelState.AddModelError("", "The registered form is invalid! Please try again");
                return View("Registry");
            }

            //if (_userService.IsEmailExists(account.Email))
            if (UserManager.IsEmailExists(account.Email))
            {
                ModelState.AddModelError("Email", $"Email '{account.Email}' arleady registered");
                return View("Registry");
            }

            //if (_userService.IsLoginExists(account.Login))
            if (UserManager.IsLoginExists(account.Login))
            {
                ModelState.AddModelError("Email", $"Login '{account.Login}' arleady registered");
                return View("Registry");
            }

            //if (_userService.IsPhoneExists(FormatPhone(account.Phone)))
            if (UserManager.IsPhoneExists(FormatPhone(account.Phone)))
            {
                ModelState.AddModelError("Email", $"Phone '{account.Phone}' arleady registered");
                return View("Registry");
            }

            var user = new IdentityUser
            {
                UserName = account.Name,
                Logins =
                    {
                        new IdentityUserLogin
                        {
                            LoginProvider = "CarsSale", ProviderKey = account.Login
                        }
                    },
                Email = account.Email
            };
            var result = UserManager.Create(user, account.Password);
            if (result.Succeeded)
            {
                SignIn(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View("Registry");


            //_userService.CreateUser(new User
            //{
            //    Birthday = account.Birthday,
            //    Phone = FormatPhone(account.Phone),
            //    Password = account.Password,
            //    Name = account.Name,
            //    Login = account.Login,
            //    Email = account.Email
            //});
            //return RedirectToAction("Index", "Home");
        }

        #region RemoteValidators

        public ActionResult CheckEmail(string email)
        {
            //return _userService.IsEmailExists(email)
            //    ? Json($"Email '{email}' arleady registered", JsonRequestBehavior.AllowGet)
            //    : Json("true", JsonRequestBehavior.AllowGet);
            return UserManager.IsEmailExists(email)
                ? Json($"Email '{email}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckLogin(string login)
        {
            //return _userService.IsLoginExists(login)
            //    ? Json($"Login '{login}' arleady registered", JsonRequestBehavior.AllowGet)
            //    : Json("true", JsonRequestBehavior.AllowGet);

            return UserManager.IsLoginExists(login)
                ? Json($"Login '{login}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPhone(string phone)
        {
            //return _userService.IsPhoneExists(FormatPhone(phone))
            //    ? Json($"Phone '{phone}' arleady registered", JsonRequestBehavior.AllowGet)
            //    : Json("true", JsonRequestBehavior.AllowGet);
            return UserManager.IsPhoneExists(FormatPhone(phone))
                ? Json($"Phone '{phone}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers

        private void SignIn(IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                },
                UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
        }

        //private void AddOuthCookies(User user, bool remember)
        //{
        //    FormsAuthentication.SetAuthCookie(user.Login, remember);

        //    var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Role.Name);
        //    var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //    HttpContext.Response.Cookies.Add(authCookie);
        //}

        private string FormatPhone(string phone) =>
            phone
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace(" ", "");

        #endregion
    }
}