using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class UserRepository : Repository<USER, int>, IUserRepository
    {
        public UserRepository(CarsSaleEntities context)
            : base(context.USERs)
        {
        }
    }
}
