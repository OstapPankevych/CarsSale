using System.Linq;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.WebUi.Models;

namespace CarsSale.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private const int UnselectedId = -1;

        private readonly IAdvertisementRepository _advertisementRepository;
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
            _advertisementRepository = advertisementRepository;
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

        public PartialViewResult Search(SearchViewModel searchViewModel)
        {
            var advertisements = _advertisementRepository.GetAdvertisements(
                searchViewModel.Brand.Id != UnselectedId ? searchViewModel.Brand : null,
                searchViewModel.Region.Id != UnselectedId ? searchViewModel.Region : null,
                searchViewModel.VehiclType.Id != UnselectedId ? searchViewModel.VehiclType : null,
                searchViewModel.TransmissionType.Id != UnselectedId ? searchViewModel.TransmissionType : null,
                searchViewModel.FuelIds.Select(x => new Fuel { Id = x }).ToList(),
                searchViewModel.EngineVolumeFrom != null ? new Engine { Volume = searchViewModel.EngineVolumeFrom.Value } : null,
                searchViewModel.EngineVolumeFrom != null ? new Engine { Volume = searchViewModel.EngineVolumeFrom.Value } : null);
            return PartialView("Partials/Advertisement", advertisements);
        }
    }
}