using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lemia.Web.Startup))]
namespace Lemia.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
