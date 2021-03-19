using Backend;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CoronaSpreadViewer.Controllers {
    [ApiController]
    public class GraphController : ControllerBase {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IGraphService _graphService;

        public GraphController(IUnitOfWorkFactory unitOfWorkFactory, IGraphService graphService) {
            _unitOfWorkFactory = unitOfWorkFactory;
            _graphService = graphService;
        }

        [HttpGet]
        [Route("api/graph/infected-absolute-linear")]
        public ActionResult GetInfectedAbsoluteLinear([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateInfectedAbsoluteLinear(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/infected-absolute-logarithmic")]
        public ActionResult GetInfectedAbsoluteLogarithmic([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateInfectedAbsoluteLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/estimated-actual-new-infected-persons")]
        public ActionResult GetEstimatedActualNewInfectedPersons([FromQuery] string countries, [FromQuery] int estimationPastInDays) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateEstimatedActualNewInfectedPersons(unitOfWork, countriesParsed, estimationPastInDays);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/infected-per-population-logarithmic")]
        public ActionResult GetInfectedPerPopulationLogarithmic([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/still-infected-per-population-logarithmic")]
        public ActionResult GetStillInfectedPerPopulationLogarithmic([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateStillInfectedPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/still-infected")]
        public ActionResult GetStillInfected([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateStillInfected(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/deaths-per-population-logarithmic")]
        public ActionResult GetDeathsPerPopulationLogarithmic([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateDeathsPerPopulationLogarithmic(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/deaths")]
        public ActionResult GetDeaths([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateDeaths(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/infected-growth-per-total-infected")]
        public ActionResult GetInfectedGrowthPerTotalInfected([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfected(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/infected-growth-per-total-infected-per-population")]
        public ActionResult GetInfectedGrowthPerTotalInfectedPerPopulation([FromQuery] string countries) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                if (!TryParseCountries(countries, out var countriesParsed)) {
                    return NotFound();
                }

                var result = _graphService.CreateInfectedGrowthPerTotalInfectedPerPopulation(unitOfWork, countriesParsed);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/top-countries-by-new-deaths")]
        public ActionResult GetTopCountriesByNewDeaths([FromQuery] int topCountriesCount, [FromQuery] int daysInPast) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByNewDeaths(unitOfWork, topCountriesCount, daysInPast);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/top-countries-by-new-infections")]
        public ActionResult GetTopCountriesByNewInfections([FromQuery] int topCountriesCount, [FromQuery] int daysInPast) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByNewInfections(unitOfWork, topCountriesCount, daysInPast);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/top-countries-by-deaths-per-population")]
        public ActionResult GetTopCountriesByDeathsPerPopulation([FromQuery] int topCountriesCount) {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                var result = _graphService.CreateTopCountriesByDeathsPerPopulation(unitOfWork, topCountriesCount);
                return CreateResponse(result);
            }
        }

        [HttpGet]
        [Route("api/graph/top-countries-by-infections-per-population")]
        public ActionResult GetTopCountriesByInfectionsPerPopulation([FromQuery] int topCountriesCount) {
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

        private ContentResult CreateResponse(string svg) {
            return Content(svg, "image/svg+xml");
        }
    }
}
