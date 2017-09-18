using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclTypeRepository: Repository, IVehiclTypeRepository
    {
        public IEnumerable<VehiclType> GetVehiclTypes()
        {
            using (var context = CreateContext())
            {
                return context.VEHICL_TYPE
                    .AsEnumerable()
                    .Select(x => new VehiclType(x))
                    .ToList();
            }
        }
    }
}
