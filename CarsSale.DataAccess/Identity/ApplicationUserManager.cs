using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.Identity
{
    public class ApplicationUserManager: UserManager<IdentityUser>
    {
        public ApplicationUserManager(IUserStore<IdentityUser> store)
            : base(store)
        {}

        public bool IsEmailExists(string email)
        {
            var user = Users.FirstOrDefault(x => x.Email == email);
            return user != null;
        }

        public bool IsLoginExists(string login)
        {
            var user = Users.FirstOrDefault(x => x.Logins.FirstOrDefault(l => l.LoginProvider == login) != null);
            return user != null;
        }

        public bool IsPhoneExists(string phone)
        {
            var user = Users.FirstOrDefault(x => x.PhoneNumber == phone);
            return user != null;
        }

        public IdentityUser FindByLogin(string login)
        {
            return Users.FirstOrDefault(x => x.Logins.FirstOrDefault(l => l.LoginProvider == login) != null);
        }
    }
}
