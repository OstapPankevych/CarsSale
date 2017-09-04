using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
