using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class AdvertisementRepository : Repository<ADVERTISEMENT, int>, IAdvertisementRepository
    {
        public AdvertisementRepository(CarsSaleEntities context)
            : base(context.ADVERTISEMENTs)
        {
        }
    }
}
