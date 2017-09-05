using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class TransmissionRepository : Repository<TRANSMISSION, int>, ITransmissionRepository
    {
        public TransmissionRepository(CarsSaleEntities context)
            : base(context.TRANSMISSIONs)
        {
        }

        public TRANSMISSION CreateIfNotExists(string name)
        {
            bool Query(TRANSMISSION x) => x.NAME == name;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new TRANSMISSION
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
