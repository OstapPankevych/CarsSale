using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarsSale.DataAccess;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Services.Interfaces;
using Advertisement = CarsSale.WebUi.Models.Advertisements.Advertisement;
using Brand = CarsSale.WebUi.Models.Vehicl.Brand;
using Fuel = CarsSale.WebUi.Models.Vehicl.Fuel;
using Region = CarsSale.WebUi.Models.Region;
using TransmissionType = CarsSale.WebUi.Models.Vehicl.TransmissionType;
using VehiclType = CarsSale.WebUi.Models.Vehicl.VehiclType;

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

        public ActionResult Index()
        {
            var advertisement = new Advertisement
            {
                RegionOptions = _regionRepository
                    .GetRegions()
                    .Select(x => new Region
                    {
                        Name = x.Name,
                        Id = x.Id
                    }),
                BrandOptions = _brandRepository.GetBrands()
                    .Select(x => new Brand
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes()
                    .Select(x => new VehiclType
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes()
                    .Select(x => new TransmissionType
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                FuelOptions = _fuelRepository.GetFuels()
                    .Select(x => new Fuel
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToArray()
            };
            return View(advertisement);
        }

        [HttpPost]
        public ActionResult CreateAdvertisement(Advertisement adv)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var advertisement = new DataAccess.DTO.Advertisement
            {
                CreatedDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(30),
                IsActive = false,
                Region = new DataAccess.DTO.Region
                {
                    Id = adv.Region.Id
                },
                User = _userService.Get("OPANKEVYCH"),//User.Identity.Name),
                Vehicl = new Vehicl
                {
                    Brand = new DataAccess.DTO.Brand
                    {
                        Id = adv.Brand.Id
                    },
                    Engine = new Engine
                    {
                        Volume = adv.EngineVolume,
                        Fuels = adv.FuelOptions
                            .Where(x => x.IsChecked)
                            .Select(x => new DataAccess.DTO.Fuel
                            {
                                Id = x.Id
                            })
                    },
                    TransmissionType = new DataAccess.DTO.TransmissionType()
                }
            };

            var res = _advertisementRepository.Create(advertisement);

            return View("Success", res);
        }
    }
}