using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Presentation.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //ConfigureAuth(app);
        }
    }
}
