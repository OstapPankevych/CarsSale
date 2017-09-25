using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class BrandRepository: Repository, IBrandRepository
    {
        public IEnumerable<Brand> GetBrands()
        {
            using (var context = CreateContext())
            {
                return context.BRANDs
                    .AsEnumerable()
                    .Select(x => new Brand(x))
                    .ToList();
            }
        }
    }
}
