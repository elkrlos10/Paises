using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Paises.Backend.Startup))]
namespace Paises.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
