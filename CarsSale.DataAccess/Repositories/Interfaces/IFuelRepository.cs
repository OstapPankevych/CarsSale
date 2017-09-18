using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IFuelRepository
    {
        IEnumerable<Fuel> GetFuels();
    }
}