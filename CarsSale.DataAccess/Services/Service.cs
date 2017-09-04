using CarsSale.DataAccess.Services.Interfaces;
using CarsSale.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Services
{
    public class Service: IService
    {
        private bool _disposed = false;

        protected readonly CarsSaleEntities DbContext;

        public Service(CarsSaleEntities dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                DbContext.Dispose();
            }
            _disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}
