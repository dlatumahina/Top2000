using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Top2000GOED.Startup))]
namespace Top2000GOED
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
