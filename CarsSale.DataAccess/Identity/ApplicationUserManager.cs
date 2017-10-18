using System.Linq;
using CarsSale.DataAccess.EF;
using CarsSale.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CarsSale.DataAccess.Identity
{
    public class ApplicationUserManager: UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
        }

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

        public ApplicationUser FindByLogin(string login)
        {
            return Users.FirstOrDefault(x => x.UserName == login);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var db = context.Get<ApplicationDbContext>();
            var store = new UserStore<ApplicationUser,
                ApplicationRole,
                int,
                ApplicationLogin,
                ApplicationUserRole,
                ApplicationClaim>(db);
            var manager = new ApplicationUserManager(store);
            return manager;
        }
    }
}
