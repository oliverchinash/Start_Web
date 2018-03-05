using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperMarket.LoginWeb.Startup))]
namespace SuperMarket.LoginWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
