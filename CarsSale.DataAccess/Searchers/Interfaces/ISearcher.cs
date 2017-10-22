using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CarsSale.DataAccess.Searchers.Interfaces
{
    public interface ISearcher<in T, out TS>
        where T : class
        where TS: ISearcher<T, TS>
    {
        TS For(IQueryable<T> query);
    }
}