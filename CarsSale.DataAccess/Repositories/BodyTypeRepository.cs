using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class BodyTypeRepository : Repository<BODY_TYPE, int>, IBodyTypeRepository
    {
        public BodyTypeRepository(CarsSaleEntities context)
            : base(context.BODY_TYPE)
        {
        }
    }
}
