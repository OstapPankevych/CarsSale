using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Searchers.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class AdvertisementRepository: Repository, IAdvertisementRepository
    {
        private readonly IAdvertisementSearcher _searcher;

        public AdvertisementRepository(IAdvertisementSearcher searcher)
        {
            _searcher = searcher;
        }

        public IEnumerable<Advertisement> GetAdvertisements(
            Brand brand = null,
            Region region = null,
            VehiclType vehiclType = null,
            TransmissionType transmission = null,
            Fuel[] fuels = null,
            Engine from = null,
            Engine to = null)
        {
            using (var context = CreateContext())
            {
                var advertisements = _searcher
                    .For(context.ADVERTISEMENTs)
                    .ByRegion(region)
                    .ByBrand(brand)
                    .ByFuelType(fuels)
                    .ByRegion(region)
                    .ByVehiclType(vehiclType)
                    .CreateQuery(context.ADVERTISEMENTs);
                return advertisements
                    .AsEnumerable()
                    .Select(x => new Advertisement(x))
                    .ToList();
            }
        }

        public Advertisement GetAdvertisement(int id)
        {
            using (var context = CreateContext())
            {
                return Get(id, context);
            }
        }

        public Advertisement Create(Advertisement advertisement)
        {
            using (var context = CreateContext())
            {
                advertisement.Vehicl.Id = CreateVehiclIfNotExists(advertisement.Vehicl, context);

                var dbAdvr = context.ADVERTISEMENTs.Add(advertisement.Convert());
                context.SaveChanges();
                return new Advertisement(dbAdvr);
            }
        }

        private Advertisement Get(int id, CarsSaleEntities context)
        {
            var db = context.ADVERTISEMENTs
                .Where(adv => adv.ID == id)
                    .Include(adv => adv.User)
                    .Include(adv => adv.REGION)
                    .Include(adv => adv.VEHICL)
                    .Include(adv => adv.VEHICL.BRAND)
                    .Include(adv => adv.VEHICL.ENGINE)
                    .Include(adv => adv.VEHICL.TRANSMISSION_TYPE)
                    .Include(adv => adv.VEHICL.VEHICL_TYPE)
                    .Include(adv => adv.VEHICL.ENGINE.ENGINE_FUEL.Select(f => f.FUEL))
                .FirstOrDefault();
            return db == null ? null : new Advertisement(db);
        }

        private int CreateVehiclIfNotExists(Vehicl vehicl, CarsSaleEntities context)
        {
            vehicl.Engine.Id =
                CreateEngineIfNotExist(vehicl.Engine, context);

            var dbVehicl =
                context.VEHICLs
                .FirstOrDefault(v => v.BRAND_ID == vehicl.Brand.Id
                    && v.VEHICL_TYPE_ID == vehicl.VehiclType.Id
                    && v.ENGINE_ID == vehicl.Engine.Id
                    && v.TRANSMISSION_TYPE_ID == vehicl.TransmissionType.Id
                    && v.ENGINE_ID == vehicl.Engine.Id);

            dbVehicl = dbVehicl ?? context.VEHICLs.Add(vehicl.Convert());
            return dbVehicl.ID;
        }

        private int CreateEngineIfNotExist(Engine engine, CarsSaleEntities context)
        {
            var dbEngine = context.ENGINEs
                .Include(x => x.ENGINE_FUEL)
                .FirstOrDefault(x => x.VOLUME == engine.Volume);

            var existWithFuel = dbEngine?.ENGINE_FUEL.Select(f => f.ID)
                                    .SequenceEqual(engine.Fuels.Select(ef => ef.Id))
                                    ?? false;
            if (existWithFuel) return dbEngine.ID;

            dbEngine = context.ENGINEs.Add(engine.Convert());
            context.ENGINE_FUEL.AddRange(
                engine.Fuels.Select(x => new ENGINE_FUEL
                {
                    ENGINE_ID = dbEngine.ID,
                    FUEL_ID = x.Id
                }));
            
            return dbEngine.ID;
        }

        #region Helpers

        public IQueryable<ADVERTISEMENT> ByRegion(IQueryable<ADVERTISEMENT> query, Region region)
        {
            query = region != null
                ? query.Where(x => x.REGION_ID == region.Id)
                : query;
            return query;
        }

        public IQueryable<ADVERTISEMENT> ByVehiclType(IQueryable<ADVERTISEMENT> query, VehiclType vehiclType)
        {
            query = vehiclType != null
                ? query.Where(x => x.VEHICL.VEHICL_TYPE_ID == vehiclType.Id)
                : query;
            return query;
        }

        public IQueryable<ADVERTISEMENT> ByBrand(IQueryable<ADVERTISEMENT> query, Brand brand)
        {
            query = brand != null
                ? query.Where(x => x.VEHICL.BRAND_ID == brand.Id)
                : query;
            return query;
        }

        public IQueryable<ADVERTISEMENT> ByTransmission(IQueryable<ADVERTISEMENT> query, TransmissionType transmission)
        {
            query = transmission != null
                ? query.Where(x => x.VEHICL.TRANSMISSION_TYPE_ID == transmission.Id)
                : query;
            return query;
        }

        public IQueryable<ADVERTISEMENT> ByEngineVolume(IQueryable<ADVERTISEMENT> query, Engine from, Engine to)
        {
            if (from == null && to == null) return q;
            if (from != null)
            {
                query = query.Where(x => x.VEHICL.ENGINE.VOLUME >= from.Volume);
            }
            if (to != null)
            {
                query = query.Where(x => x.VEHICL.ENGINE.VOLUME <= to.Volume);
            }
            return query;
        }

        public IQueryable<ADVERTISEMENT> ByFuelType(IQueryable<ADVERTISEMENT> query, IEnumerable<Fuel> fuels)
        {
            query = fuels != null
                ? query.Where(x => x.VEHICL.ENGINE.ENGINE_FUEL.Select(ef => ef.FUEL_ID)
                    .SequenceEqual(fuels.Select(f => f.Id)))
                : query;
            return query;
        }

        #endregion
    }
}
