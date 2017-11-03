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
using CarsSale.WebUi.Exceptions;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Logger;
using CarsSale.WebUi.Models;
using CarsSale.WebUi.Models.Advertisements;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CarsSale.WebUi.Controllers
{
    [CarsSaleExceptionFilter]
    public class AdvertisementController : Controller
    {
        private readonly ILogger _logger;
        private readonly IContentProvider _contentProvider;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly ITransmissionTypeRepository _transmissionRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly IRegionRepository _regionRepository;

        private CarsSaleUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<CarsSaleUserManager>();

        public AdvertisementController(
            ILogger logger,
            IContentProvider contentProvider,
            IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository)
        {
            _logger = logger;
            _contentProvider = contentProvider;
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
            try
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
            catch (Exception ex)
            {
                throw new AdvertisementException($"Error during getting advertisement options. Message: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(NewAdvertisementViewModel adv)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "The registered form is invalid! Please try again");
                    return Index();
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
            catch (Exception ex)
            {
                throw new AdvertisementException($"Error during create new Advertisement: {adv}. Message: {ex.Message}");
            }
        }

        public PartialViewResult Search(SearchViewModel searchViewModel)
        {
            try
            {
                var advertisements = _advertisementRepository.GetAdvertisements(
                    searchViewModel.Brand,
                    searchViewModel.Region,
                    searchViewModel.VehiclType,
                    searchViewModel.TransmissionType,
                    searchViewModel.Fuels?.ToList(),
                    searchViewModel.EngineFrom != null ? new Engine {Volume = searchViewModel.EngineFrom.Volume} : null,
                    searchViewModel.EngineTo != null ? new Engine {Volume = searchViewModel.EngineTo.Volume} : null);
                return PartialView("~/Views/Partials/Advertisement.cshtml", advertisements);
            }
            catch (Exception ex)
            {
                throw new AdvertisementException($"Error during search advertisemetn with parameters: {searchViewModel}. Message: {ex.Message}");
            }
        }

        public FileContentResult GetImage(string imagePath)
        {
            try
            {
                var stream = _contentProvider.Load(imagePath);
                var ms = new MemoryStream();
                stream.Position = 0;
                stream.CopyTo(ms);
                return new FileContentResult(ms.ToArray(), $"image/{Path.GetExtension(imagePath)}");
            }
            catch (Exception ex)
            {
                _logger.Log(new AdvertisementException($"Error during get image from azure by path {imagePath}. Message: {ex.Message}"));
            }
            return new FileContentResult(new byte[] {}, "image/jpg");
        }
    }
}