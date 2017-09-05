using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class FuelRepository : Repository<FUEL, int>, IFuelRepository
    {
        public FuelRepository(CarsSaleEntities context)
            : base(context.FUELs)
        {
        }

        public FUEL CreateIfNotExists(string name)
        {
            bool Query (FUEL x) => x.NAME == name;
            var fuel = Get(Query);
            if (fuel != null) return fuel;
            Create(new FUEL
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
