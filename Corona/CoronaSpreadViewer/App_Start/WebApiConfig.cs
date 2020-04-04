using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebApi.OutputCache.V2;

namespace CoronaSpreadViewer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            config.Services.Replace(typeof(IHttpControllerActivator), new App_Start.ServiceActivator(config));
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new ServerSideCache());
        }
    }
}
