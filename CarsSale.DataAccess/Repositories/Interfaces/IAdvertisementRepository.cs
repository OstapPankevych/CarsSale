using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IAdvertisementRepository
    {
        Advertisement Create(Advertisement advertisement);
        IEnumerable<Advertisement> GetAdvertisements();
    }
}
