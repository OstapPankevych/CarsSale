using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class CompleteSetRepository : Repository<COMPLETESET, int>, ICompleteSetRepository
    {
        public CompleteSetRepository(CarsSaleEntities context)
            : base(context.COMPLETESETs)
        {
        }

        public COMPLETESET CreateIfNotExists(int engineId, int transmissionId)
        {
            bool Query(COMPLETESET x) => x.ENGINE_ID == engineId && x.TRANSMISSION_ID == transmissionId;
            var cms = Get(Query);
            if (cms != null) return cms;
            Create(new COMPLETESET
            {
                ENGINE_ID = engineId,
                TRANSMISSION_ID = transmissionId
            });
            return Get(Query, true);
        }
    }
}
