using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public abstract class Repository
    {
        protected CarsSaleEntities CreateContext() => new CarsSaleEntities();
    }
}
