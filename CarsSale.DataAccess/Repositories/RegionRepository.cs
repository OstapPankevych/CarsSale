using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class RegionRepository : Repository, IRegionRepository
    {
        public RegionRepository(string connectionString)
            : base(connectionString)
        { }

        public IEnumerable<Region> GetRegions()
        {
            using (var context = CreateContext())
            {
                return context.REGIONs
                    .AsEnumerable()
                    .Select(x => new Region(x))
                    .ToList();
            }
        }
    }
}