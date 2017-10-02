using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarsSale.DataAccess;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.WebUi.Models.Advertisements;

namespace CarsSale.WebUi.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly ITransmissionTypeRepository _transmissionRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IUserService _userService;

        public AdvertisementController(
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IAdvertisementRepository advertisementRepository,
            IRegionRepository regionRepository,
            IUserService userService)
        {
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _advertisementRepository = advertisementRepository;
            _regionRepository = regionRepository;
            _userService = userService;
        }

        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            var advertisement = new NewAdvertisementViewModel
            {
                RegionOptions = _regionRepository.GetRegions(),
                BrandOptions = _brandRepository.GetBrands(),
                VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes(),
                TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes(),
                Fuels = _fuelRepository.GetFuels().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(advertisement);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult CreateAdvertisement(NewAdvertisementViewModel adv)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var advertisement = new Advertisement
            {
                CreatedDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(30),
                IsActive = false,
                User = _userService.Get(User.Identity.Name),
                Region = adv.Region,
                Vehicl = new Vehicl
                {
                    Brand = adv.Brand,
                    Engine = new Engine
                    {
                        Volume = adv.EngineVolume,
                        Fuels = adv.Fuels.Where(x => x.Selected).Select(x => new Fuel { Id = Convert.ToInt32(x.Value) })
                    },
                    TransmissionType = adv.TransmissionType,
                    VehiclType = adv.VehiclType
                }
            };

            var res = _advertisementRepository.Create(advertisement);

            return View("Success", res);
        }
    }
}