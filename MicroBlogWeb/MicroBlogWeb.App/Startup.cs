using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroBlogWeb.App.Startup))]
namespace MicroBlogWeb.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
