using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class GraphController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string id) {
            var result = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""100"" width=""100"">
  <circle cx=""50"" cy=""50"" r=""40"" stroke=""black"" stroke-width=""3"" fill=""red"" />
</svg>";
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/svg");
            response.Content.Headers.ContentEncoding.Add("utf-8");
            return response;
        }
    }
}
