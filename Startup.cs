using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPNpaypal.Startup))]
namespace IPNpaypal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
