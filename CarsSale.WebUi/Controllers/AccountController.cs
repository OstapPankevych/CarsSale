using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
        
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registry()
        {
            return View(new Account());
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult SubmitForgotPassword()
        {
            return View("~/Views/Home/Index");
        }

        [HttpPost]
        public ActionResult SubmitRegistry(Account account)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _userService.CreateUser(new DataAccess.DTO.User
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
    }
}