﻿using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclRepository : Repository<VEHICL, int>, IVehiclRepository
    {
        public VehiclRepository(CarsSaleEntities context)
            : base(context.VEHICLs)
        {
        }

        public VEHICL CreateIfNotExists(int brandId,
            int vehiclTypeId,
            int bodyTypeId,
            int completeSetId)
        {
            bool Query(VEHICL x) =>
                x.BRAND_ID == brandId
                && x.VEHICL_TYPE_ID == vehiclTypeId
                && x.BODY_TYPE_ID == bodyTypeId
                && x.COMPLETESET_ID == completeSetId;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new VEHICL
            {
                BRAND_ID = brandId,
                VEHICL_TYPE_ID = vehiclTypeId,
                BODY_TYPE_ID = bodyTypeId,
                COMPLETESET_ID = completeSetId
            });
            return Get(Query);
        }
    }
}