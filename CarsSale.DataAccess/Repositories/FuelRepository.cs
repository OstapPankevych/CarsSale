using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class FuelRepository : Repository, IFuelRepository
    {
        public FuelRepository(string connectionString)
            : base(connectionString)
        { }

        public IEnumerable<Fuel> GetFuels()
        {
            using (var context = CreateContext())
            {
                return context.FUELs
                    .AsEnumerable()
                    .Select(x => new Fuel(x))
                    .ToList();
            }
        }
    }
}
