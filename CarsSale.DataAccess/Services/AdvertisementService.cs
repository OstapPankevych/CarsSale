using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Services.Interfaces;

namespace CarsSale.DataAccess.Services
{
    public class AdvertisementService: Service, IAdvertisementService
    {
        private readonly IVehiclRepository _vehiclRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IBodyTypeRepository _bodyTypeRepository;
        private readonly IVehiclTypeRepository _vehiclTypeRepository;
        private readonly ICompleteSetRepository _completeSetRepository;
        private readonly IEngineRepository _engineRepository;
        private readonly ITransmissionRepository _transmissionRepository;

        public AdvertisementService(CarsSaleEntities dbContext,
            IVehiclRepository vehiclRepository,
            IBrandRepository brandRepository,
            IBodyTypeRepository bodyTypeRepository,
            IVehiclTypeRepository vehiclTypeRepository,
            ICompleteSetRepository completeSetRepository,
            IEngineRepository engineRepository,
            ITransmissionRepository transmissionRepository)
            : base(dbContext)
        {
            _vehiclRepository = vehiclRepository;
        }

        public void CreateAdvertisement(Advertisement advertisement)
        {
            var brand = _brandRepository.CreateIfNotExists(advertisement.Vehicl.Brand.Name);
            var vehiclType = _vehiclTypeRepository.CreateIfNotExists(advertisement.Vehicl.VehiclType.Name);
            var bodyType = _bodyTypeRepository.CreateIfNotExists(advertisement.Vehicl.BodyType.Name);
            var completeSet = _completeSetRepository.CreateIfNotExists(
                advertisement.Vehicl.CompleteSet.Engine.Id,
                advertisement.Vehicl.CompleteSet.Transmission.Id);
        }
    }
}
