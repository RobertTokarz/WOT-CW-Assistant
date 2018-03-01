using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WOT_CW_Assistant.Startup))]
namespace WOT_CW_Assistant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
