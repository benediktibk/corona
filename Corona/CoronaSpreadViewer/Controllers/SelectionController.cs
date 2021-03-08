using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CoronaSpreadViewer.Controllers {
    public class SelectionController : ControllerBase {
        [HttpPost]
        [Route("api/selection/apply")]
        public HttpResponseMessage Apply(CountrySelection countrySelection) {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var rootUri = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var completeUri = $"{rootUri}?countries={string.Join(",", countrySelection.SelectedCountries.Select(x => x.ToLower()))}";
            response.Headers.Location = new Uri(completeUri);
            return response;
        }
    }
}
