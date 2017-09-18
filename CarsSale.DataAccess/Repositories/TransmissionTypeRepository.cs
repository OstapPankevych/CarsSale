using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class TransmissionTypeRepository: Repository, ITransmissionTypeRepository
    {
        public IEnumerable<TransmissionType> GetTransmissionTypes()
        {
            using (var context = CreateContext())
            {
                return context.TRANSMISSION_TYPE
                    .AsEnumerable()
                    .Select(x => new TransmissionType(x))
                    .ToList();
            }
        }
    }
}
