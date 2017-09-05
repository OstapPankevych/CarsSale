using System;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class EngineRepository : Repository<ENGINE, int>, IEngineRepository
    {
        public EngineRepository(CarsSaleEntities context)
            : base(context.ENGINEs)
        {
        }

        public ENGINE CreateIfNotExists(int volume)
        {
            bool Query(ENGINE x) => x.VOLUME == volume;

            var eng = Get(Query) ;
            if (eng != null) return eng;
            Create(new ENGINE
            {
                VOLUME = volume
            });
            return Get(Query);
        }
    }
}
