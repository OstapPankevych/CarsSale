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
