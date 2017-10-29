using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity.Managers;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisements;
using Microsoft.AspNet.Identity.Owin;

namespace CarsSale.WebUi.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly ITransmissionTypeRepository _transmissionRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly IRegionRepository _regionRepository;

        private CarsSaleUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<CarsSaleUserManager>();

        public AdvertisementController(
            IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository)
        {
            _advertisementRepository = advertisementRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _regionRepository = regionRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            var advertisement = new NewAdvertisementViewModel
            {
                RegionOptions = _regionRepository.GetRegions(),
                BrandOptions = _brandRepository.GetBrands(),
                VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes(),
                TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes(),
                FuelOptions = _fuelRepository.GetFuels(),
               
            };
            return View(advertisement);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(NewAdvertisementViewModel adv)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The registered form is invalid! Please try again");
                return View("Index", adv);
            }

            var advertisement = new Advertisement
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Today.AddDays(30),
                IsActive = false,
                Region = adv.Region,
                Vehicl = new Vehicl
                {
                    Brand = adv.Brand,
                    Engine = new Engine
                    {
                        Volume = adv.EngineVolume,
                        Fuels = adv.Fuels.Select(x => new Fuel { Id = x })
                    },
                    TransmissionType = adv.TransmissionType,
                    VehiclType = adv.VehiclType
                },
                User = UserManager.FindByLogin(User.Identity.Name)
        };

            var res = _advertisementRepository.Create(advertisement);

            return View("Success", res);
        }

        public PartialViewResult Search(SearchViewModel searchViewModel)
        {
            var advertisements = _advertisementRepository.GetAdvertisements(
                searchViewModel.Brand,
                searchViewModel.Region,
                searchViewModel.VehiclType,
                searchViewModel.TransmissionType,
                searchViewModel.Fuels?.ToList(),
                searchViewModel.EngineFrom != null ? new Engine { Volume = searchViewModel.EngineFrom.Volume } : null,
                searchViewModel.EngineTo != null ? new Engine { Volume = searchViewModel.EngineTo.Volume } : null);
            return PartialView("~/Views/Partials/Advertisement.cshtml", advertisements);
        }
    }
}