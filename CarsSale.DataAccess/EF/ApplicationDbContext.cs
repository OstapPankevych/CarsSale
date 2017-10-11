using Microsoft.AspNet.Identity.EntityFramework;

namespace CarsSale.DataAccess.EF
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}