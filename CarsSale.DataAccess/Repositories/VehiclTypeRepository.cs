using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclTypeRepository : Repository<VEHICL_TYPE, int>, IVehiclTypeRepository
    {
        public VehiclTypeRepository(CarsSaleEntities context)
            : base(context.VEHICL_TYPE)
        {
        }

        public VEHICL_TYPE CreateIfNotExists(string name)
        {
            bool Query (VEHICL_TYPE x) => x.NAME == name;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new VEHICL_TYPE
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
