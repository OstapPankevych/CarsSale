using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity.Managers;
using CarsSale.DataAccess.Providers.Content;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisement;
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
        private readonly ICurrencyRepository _currencyRepository;

        private CarsSaleUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<CarsSaleUserManager>();

        public AdvertisementController(
            IContentProvider contentProvider,
            IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository,
            ICurrencyRepository currencyRepository)
        {
            _contentProvider = contentProvider;
            _advertisementRepository = advertisementRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _regionRepository = regionRepository;
            _currencyRepository = currencyRepository;
        }

        [Authorize]
        public ActionResult Create()
        {
            var advertisement = new CreateAdvertisementViewModel();
            InitCreateAdvertismentViewModelOptions(advertisement);
            return View(advertisement);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateAdvertisementViewModel adv)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    if (errors.Count == 0) continue;
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(key, error.ErrorMessage);
                    }
                }
                ModelState.AddModelError("", "The form is invalid! Please try again");
                InitCreateAdvertismentViewModelOptions(adv);
                return View(adv);
            }

            var rootFolder = ConfigurationManager.AppSettings["advertismentsRootFolder"];
            var imagesFolder = Path.Combine(rootFolder, Guid.NewGuid().ToString());
            var fullPath = Path.Combine(imagesFolder, adv.Image.FileName);

            var advertisement = new Advertisement
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.Today.AddDays(30),
                IsActive = false,
                Region = new Region { Id = adv.RegionId },
                Vehicl = new Vehicl
                {
                    Brand = new Brand { Id = adv.BrandId },
                    Engine = new Engine
                    {
                        Volume = adv.EngineVolume,
                        Fuels = adv.Fuels.Select(x => new Fuel {Id = x})
                    },
                    TransmissionType = new TransmissionType { Id = adv.TransmissionTypeId },
                    VehiclType = new VehiclType { Id = adv.VehiclTypeId }
                },
                User = UserManager.FindByLogin(User.Identity.Name),
                ImagePath = fullPath,
                Price = adv.Price,
                Currency = new Currency { Id = adv.CurrencyId }
            };
            
            _contentProvider.Upload(fullPath, adv.Image.InputStream);

            _advertisementRepository.Create(advertisement);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Search()
        {
            var searchViewModel = new SearchAdvertismentViewModel
            {
                RegionOptions = _regionRepository.GetRegions(),
                BrandOptions = _brandRepository.GetBrands(),
                FuelOptions = _fuelRepository.GetFuels(),
                VehiclTypeOptions =  _vehiclTypeRepository.GetVehiclTypes(),
                TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes()
            };
            return View(searchViewModel);
        }

        [HttpPost]
        public PartialViewResult Search(SearchAdvertismentViewModel searchViewModel)
        {
            var advertisements = _advertisementRepository.GetAdvertisements(
                searchViewModel.Brand,
                searchViewModel.Region,
                searchViewModel.VehiclType,
                searchViewModel.TransmissionType,
                searchViewModel.Fuels?.ToList(),
                searchViewModel.EngineFrom != null ? new Engine { Volume = searchViewModel.EngineFrom.Volume } : null,
                searchViewModel.EngineTo != null ? new Engine { Volume = searchViewModel.EngineTo.Volume } : null);
            return PartialView("Partials/Advertisment", advertisements);
        }

        public FileContentResult GetImage(string imagePath)
        {
            var stream = _contentProvider.Load(imagePath);
            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            return new FileContentResult(ms.ToArray(), $"image/{Path.GetExtension(imagePath)}");
        }

        public PartialViewResult GetTopAdvertisements(int top)
        {
            var arvertisements = _advertisementRepository.GetTopAdvertisements(top);
            return PartialView("Partials/AdvertisementSlider", arvertisements);
        }

        private void InitCreateAdvertismentViewModelOptions(CreateAdvertisementViewModel adv)
        {
            adv.RegionOptions = _regionRepository.GetRegions();
            adv.BrandOptions = _brandRepository.GetBrands();
            adv.VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes();
            adv.TransmissionTypeOptions = _transmissionRepository.GetTransmissionTypes();
            adv.FuelOptions = _fuelRepository.GetFuels();
            adv.CurrencyOptions = _currencyRepository.GetCurrencies();
        }
    }
}