using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_88DSL.Startup))]
namespace _88DSL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
