using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Vehicl;
using Advertisement = CarsSale.WebUi.Models.Advertisements.Advertisement;

namespace CarsSale.WebUi.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        // GET: Advertisement
        public ActionResult Index()
        {
            var advertisement = new Advertisement
            {
                RegionOptions = _advertisementService.GetRegions()
                    .Select(x => new Region
                        {
                            Name = x.Name,
                            Id = x.Id
                        }),
                BrandOptions = _advertisementService.GetBrands()
                    .Select(x => new Brand
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                VehiclTypeOptions = _advertisementService.GetVehiclTypes()
                    .Select(x => new VehiclType
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                TransmissionTypeOptions = _advertisementService.GetTransmissionTypes()
                    .Select(x => new TransmissionType
                        {
                            Id = x.Id,
                            Name = x.Name
                        }),
                FuelOptions = _advertisementService.GetFuels()
                    .Select(x => new Fuel
                        {
                            Id = x.Id,
                            Name = x.Name
                        })
            };
            return View(advertisement);
        }

        [HttpPost]
        public ActionResult CreateAdvertisement(Advertisement adv)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userId = 1;
            var regionId = 1;
            var fuels = new int[] {1};

            var expirationDays = 30;
            var isActive = true;

            var advertisement = _advertisementService.CreateAdvertisement(
                userId,
                regionId,
                adv.Brand.Id,
                adv.VehiclType.Id,
                fuels,
                adv.EngineVolume,
                adv.TransmissionType.Id,
                expirationDays,
                isActive);

            return View("Success", advertisement);
        }
    }
}