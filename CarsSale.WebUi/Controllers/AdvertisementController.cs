using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity.Managers;
using CarsSale.DataAccess.Providers.Content;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisements;
using Microsoft.AspNet.Identity.Owin;

namespace CarsSale.WebUi.Controllers
{
    [ExceptionLoggingFilter]
    [LoggingFilter]
    public class AdvertisementController : Controller
    {
        private readonly IContentProvider _contentProvider;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly ITransmissionTypeRepository _transmissionRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly IRegionRepository _regionRepository;

        private CarsSaleUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<CarsSaleUserManager>();

        public AdvertisementController(
            IContentProvider contentProvider,
            IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository)
        {
            _contentProvider = contentProvider;
            _advertisementRepository = advertisementRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _regionRepository = regionRepository;
        }

        [Authorize]
        public ActionResult Create()
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
        public ActionResult Create(NewAdvertisementViewModel adv)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The registered form is invalid! Please try again");
                return View();
            }

            var imagesFolder = new Guid();
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
                        Fuels = adv.Fuels.Select(x => new Fuel {Id = x})
                    },
                    TransmissionType = adv.TransmissionType,
                    VehiclType = adv.VehiclType
                },
                User = UserManager.FindByLogin(User.Identity.Name),
                ImagePath = Path.Combine(imagesFolder.ToString(), adv.Image.FileName)
            };

            _contentProvider.Upload(Path.Combine(imagesFolder.ToString(), adv.Image.FileName),
                adv.Image.InputStream);

            _advertisementRepository.Create(advertisement);

            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult Search(SearchViewModel searchViewModel)
        {
            var advertisements = _advertisementRepository.GetAdvertisements(
                searchViewModel.Brand,
                searchViewModel.Region,
                searchViewModel.VehiclType,
                searchViewModel.TransmissionType,
                searchViewModel.Fuels?.ToList(),
                searchViewModel.EngineFrom != null ? new Engine {Volume = searchViewModel.EngineFrom.Volume} : null,
                searchViewModel.EngineTo != null ? new Engine {Volume = searchViewModel.EngineTo.Volume} : null);
            return PartialView("Partials/Advertisement", advertisements);
        }

        public FileContentResult GetImage(string imagePath)
        {
            var stream = _contentProvider.Load(imagePath);
            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            return new FileContentResult(ms.ToArray(), $"image/{Path.GetExtension(imagePath)}");
        }
    }
}