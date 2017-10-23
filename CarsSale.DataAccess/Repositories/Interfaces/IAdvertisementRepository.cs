using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IAdvertisementRepository
    {
        Advertisement Create(Advertisement advertisement);
        IEnumerable<Advertisement> GetAdvertisements(
            Brand brand = null,
            Region region = null,
            VehiclType vehiclType = null,
            TransmissionType transmission = null,
            List<Fuel> fuels = null,
            Engine from = null,
            Engine to = null);
    }
}
