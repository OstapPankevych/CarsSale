using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class EngineFuelRepository : Repository<ENGINE_FUEL, int>, IEngineFuelRepository
    {
        public EngineFuelRepository(CarsSaleEntities context)
            : base(context.ENGINE_FUEL)
        {
        }

        public IEnumerable<ENGINE_FUEL> CreateIfNotExist(int engineId, int[] fuelIds)
        {
            Expression<Func<ENGINE_FUEL, bool>> query =
                x => x.ENGINE_ID == engineId
                    && fuelIds.Contains(x.FUEL_ID);
            var engineFuels = GetAll(query).ToList();
            if (engineFuels.Count == fuelIds.Length) return engineFuels;
            foreach (var fuelId in fuelIds)
            {
                Create(new ENGINE_FUEL
                {
                    ENGINE_ID = engineId,
                    FUEL_ID = fuelId
                });
            }
            return GetAll(query).ToList();
        }
    }
}
