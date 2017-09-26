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
using BrandViewModel = CarsSale.WebUi.Models.Vehicl.BrandViewModel;
using FuelViewModel = CarsSale.WebUi.Models.Vehicl.FuelViewModel;
using RegionViewModel = CarsSale.WebUi.Models.RegionViewModel;
using TransmissionTypeViewModel = CarsSale.WebUi.Models.Vehicl.TransmissionTypeViewModel;
using VehiclTypeViewModel = CarsSale.WebUi.Models.Vehicl.VehiclTypeViewModel;

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
            var advertisement = new AdvertisementViewModel
            {
                RegionOptions = _regionRepository
                    .GetRegions()
                    .Select(x => new RegionViewModel
                    {
                        Name = x.Name,
                        Id = x.Id
                    }),
                BrandOptions = _brandRepository.GetBrands()
                    .Select(x => new BrandViewModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes()
                    .Select(x => new VehiclTypeViewModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes()
                    .Select(x => new TransmissionTypeViewModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                FuelOptions = _fuelRepository.GetFuels()
                    .Select(x => new FuelViewModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToArray()
            };
            return View(advertisement);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult CreateAdvertisement(AdvertisementViewModel adv)
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
                Region = new Region
                {
                    Id = adv.Region.Id
                },
                User = _userService.Get(User.Identity.Name),
                Vehicl = new Vehicl
                {
                    Brand = new Brand
                    {
                        Id = adv.Brand.Id
                    },
                    Engine = new Engine
                    {
                        Volume = adv.EngineVolume,
                        Fuels = adv.FuelOptions
                            .Where(x => x.IsChecked)
                            .Select(x => new Fuel
                            {
                                Id = x.Id
                            })
                    },
                    TransmissionType = new TransmissionType
                        {
                            Id = adv.TransmissionType.Id
                        },
                    VehiclType = new VehiclType
                        {
                            Id = adv.VehiclType.Id
                        }
                }
            };

            var res = _advertisementRepository.Create(advertisement);

            return View("Success", res);
        }
    }
}