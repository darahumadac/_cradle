using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cradle.Startup))]
namespace Cradle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
