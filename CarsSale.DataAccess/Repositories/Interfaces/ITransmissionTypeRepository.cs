using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface ITransmissionTypeRepository
    {
        IEnumerable<TransmissionType> GetTransmissionTypes();
    }
}
