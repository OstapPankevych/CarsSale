using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Models;

namespace CarsSale.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;

        public HomeController(IAdvertisementRepository advertisementRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            IRegionRepository regionRepository)
        {
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _transmissionTypeRepository = transmissionRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _regionRepository = regionRepository;
        }

        public ActionResult Index()
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
    }
}