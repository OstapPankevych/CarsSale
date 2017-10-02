using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisements;

namespace CarsSale.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public HomeController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            var advertisements = _advertisementRepository.GetAdvertisements()
                .Select(x => new AdvertisementViewModel
                {
                    User = x.User,
                    Region = x.Region,
                    Vehicl = new Vehicl
                    {
                        Brand = x.Vehicl.Brand,
                        VehiclType = x.Vehicl.VehiclType,
                        TransmissionType = x.Vehicl.TransmissionType,
                        Engine = x.Vehicl.Engine
                    }
                    
                });
            return View("Index", advertisements);
        }
    }
}