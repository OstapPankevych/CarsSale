using System.Linq;

namespace CarsSale.DataAccess.Repositories.QueryBuilders
{
    public abstract class QueryBuilder<T>
    {
        protected IQueryable<T> Query { get; set; }

        protected void Init(IQueryable<T> query)
        {
            Query = query;
        }

        public IQueryable<T> CreateQuery() => Query;
    }
}