using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CoronaSpreadViewer.Controllers {
    [ApiController]
    public class SelectionController : ControllerBase {
        [HttpPost]
        [Route("api/selection/apply")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Apply([FromForm]CountrySelection countrySelection) {
            var rootUri = Request.GetUri().GetLeftPart(UriPartial.Authority);
            var completeUri = $"{rootUri}?countries={string.Join(",", countrySelection.SelectedCountries.Select(x => x.ToLower()))}";
            return new RedirectResult(url: completeUri, permanent: true, preserveMethod: false);
        }
    }
}
