using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        public virtual void Create(TEntity entity) =>
            DbSet.Add(entity);

        public virtual void Delete(TEntity entity) =>
            DbSet.Remove(entity);

        public virtual TEntity Get(Func<TEntity, bool> predicate) =>
            DbSet.FirstOrDefault(predicate);

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet : DbSet.Where(predicate);
        }
    }
}
