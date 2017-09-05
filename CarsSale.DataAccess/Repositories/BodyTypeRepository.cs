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

        public BODY_TYPE CreateIfNotExists(string name)
        {
            bool Query(BODY_TYPE x) => x.NAME == name;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new BODY_TYPE
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
