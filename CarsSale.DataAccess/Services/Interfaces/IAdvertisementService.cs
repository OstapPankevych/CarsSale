using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Advertisement CreateAdvertisement(
            int userId,
            int regionId,
            int brandId,
            int vehiclTypeId,
            int[] fuelIds,
            int engineVolume,
            int transmissionId,
            double expirationDays,
            bool isActive = true);

        IEnumerable<Brand> GetBrands();
        IEnumerable<VehiclType> GetVehiclTypes();
        IEnumerable<TransmissionType> GetTransmissionTypes();
        IEnumerable<Region> GetRegions();
        IEnumerable<Fuel> GetFuels();
    }
}