using System.Security.Claims;
using System.Threading.Tasks;
using CarsSale.DataAccess.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.Identity.Entities
{
    public class CarsSaleUser: IdentityUser<int, CarsSaleLogin, CarsSaleUserRole, CarsSaleClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(CarsSaleUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}