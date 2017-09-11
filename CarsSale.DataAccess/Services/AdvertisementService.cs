using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Services.Interfaces;

namespace CarsSale.DataAccess.Services
{
    public class AdvertisementService : Service, IAdvertisementService
    {
        private readonly IVehiclRepository _vehiclRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly ICompleteSetRepository _completeSetRepository;
        private readonly IEngineRepository _engineRepository;
        private readonly IEngineFuelRepository _engineFuelRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly ITransmissionTypeRepository _transmissionRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IRegionRepository _regionRepository;

        public AdvertisementService(CarsSaleEntities dbContext,
            IVehiclRepository vehiclRepository,
            IBrandRepository brandRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            ICompleteSetRepository completeSetRepository,
            IEngineRepository engineRepository,
            IFuelRepository fuelRepository,
            IEngineFuelRepository engineFuelRepository,
            ITransmissionTypeRepository transmissionRepository,
            IAdvertisementRepository advertisementRepository,
            IRegionRepository regionRepository)
            : base(dbContext)
        {
            _vehiclRepository = vehiclRepository;
            _brandRepository = brandRepository;
            _vehiclTypeRepository = vehiclTypeRepository;
            _completeSetRepository = completeSetRepository;
            _engineRepository = engineRepository;
            _fuelRepository = fuelRepository;
            _engineFuelRepository = engineFuelRepository;
            _transmissionRepository = transmissionRepository;
            _advertisementRepository = advertisementRepository;
            _regionRepository = regionRepository;
        }

        public Advertisement CreateAdvertisement(
            int userId,
            int regionId,
            int brandId,
            int vehiclTypeId,
            int[] fuelIds,
            int engineVolume,
            int transmissionId,
            double expirationDays,
            bool isActive = true)
        {
            var brand = _brandRepository.Get(x => x.ID == brandId);
            var vehiclType = _vehiclTypeRepository.Get(x => x.ID == vehiclTypeId);
            var engine = _engineRepository.CreateIfNotExists(engineVolume);

            var transmission =
                _transmissionRepository.Get(x => x.ID == transmissionId);
            var completeSet = _completeSetRepository.CreateIfNotExists(
                engine.ID,
                transmission.ID);
            var vehicl = _vehiclRepository.CreateIfNotExists(
                brand.ID,
                vehiclType.ID,
                completeSet.ID);
            var today = DateTime.Now;
            _advertisementRepository.Create(new ADVERTISEMENT
            {
                USER_ID = userId,
                REGION_ID = regionId,
                IS_ACTIVE = true,
                VEHICL_ID = vehicl.ID,
                CREATED_DATE = today,
                EXPIRATION_DATE = today.AddDays(expirationDays)
            });
            SaveChanges();
            return new Advertisement(_advertisementRepository.Get(x => x.VEHICL_ID == vehicl.ID));
        }

        public IEnumerable<Brand> GetBrands() =>
            _brandRepository.GetAll()
                .ToList().Select(x => new Brand(x));

        public IEnumerable<Region> GetRegions() =>
            _regionRepository.GetAll()
                .ToList().Select(x => new Region(x));

        public IEnumerable<TransmissionType> GetTransmissionTypes() => 
            _transmissionRepository.GetAll()
                .ToList().Select(x => new TransmissionType(x));

        public IEnumerable<VehiclType> GetVehiclTypes() =>
            _vehiclTypeRepository.GetAll()
                .ToList().Select(x => new VehiclType(x));

        public IEnumerable<Fuel> GetFuels() =>
            _fuelRepository.GetAll()
                .ToList().Select(x => new Fuel(x));
    }
}
