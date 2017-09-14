using System;
using System.Data.Entity;
using System.Linq;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class AdvertisementRepository : Repository<ADVERTISEMENT, int>, IAdvertisementRepository
    {
        public AdvertisementRepository(CarsSaleEntities context)
            : base(context.ADVERTISEMENTs)
        {
        }

        public override ADVERTISEMENT Get(Func<ADVERTISEMENT, bool> predicate, bool local = false)
        {
            var dbSet = local
                ? DbSet.Local.AsQueryable()
                : DbSet.AsQueryable();
            return dbSet
                .Include(x => x.USER)
                .Include(x => x.VEHICL)
                .FirstOrDefault(predicate);
        }
    }
}
