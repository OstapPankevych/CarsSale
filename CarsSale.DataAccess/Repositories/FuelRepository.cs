using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class FuelRepository : Repository<FUEL, int>, IFuelRepository
    {
        public FuelRepository(CarsSaleEntities context)
            : base(context.FUELs)
        {
        }
    }
}
