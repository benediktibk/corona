using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class SelectionController : ApiController
    {
        public SelectionController() {
        }

        [HttpPost]
        [Route("api/selection/apply")]
        public HttpResponseMessage Apply(string[] selectedCountries) {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var rootUri = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            response.Headers.Location = new Uri(rootUri);
            return response;
        }
    }
}
