using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
