using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class TransmissionTypeRepository : Repository<TRANSMISSION_TYPE, int>, ITransmissionTypeRepository
    {
        public TransmissionTypeRepository(CarsSaleEntities context)
            : base(context.TRANSMISSION_TYPE)
        {
        }

        public TRANSMISSION_TYPE CreateIfNotExists(string name)
        {
            bool Query(TRANSMISSION_TYPE x) => x.NAME == name;
            var vehiclType = Get(Query);
            if (vehiclType != null) return vehiclType;
            Create(new TRANSMISSION_TYPE
            {
                NAME = name
            });
            return Get(Query);
        }
    }
}
