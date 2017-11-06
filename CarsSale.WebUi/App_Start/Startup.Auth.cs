using CarsSale.DataAccess.Identity;
using CarsSale.DataAccess.Identity.Managers;
using CarsSale.WebUi.Support;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace CarsSale.WebUi
{
    public partial class Startup
    {
        #region methods

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => CarsSaleDbContext.Create(ConnectionStringBuilder.IdentityConnectionString));
            app.CreatePerOwinContext<CarsSaleUserManager>(CarsSaleUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        #endregion
    }
}