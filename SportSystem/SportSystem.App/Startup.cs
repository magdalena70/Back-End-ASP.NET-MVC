using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportSystem.App.Startup))]
namespace SportSystem.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
