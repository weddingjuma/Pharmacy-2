using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PharmacistHelper.Startup))]
namespace PharmacistHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
