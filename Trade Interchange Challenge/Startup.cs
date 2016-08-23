using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trade_Interchange_Challenge.Startup))]
namespace Trade_Interchange_Challenge
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
