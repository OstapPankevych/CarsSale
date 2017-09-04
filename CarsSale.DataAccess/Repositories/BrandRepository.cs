using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class BrandRepository : Repository<BRAND, int>, IBrandRepository
    {
        public BrandRepository(CarsSaleEntities context)
            : base(context.BRANDs)
        {
        }
    }
}
