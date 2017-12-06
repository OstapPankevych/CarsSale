using System.Linq;
using System.Runtime.CompilerServices;
using CarsSale.DataAccess.Identity.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CarsSale.DataAccess.Identity.Managers
{
    public class CarsSaleUserManager: UserManager<CarsSaleUser, int>
    {
        public CarsSaleUserManager(IUserStore<CarsSaleUser, int> store)
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
            var user = Users.FirstOrDefault(x => x.UserName == login);
            return user != null;
        }

        public bool IsPhoneExists(string phone)
        {
            var user = Users.FirstOrDefault(x => x.PhoneNumber == phone);
            return user != null;
        }

        public CarsSaleUser FindByLogin(string login)
        {
            return Users.FirstOrDefault(x => x.UserName == login);
        }

        public static CarsSaleUserManager Create(IdentityFactoryOptions<CarsSaleUserManager> options, IOwinContext context)
        {
            return Create(context);
        }

        public static CarsSaleUserManager Create(
            IdentityFactoryOptions<CarsSaleUserManager> options,
            IOwinContext context,
            IIdentityValidator<string> passwordValidator)
        {
            var manager = Create(context);
            manager.PasswordValidator = passwordValidator;
            return manager;
        }

        private static CarsSaleUserManager Create(IOwinContext context)
        {
            var db = context.Get<CarsSaleDbContext>();
            var store = new UserStore<CarsSaleUser,
                CarsSaleRole,
                int,
                CarsSaleLogin,
                CarsSaleUserRole,
                CarsSaleClaim>(db);
            var manager = new CarsSaleUserManager(store);
            return manager;
        }
    }
}
