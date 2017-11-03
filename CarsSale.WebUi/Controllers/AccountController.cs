using System;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarsSale.DataAccess.Identity.Entities;
using CarsSale.DataAccess.Identity.Managers;
using CarsSale.WebUi.Exceptions;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Logger;
using CarsSale.WebUi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CarsSale.WebUi.Controllers
{
    [CarsSaleExceptionFilter]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        private CarsSaleUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<CarsSaleUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(ILogger logger)
        {
            _logger = logger;
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

            CarsSaleUser dbUser;
            try
            {
                dbUser = UserManager.Find(user.Login, user.Password);
                if (dbUser == null)
                {
                    ModelState.AddModelError("", "Wrong Login or Password!");
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                throw new AccountException($"Error during find user by login {user.Login}. Message: {ex.Message}");
            }

            SignIn(dbUser, user.Remember);

            var url = !string.IsNullOrEmpty(returnUrl)
                ? returnUrl
                : FormsAuthentication.DefaultUrl;

            return Redirect(url);
        }

        public ActionResult LogOut()
        {
            try
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw new AccountException($"Error during SingOut user. Message: {ex.Message}");
            }
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

            var user = new CarsSaleUser
            {
                UserName = account.Login,
                PhoneNumber = account.Phone,
                Logins =
                    {
                        new CarsSaleLogin
                        {
                            LoginProvider = "CarsSale", ProviderKey = account.Login
                        }
                    },
                Email = account.Email,
                Claims =
                {
                    new CarsSaleClaim { ClaimType = ClaimTypes.Name, ClaimValue = account.Name },
                    new CarsSaleClaim {ClaimType = ClaimTypes.DateOfBirth, ClaimValue = account.Birthday.ToLongDateString() }
                }
            };

            IdentityResult result;
            try
            {
                result = UserManager.Create(user, account.Password);
            }
            catch (Exception ex)
            {
                throw new AccountException($"Cannot create user: {new { user.UserName, user.PhoneNumber, account.Login, user.Email }}. Message = {ex.Message}");
            }
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
            try
            {
                return UserManager.IsEmailExists(email)
                    ? Json($"Email '{email}' arleady registered", JsonRequestBehavior.AllowGet)
                    : Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Log(new AccountValidationException($"Error during check email '{email}'. Message: {ex.Message}"));
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckLogin(string login)
        {
            try
            {
                return UserManager.IsLoginExists(login)
                    ? Json($"Login '{login}' arleady registered", JsonRequestBehavior.AllowGet)
                    : Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Log(new AccountValidationException($"Error during check login '{login}'. Message: {ex.Message}"));
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPhone(string phone)
        {
            try
            {
                return UserManager.IsPhoneExists(FormatPhone(phone))
                    ? Json($"Phone '{phone}' arleady registered", JsonRequestBehavior.AllowGet)
                    : Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Log(new AccountValidationException($"Error during check phone '{phone}'. Message: {ex.Message}"));
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers

        private void SignIn(CarsSaleUser user, bool isPersistent)
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = isPersistent
                    },
                    UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
            }
            catch (Exception ex)
            {
                throw new AccountException($"Cannot login { new {UserId = user.Id }}. Message = {ex.Message}");
            }
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