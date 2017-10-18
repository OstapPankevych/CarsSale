using System.Security.Claims;
using System.Threading.Tasks;
using CarsSale.DataAccess.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.Entities
{
    public class ApplicationUser: IdentityUser<int, ApplicationLogin, ApplicationUserRole, ApplicationClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}