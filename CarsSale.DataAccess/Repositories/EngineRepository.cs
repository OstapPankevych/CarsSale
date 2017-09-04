using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class EngineRepository : Repository<ENGINE, int>, IEngineRepository
    {
        public EngineRepository(CarsSaleEntities context)
            : base(context.ENGINEs)
        {
        }
    }
}
