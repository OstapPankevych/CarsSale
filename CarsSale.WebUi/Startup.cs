using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarsSale.WebUi.Startup))]
namespace CarsSale.WebUi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
