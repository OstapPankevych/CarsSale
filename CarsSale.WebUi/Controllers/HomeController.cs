using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisements;
using CarsSale.WebUi.Models.Vehicl;

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
                    User = new UserViewModel
                    {
                        Name = x.User.Name
                    },
                    Region = new RegionViewModel
                    {
                        Name = x.Region.Name,
                    },
                    Brand = new BrandViewModel
                    {
                        Name = x.Vehicl.Brand.Name
                    },
                    VehiclType = new VehiclTypeViewModel
                    {
                        Name = x.Vehicl.VehiclType.Name
                    },
                    TransmissionType = new TransmissionTypeViewModel
                    {
                        Name = x.Vehicl.TransmissionType.Name
                    },
                    EngineVolume = x.Vehicl.Engine.Volume,
                    FuelOptions = x.Vehicl.Engine.Fuels.Select(fuel => new FuelViewModel
                    {
                        Name = fuel.Name
                    }).ToArray()
                });
            return View("Index", advertisements);
        }
    }
}