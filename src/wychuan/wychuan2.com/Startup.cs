using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wychuan2.com.Startup))]
namespace wychuan2.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
