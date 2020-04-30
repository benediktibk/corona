using Backend;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers {
    public class ServerSideCacheController : ApiController {
        private readonly IServerSideCache _serverSideCache;

        public ServerSideCacheController(IServerSideCache serverSideCache) {
            _serverSideCache = serverSideCache;
        }

        [HttpGet]
        [Route("api/serversidecache")]
        public HttpResponseMessage Get() {
            ;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<html>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("<head>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("<title>");
            stringBuilder.Append("server side cache");
            stringBuilder.Append("</title>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("</head>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("<body>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("<ul>");
            stringBuilder.Append(System.Environment.NewLine);

            foreach (var key in _serverSideCache.AllKeys) {
                stringBuilder.Append("<li>");
                stringBuilder.Append(key);
                stringBuilder.Append("</li>");
                stringBuilder.Append(System.Environment.NewLine);
            }

            stringBuilder.Append("</ul>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("</body>");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append("</html>");

            var response = new HttpResponseMessage();
            response.Content = new StringContent(stringBuilder.ToString());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
