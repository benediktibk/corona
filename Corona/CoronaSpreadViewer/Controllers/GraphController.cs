using Backend;
using Backend.Service;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class GraphController : ApiController
    {
        private readonly IGraphService _graphService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GraphController(IUnitOfWorkFactory unitOfWorkFactory, IGraphService graphService) {
            _unitOfWorkFactory = unitOfWorkFactory;
            _graphService = graphService;
        }

        [HttpGet]
        [Route("api/graph/infected-absolute-linear")]
        public HttpResponseMessage GetInfectedAbsoluteLinear([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateGraphInfectedAbsoluteLinear(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/infected-absolute-logarithmic")]
        public HttpResponseMessage GetInfectedAbsoluteLogarithmic([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateGraphInfectedAbsoluteLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        private bool TryParseCountries(string[] country, out List<CountryType> result) {
            result = new List<CountryType>();

            foreach (var countrySingle in country) {
                if (!Enum.TryParse<CountryType>(countrySingle, true, out var countryParsed)) {
                    return false;
                }

                result.Add(countryParsed);
            }

            return true;
        }

        private HttpResponseMessage CreateResponse(string svg) {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(svg);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/svg+xml");
            response.Content.Headers.ContentEncoding.Add("utf-8");
            return response;
        }
    }
}
