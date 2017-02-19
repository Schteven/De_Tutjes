using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(De_Tutjes.Startup))]
namespace De_Tutjes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
