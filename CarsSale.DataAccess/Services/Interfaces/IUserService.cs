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
        User CreateUser(User user);
        User Get(string login);
        bool IsUserValid(string login, string password);

        bool IsEmailExists(string email);
        bool IsPhoneExists(string phone);
        bool IsLoginExists(string login);
    }
}
