using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetRegions();
    }
}