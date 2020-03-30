using Backend.Service;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class GraphController : ApiController
    {
        private readonly IGraphService _graphService;

        public GraphController(IGraphService graphService) {
            _graphService = graphService;
        }

        [HttpGet]
        public HttpResponseMessage Get(string id) {
            var result = _graphService.CreateGraph();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/svg+xml");
            response.Content.Headers.ContentEncoding.Add("utf-8");
            return response;
        }
    }
}
