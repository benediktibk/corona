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
        public HttpResponseMessage Get([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var countries = new List<CountryType>();

                foreach (var countrySingle in country) {
                    if (!Enum.TryParse<CountryType>(countrySingle, true, out var countryParsed)) {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    countries.Add(countryParsed);
                }

                var result = _graphService.CreateGraphInfectedAbsoluteLinear(unitOfWork, countries);

                if (string.IsNullOrEmpty(result)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(result);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/svg+xml");
                response.Content.Headers.ContentEncoding.Add("utf-8");
                return response;
            }
        }
    }
}
