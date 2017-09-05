using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class RegionRepository : Repository<REGION, int>, IRegionRepository
    {
        public RegionRepository(CarsSaleEntities context)
            : base(context.REGIONs)
        {
        }
    }
}
