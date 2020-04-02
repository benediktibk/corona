using Backend;
using Backend.Service;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class DataController : ApiController
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDataReimportService _dataReimportService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DataController(IDataReimportService dataReimportService, IAuthorizationService authorizationService, IUnitOfWorkFactory unitOfWorkFactory) {
            _dataReimportService = dataReimportService;
            _authorizationService = authorizationService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpPost]
        [Route("api/data/reimport")]
        public HttpResponseMessage Reimport(Credentials credentials) {
            _logger.Info($"user {credentials.Username} tries to update the data");

            if (!_authorizationService.IsAllowedToUpdateData(credentials.Username, credentials.Password)) {
                _logger.Info($"user {credentials.Username} is not allowed to update the data");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            _logger.Info("triggering update of data");

            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                unitOfWork.BeginDatabaseTransaction();
                _dataReimportService.ReimportAll(unitOfWork);
                unitOfWork.CommitDatabaseTransaction();
            }

            _logger.Info("successfully updated data, redirecting to start page");

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var rootUri = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            response.Headers.Location = new Uri(rootUri);
            return response;
        }
    }
}
