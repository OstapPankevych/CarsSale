using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Searchers.Interfaces;

namespace CarsSale.DataAccess.Searchers
{
    public class AdvertisementSearcher: Searcher<ADVERTISEMENT>, IAdvertisementSearcher
    {
        public IAdvertisementSearcher ByRegion(Region region)
        {
            Query = region != null
                ? Query.Where(x => x.REGION_ID == region.Id)
                : Query;
            return this;
        }

        public IAdvertisementSearcher ByVehiclType(VehiclType vehiclType)
        {
            Query = vehiclType != null
                ? Query.Where(x => x.VEHICL.VEHICL_TYPE_ID == vehiclType.Id)
                : Query;
            return this;
        }

        public IAdvertisementSearcher ByBrand(Brand brand)
        {
            Query = brand != null
                ? Query.Where(x => x.VEHICL.BRAND_ID == brand.Id)
                : Query;
            return this;
        }

        public IAdvertisementSearcher ByTransmission(TransmissionType transmission)
        {
            Query = transmission != null
                ? Query.Where(x => x.VEHICL.TRANSMISSION_TYPE_ID == transmission.Id)
                : Query;
            return this;
        }

        public IAdvertisementSearcher ByEngineVolume(Engine from, Engine to)
        {
            if (from == null && to == null) return this;
            if (from != null)
            {
                Query = Query.Where(x => x.VEHICL.ENGINE.VOLUME >= from.Volume);
            }
            if (to != null)
            {
                Query = Query.Where(x => x.VEHICL.ENGINE.VOLUME <= to.Volume);
            }
            return this;
        }

        public IAdvertisementSearcher ByFuelType(IEnumerable<Fuel> fuels)
        {
            Query = fuels != null
                ? Query.Where(x => x.VEHICL.ENGINE.ENGINE_FUEL.Select(ef => ef.FUEL_ID)
                    .SequenceEqual(fuels.Select(f => f.Id)))
                : Query;
            return this;
        }
    }
}