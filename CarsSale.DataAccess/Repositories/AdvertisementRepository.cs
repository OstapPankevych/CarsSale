﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.QueryBuilders;

namespace CarsSale.DataAccess.Repositories
{
    public class AdvertisementRepository: Repository, IAdvertisementRepository
    {
        private readonly IAdvertisementSearchQueryBuilder _queryBuilder;

        public AdvertisementRepository(string connectionString, IAdvertisementSearchQueryBuilder queryBuilder)
            : base(connectionString)
        {
            _queryBuilder = queryBuilder;
        }

        public IEnumerable<Advertisement> GetAdvertisements(
            Brand brand = null,
            Region region = null,
            VehiclType vehiclType = null,
            TransmissionType transmission = null,
            List<Fuel> fuels = null,
            Engine from = null,
            Engine to = null)
        {
            using (var context = CreateContext())
            {
                var query = _queryBuilder
                    .For(context.ADVERTISEMENTs.AsQueryable())
                    .By(brand)
                    .By(region)
                    .By(vehiclType)
                    .By(transmission)
                    .By(fuels)
                    .By(from, to)
                    .CreateQuery();

                 return query
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

        public IEnumerable<Advertisement> GetTopAdvertisements(int top)
        {
            using (var context = CreateContext())
            {
                return context.ADVERTISEMENTs
                    .OrderByDescending(x => x.CREATED_DATE)
                    .Take(top)
                    .AsEnumerable()
                    .Select(x => new Advertisement(x))
                    .ToList();
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
    }
}
