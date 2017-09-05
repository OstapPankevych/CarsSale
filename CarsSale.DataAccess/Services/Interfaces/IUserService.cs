using CarsSale.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsSale.DataAccess.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserWithRole(int userId);
        User GetUser(int userId);
        IEnumerable<Role> GetRoles();
        void CreateUser(User user);
    }
}
