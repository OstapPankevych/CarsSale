using System.Linq;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.DataAccess.Repositories
{
    public class RoleRepository : Repository, IRoleRepository
    {
        public Role GetRoleByName(string roleName)
        {
            using (var context = CreateContext())
            {
                var dbRole = context.ROLEs
                    .FirstOrDefault(x => x.NAME == roleName);
                return dbRole != null ? new Role(dbRole) : null;
            }
        }
    }
}