using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CoronaSpreadViewer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            config.Services.Replace(typeof(IHttpControllerActivator), new App_Start.ServiceActivator(config));
            config.MapHttpAttributeRoutes();
        }
    }
}
