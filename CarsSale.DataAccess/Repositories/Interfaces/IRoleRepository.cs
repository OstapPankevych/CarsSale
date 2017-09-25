using CarsSale.DataAccess.DTO;

namespace CarsSale.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role GetRoleByName(string roleName);
    }
}