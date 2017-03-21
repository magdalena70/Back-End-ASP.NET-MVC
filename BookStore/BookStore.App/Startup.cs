using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookStore.App.Startup))]
namespace BookStore.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
