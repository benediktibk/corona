using Backend;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebApi.OutputCache.V2;

namespace CoronaSpreadViewer {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            var serviceActivator = new App_Start.ServiceActivator();
            config.Services.Replace(typeof(IHttpControllerActivator), serviceActivator);
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            var serverSideCache = serviceActivator.Container.GetInstance<IServerSideCache>();
            config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new ServerSideCacheWrapper(serverSideCache));
        }
    }
}
