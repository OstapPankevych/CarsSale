using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CarsSale.DataAccess.Searchers.Interfaces
{
    public interface ISearcher<T>
        where T : class
    {
        IQueryable<T> CreateQuery(IQueryable<T> query);
    }
}