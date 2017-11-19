using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess
{
    public class AdvertisementSearcher
    {
        public IQueryable<ADVERTISEMENT> Query { get; set; }

        public AdvertisementSearcher For(IQueryable<ADVERTISEMENT> query)
        {
            Query = query;
            return this;
        }

        public AdvertisementSearcher By(Region region)
        {
            Query = region != null ? Query.Where(x => x.REGION_ID == region.Id) : Query;
            return this;
        }

        public AdvertisementSearcher By(Brand brand)
        {
            Query = brand != null ? Query.Where(x => x.VEHICL.BRAND_ID == brand.Id) : Query;
            return this;
        }

        public AdvertisementSearcher By(VehiclType vehiclType)
        {
            Query = vehiclType != null ? Query.Where(x => x.VEHICL.VEHICL_TYPE_ID == vehiclType.Id) : Query;
            return this;
        }

        public AdvertisementSearcher By(TransmissionType transmissionType)
        {
            Query = transmissionType != null ? Query.Where(x => x.VEHICL.TRANSMISSION_TYPE_ID == transmissionType.Id) : Query;
            return this;
        }

        public AdvertisementSearcher By(List<Fuel> fuels)
        {
            if(fuels == null) return this;
            var fuelIds = fuels.Select(x => x.Id).ToList();
            Query = Query.Where(
                x => fuelIds.All(id => x.VEHICL.ENGINE.ENGINE_FUEL.Select(ef => ef.FUEL_ID).Any(f => f == id))
                     && fuelIds.Count == x.VEHICL.ENGINE.ENGINE_FUEL.Count);
            return this;
        }

        public AdvertisementSearcher By(Engine from, Engine to)
        {
            if (from != null) Query = Query.Where(x => x.VEHICL.ENGINE.VOLUME >= from.Volume);
            if (to != null) Query = Query.Where(x => x.VEHICL.ENGINE.VOLUME <= to.Volume);
            return this;
        }

        public IQueryable<ADVERTISEMENT> CreateQuery() => Query;
    }
}
