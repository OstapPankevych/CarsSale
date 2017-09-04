using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity, in TId> where TEntity: class
    {
        void Create(TEntity entity);
        TEntity Get(Func<TEntity, bool> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
