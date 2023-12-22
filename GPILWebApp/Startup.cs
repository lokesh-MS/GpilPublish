using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GPILWebApp.Startup))]
namespace GPILWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
