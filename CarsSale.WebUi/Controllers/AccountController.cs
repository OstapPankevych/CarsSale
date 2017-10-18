using System;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Entities;
using CarsSale.DataAccess.Identity;
using CarsSale.WebUi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CarsSale.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        
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
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registry(AccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The registered form is invalid! Please try again");
                return View();
            }

            if (UserManager.IsEmailExists(account.Email))
            {
                ModelState.AddModelError("Email", $"Email '{account.Email}' arleady registered");
                return View();
            }

            if (UserManager.IsLoginExists(account.Login))
            {
                ModelState.AddModelError("Email", $"Login '{account.Login}' arleady registered");
                return View();
            }

            if (UserManager.IsPhoneExists(FormatPhone(account.Phone)))
            {
                ModelState.AddModelError("Email", $"Phone '{account.Phone}' arleady registered");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = account.Login,
                PhoneNumber = account.Phone,
                Logins =
                    {
                        new ApplicationLogin
                        {
                            LoginProvider = "CarsSale", ProviderKey = account.Login
                        }
                    },
                Email = account.Email,
                Claims =
                {
                    new ApplicationClaim { ClaimType = ClaimTypes.Name, ClaimValue = account.Name },
                    new ApplicationClaim {ClaimType = ClaimTypes.DateOfBirth, ClaimValue = account.Birthday.ToLongDateString() }
                }
            };
            var result = UserManager.Create(user, account.Password);
            if (result.Succeeded)
            {
                SignIn(user, false);

                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View();
        }

        #region RemoteValidators

        public ActionResult CheckEmail(string email)
        {
            return UserManager.IsEmailExists(email)
                ? Json($"Email '{email}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckLogin(string login)
        {
            return UserManager.IsLoginExists(login)
                ? Json($"Login '{login}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPhone(string phone)
        {
            return UserManager.IsPhoneExists(FormatPhone(phone))
                ? Json($"Phone '{phone}' arleady registered", JsonRequestBehavior.AllowGet)
                : Json("true", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers

        private void SignIn(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                },
                UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
        }

        private string FormatPhone(string phone) =>
            phone
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace(" ", "");

        #endregion
    }
}