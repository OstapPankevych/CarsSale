using System.Collections.Generic;
using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class VehiclTypeRepository: Repository, IVehiclTypeRepository
    {
        public VehiclTypeRepository(string connectionString)
            : base(connectionString)
        { }

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
