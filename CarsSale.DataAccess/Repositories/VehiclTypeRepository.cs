using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclTypeRepository : Repository<VEHICL_TYPE, int>, IVehiclTypeRepository
    {
        public VehiclTypeRepository(CarsSaleEntities context)
            : base(context.VEHICL_TYPE)
        {
        }
    }
}
