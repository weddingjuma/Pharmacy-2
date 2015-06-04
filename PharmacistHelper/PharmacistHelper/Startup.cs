using Microsoft.Owin;
using Owin;
using PharmacistHelper;

[assembly: OwinStartup(typeof (Startup))]

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