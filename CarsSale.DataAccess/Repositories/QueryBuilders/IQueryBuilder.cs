using System.Linq;

namespace CarsSale.DataAccess.Repositories.QueryBuilders
{
    public interface IQueryBuilder<out TQueryBuilder, TEntity>
    {
        TQueryBuilder For(IQueryable<TEntity> query);
        IQueryable<TEntity> CreateQuery();
    }
}