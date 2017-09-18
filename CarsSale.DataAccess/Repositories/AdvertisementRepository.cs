using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories
{
    public class AdvertisementRepository: Repository, IAdvertisementRepository
    {
        public IEnumerable<Advertisement> GetAdvertisements()
        {
            using (var context = CreateContext())
            {
                return context.ADVERTISEMENTs
                    .AsEnumerable()
                    .Select(x => new Advertisement(x))
                    .ToList();
            }
        }

        public Advertisement Create(Advertisement advertisement)
        {
            using (var context = CreateContext())
            {
                var vehicl = CreateVehiclIfNotExists(advertisement.Vehicl, context);
                if (vehicl != null)
                {
                    advertisement.Vehicl = vehicl;
                    advertisement.Vehicl.Engine =
                        CreateEngineIfNotExist(advertisement.Vehicl.Engine, context) ?? advertisement.Vehicl.Engine;
                }

                var dbAdvr = context.ADVERTISEMENTs.Add(advertisement.Convert());
                context.SaveChanges();
                return new Advertisement(dbAdvr);
            }
        }

        private Vehicl CreateVehiclIfNotExists(Vehicl vehicl, CarsSaleEntities context)
        {
            var dbVehicl =
                context.VEHICLs.FirstOrDefault(v =>
                    v.BRAND_ID == vehicl.Brand.Id
                    && v.VEHICL_TYPE_ID == vehicl.VehiclType.Id
                    && v.ENGINE_ID == vehicl.Engine.Id
                    && v.TRANSMISSION_TYPE_ID == vehicl.TransmissionType.Id);

            return dbVehicl != null && dbVehicl.ENGINE.ENGINE_FUEL.Select(x => x.FUEL_ID)
                       .Equals(vehicl.Engine.Fuels.Select(x => x.Id))
                       ? null
                       : new Vehicl(context.VEHICLs.Add(vehicl.Convert()));
        }

        private Engine CreateEngineIfNotExist(Engine engine, CarsSaleEntities context)
        {
            var dbEngine = context.ENGINEs
                .Include(x => x.ENGINE_FUEL)
                .FirstOrDefault(
                    x => x.VOLUME == engine.Volume
                    && engine.Fuels.Select(f => f.Id)
                        .SequenceEqual(engine.Fuels.Select(ef => ef.Id)));

            if (dbEngine != null) return null;
            dbEngine = context.ENGINEs.Add(engine.Convert());
            var engineFuels = context.ENGINE_FUEL.AddRange(
                engine.Fuels.Select(x => new ENGINE_FUEL
                {
                    ENGINE_ID = dbEngine.ID,
                    FUEL_ID = x.Id
                }));
            dbEngine.ENGINE_FUEL = engineFuels.ToList();
            return engine;
        }
    }
}
