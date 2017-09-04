using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class CompleteSetRepository : Repository<COMPLETESET, int>, ICompleteSetRepository
    {
        public CompleteSetRepository(CarsSaleEntities context)
            : base(context.COMPLETESETs)
        {
        }
    }
}
