using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Searchers.Interfaces
{
    public interface IAdvertisementSearcher: ISearcher<ADVERTISEMENT>
    {
        IAdvertisementSearcher ByBrand(Brand brand);
        IAdvertisementSearcher ByFuelType(IEnumerable<Fuel> fuels);
        IAdvertisementSearcher ByRegion(Region region);
        IAdvertisementSearcher ByVehiclType(VehiclType vegiclType);
    }
}