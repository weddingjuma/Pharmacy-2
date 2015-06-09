using Microsoft.Owin;
using Owin;
using Pharmacy;

[assembly: OwinStartup(typeof (Startup))]

namespace Pharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}