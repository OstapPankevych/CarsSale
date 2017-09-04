using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class TransmissionRepository : Repository<TRANSMISSION, int>, ITransmissionRepository
    {
        public TransmissionRepository(CarsSaleEntities context)
            : base(context.TRANSMISSIONs)
        {
        }
    }
}
