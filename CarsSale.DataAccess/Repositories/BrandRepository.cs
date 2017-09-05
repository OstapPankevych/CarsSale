using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class BrandRepository : Repository<BRAND, int>, IBrandRepository
    {
        public BrandRepository(CarsSaleEntities context)
            : base(context.BRANDs)
        {
        }

        public BRAND CreateIfNotExists(string name)
        {
            bool Query(BRAND x) => x.NAME == name;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new BRAND
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
