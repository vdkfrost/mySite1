using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitch_core.Startup))]
namespace Twitch_core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
