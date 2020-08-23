using Backend;
using Backend.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace CoronaSpreadViewer.Controllers {
    public class GraphController : ApiController {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IGraphService _graphService;
#if DEBUG
        private const int CachingTimeInSecondsClient = 0;
        private const int CachingTimeInSecondsServer = 0;
#else
        private const int CachingTimeInSecondsClient = 0;
        private const int CachingTimeInSecondsServer = 86400;
#endif

        public GraphController(IUnitOfWorkFactory unitOfWorkFactory, IGraphService graphService) {
            _unitOfWorkFactory = unitOfWorkFactory;
            _graphService = graphService;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-absolute-linear")]
        public HttpResponseMessage GetInfectedAbsoluteLinear([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedAbsoluteLinear(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-absolute-logarithmic")]
        public HttpResponseMessage GetInfectedAbsoluteLogarithmic([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedAbsoluteLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/estimated-actual-new-infected-persons")]
        public HttpResponseMessage GetEstimatedActualNewInfectedPersons([FromUri] string countries, [FromUri] int estimationPastInDays) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateEstimatedActualNewInfectedPersons(unitOfWork, countriesParsed, estimationPastInDays);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-per-population-logarithmic")]
        public HttpResponseMessage GetInfectedPerPopulationLogarithmic([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/still-infected-per-population-logarithmic")]
        public HttpResponseMessage GetStillInfectedPerPopulationLogarithmic([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateStillInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/still-infected")]
        public HttpResponseMessage GetStillInfected([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateStillInfected(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/deaths-per-population-logarithmic")]
        public HttpResponseMessage GetDeathsPerPopulationLogarithmic([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateDeathsPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/deaths")]
        public HttpResponseMessage GetDeaths([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateDeaths(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-growth-per-total-infected")]
        public HttpResponseMessage GetInfectedGrowthPerTotalInfected([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfected(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/infected-growth-per-total-infected-per-population")]
        public HttpResponseMessage GetInfectedGrowthPerTotalInfectedPerPopulation([FromUri] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfectedPerPopulation(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/top-countries-by-new-deaths")]
        public HttpResponseMessage GetTopCountriesByNewDeaths([FromUri] int topCountriesCount, [FromUri] int daysInPast) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByNewDeaths(unitOfWork, topCountriesCount, daysInPast);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/top-countries-by-new-infections")]
        public HttpResponseMessage GetTopCountriesByNewInfections([FromUri] int topCountriesCount, [FromUri] int daysInPast) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByNewInfections(unitOfWork, topCountriesCount, daysInPast);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/top-countries-by-deaths-per-population")]
        public HttpResponseMessage GetTopCountriesByDeathsPerPopulation([FromUri] int topCountriesCount) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByDeathsPerPopulation(unitOfWork, topCountriesCount);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CachingTimeInSecondsClient, ServerTimeSpan = CachingTimeInSecondsServer)]
        [Route("api/graph/top-countries-by-infections-per-population")]
        public HttpResponseMessage GetTopCountriesByInfectionsPerPopulation([FromUri] int topCountriesCount) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByInfectionsPerPopulation(unitOfWork, topCountriesCount);
                return CreateResponse(result);
            }
        }

        private bool TryParseCountries(string countries, out List<CountryType> result) {
            result = new List<CountryType>();

            foreach (var countrySingle in countries.Split(',')) {
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
