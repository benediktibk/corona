using NConfig;
using System.Web.Http;

namespace CoronaSpreadViewer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static WebApiApplication() {
            NConfigurator.UsingFiles("Config\\Corona.config").SetAsSystemDefault();
        }

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
