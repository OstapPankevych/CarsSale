using System.Linq;
using CarsSale.DataAccess.Searchers.Interfaces;

namespace CarsSale.DataAccess.Searchers
{
    public abstract class Searcher<T>: ISearcher<T>
        where T: class
    {
        private IQueryable<T> _query;
        
        protected IQueryable<T> Query
        {
            get => _query ?? (_query = Enumerable.Empty<T>().AsQueryable());
            set => _query = value;
        }

        public IQueryable<T> CreateQuery(IQueryable<T> query)
        {
            query.Provider.CreateQuery(Query.Expression);
            Query = null;
            return query;
        }
    }
}