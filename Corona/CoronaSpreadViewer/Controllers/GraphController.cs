using Backend;
using Backend.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace CoronaSpreadViewer.Controllers
{
    public class GraphController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IGraphService _graphService;
        private readonly IGraphLegendService _graphLegendService;
        private const int CachingTimeInSecondsClient = 3600;
        private const int CachingTimeInSecondsServer = 86400;

        public GraphController(IUnitOfWorkFactory unitOfWorkFactory, IGraphService graphService, IGraphLegendService graphLegendService) {
            _unitOfWorkFactory = unitOfWorkFactory;
            _graphService = graphService;
            _graphLegendService = graphLegendService;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/legend")]
        public HttpResponseMessage GetLegend([FromUri] string[] country) {
            if (!TryParseCountries(country, out var countriesParsed)) {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var result = _graphLegendService.CreateLegend(countriesParsed);
            return CreateResponse(result);
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-absolute-linear")]
        public HttpResponseMessage GetInfectedAbsoluteLinear([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedAbsoluteLinear(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-absolute-logarithmic")]
        public HttpResponseMessage GetInfectedAbsoluteLogarithmic([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedAbsoluteLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-per-population-logarithmic")]
        public HttpResponseMessage GetInfectedPerPopulationLogarithmic([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/still-infected-per-population-logarithmic")]
        public HttpResponseMessage GetStillInfectedPerPopulationLogarithmic([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateStillInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/deaths-per-population-logarithmic")]
        public HttpResponseMessage GetDeathsPerPopulationLogarithmic([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateDeathsPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/deaths")]
        public HttpResponseMessage GetDeaths([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateDeaths(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-growth-per-total-infected")]
        public HttpResponseMessage GetInfectedGrowthPerTotalInfected([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfected(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-growth-per-total-infected-per-population")]
        public HttpResponseMessage GetInfectedGrowthPerTotalInfectedPerPopulation([FromUri] string[] country) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(country, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfectedPerPopulation(unitOfWork, countriesParsed);
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
            return response;
        }
    }
}
