using System;
using System.Web.Mvc;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Exceptions;
using CarsSale.WebUi.Filters;
using CarsSale.WebUi.Logger;
using CarsSale.WebUi.Models;

namespace CarsSale.WebUi.Controllers
{
    [CarsSaleExceptionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;

        public HomeController(
            ILogger logger,
            IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository)
        {
            _logger = logger;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionTypeRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _regionRepository = regionRepository;
        }

        public ActionResult Index()
        {
            throw new Exception("testing");
            try
            {
                var searchViewModel = new SearchViewModel
                {
                    BrandOptions = _brandRepository.GetBrands(),
                    RegionOptions = _regionRepository.GetRegions(),
                    VehiclTypeOptions = _vehiclTypeRepository.GetVehiclTypes(),
                    TransmissionTypeOptions = _transmissionTypeRepository.GetTransmissionTypes(),
                    FuelOptions = _fuelRepository.GetFuels()
                };
                return View("Index", searchViewModel);
            }
            catch (Exception ex)
            {
                throw new AdvertisementException($"Error during get search options. Message: {ex.Message}");
            }
        }
    }
}