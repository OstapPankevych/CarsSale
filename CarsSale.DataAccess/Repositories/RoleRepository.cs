using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class RoleRepository : Repository<ROLE, int>, IRoleRepository
    {
        public RoleRepository(CarsSaleEntities context)
            : base(context.ROLEs)
        {
        }
    }
}