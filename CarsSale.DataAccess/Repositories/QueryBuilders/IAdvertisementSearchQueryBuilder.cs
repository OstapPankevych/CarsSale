using System.Collections.Generic;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.QueryBuilders
{
    public interface IAdvertisementSearchQueryBuilder:
        IQueryBuilder<IAdvertisementSearchQueryBuilder, ADVERTISEMENT>
    {
        IAdvertisementSearchQueryBuilder By(Region region);

        IAdvertisementSearchQueryBuilder By(Brand brand);

        IAdvertisementSearchQueryBuilder By(VehiclType vehiclType);

        IAdvertisementSearchQueryBuilder By(TransmissionType transmissionType);

        IAdvertisementSearchQueryBuilder By(List<Fuel> fuels);

        IAdvertisementSearchQueryBuilder By(Engine from, Engine to);
    }
}