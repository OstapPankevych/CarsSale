using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.QueryBuilders
{
    public class AdvertismentRepositoryLinqExpressions :
        QueryBuilder<ADVERTISEMENT>,
        IAdvertisementSearchQueryBuilder
    {
        private Expression<Func<ADVERTISEMENT, bool>> BuildPredicate<T>(Type type,
            string prop,
            Func<Expression, Expression, BinaryExpression> operation,
            T value)
        {
            var pe = Expression.Parameter(type, "x");
            var left = prop.Split('.').Aggregate<string, Expression>(pe, Expression.PropertyOrField);
            var right = Expression.Constant(value, typeof(T));
            var equal = operation(left, right);
            var where = Expression.Lambda<Func<ADVERTISEMENT, bool>>(equal, pe);
            return where;
        }

        public IAdvertisementSearchQueryBuilder By(Region region)
        {
            if (region != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "REGION_ID", Expression.Equal, region.Id));
            }
            return this;
        }

        public IAdvertisementSearchQueryBuilder By(Brand brand)
        {
            if (brand != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "VEHICL.BRAND_ID", Expression.Equal, brand.Id));
            }
            return this;
        }

        public IAdvertisementSearchQueryBuilder By(VehiclType vehiclType)
        {
            if (vehiclType != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "VEHICL.VEHICL_TYPE_ID", Expression.Equal, vehiclType.Id));
            }
            return this;
        }

        public IAdvertisementSearchQueryBuilder By(TransmissionType transmissionType)
        {
            if (transmissionType != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "VEHICL.TRANSMISSION_TYPE_ID", Expression.Equal, transmissionType.Id));
            }
            return this;
        }

        public IAdvertisementSearchQueryBuilder By(List<Fuel> fuels)
        {
            if (fuels == null) return this;
            var fuelIds = fuels.Select(x => x.Id).ToList();
            Query = Query.Where(
                x => fuelIds.All(id => x.VEHICL.ENGINE.ENGINE_FUEL.Select(ef => ef.FUEL_ID).Any(f => f == id))
                     && fuelIds.Count == x.VEHICL.ENGINE.ENGINE_FUEL.Count);
            return this;
        }

        public IAdvertisementSearchQueryBuilder By(Engine from, Engine to)
        {
            if (from != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "VEHICL.ENGINE.VOLUME", Expression.GreaterThanOrEqual, from.Volume));
            }
            if (to != null)
            {
                Query = Query.Where(BuildPredicate(typeof(ADVERTISEMENT), "VEHICL.ENGINE.VOLUME", Expression.LessThanOrEqual, to.Volume));
            }
            return this;
        }

        public IAdvertisementSearchQueryBuilder For(IQueryable<ADVERTISEMENT> query)
        {
            Init(query);
            return this;
        }
    }
}