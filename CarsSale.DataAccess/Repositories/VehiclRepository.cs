using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclRepository : Repository<VEHICL, int>, IVehiclRepository
    {
        public VehiclRepository(CarsSaleEntities context)
            : base(context.VEHICLs)
        {
        }
    }
}
