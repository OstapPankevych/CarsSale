using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IEngineFuelRepository: IRepository<ENGINE_FUEL, int>
    {
        IEnumerable<ENGINE_FUEL> CreateIfNotExist(int engineId, int[] fuelIds);
    }
}
