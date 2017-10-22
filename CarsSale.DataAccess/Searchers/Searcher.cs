using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CarsSale.DataAccess.Searchers.Interfaces;

namespace CarsSale.DataAccess.Searchers
{
    public abstract class Searcher<T, TS>: ISearcher<T, TS>
        where T: class
        where TS: Searcher<T, TS>
    {
        private IQueryable<T> _query;
        
        protected IQueryable<T> Query
        {
            get => _query ?? (_query = Enumerable.Empty<T>().AsQueryable());
            set => _query = value;
        }

        public Searcher<T, TS> For(IQueryable<T> query)
        {
            Query = query;
            return this;
        }
    }
}