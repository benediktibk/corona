using Backend;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Net;
using System.Net.Http;

namespace CoronaSpreadViewer.Controllers {
    public class DataController : ControllerBase {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDataReimportService _dataReimportService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IServerSideCache _serverSideCache;

        public DataController(IDataReimportService dataReimportService, IAuthorizationService authorizationService, IUnitOfWorkFactory unitOfWorkFactory, IServerSideCache serverSideCache) {
            _dataReimportService = dataReimportService;
            _authorizationService = authorizationService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _serverSideCache = serverSideCache;
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
            bool importSuccess;

            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                unitOfWork.BeginDatabaseTransaction();
                importSuccess = _dataReimportService.ReimportAll(unitOfWork);
                unitOfWork.CommitDatabaseTransaction();
            }

            if (!importSuccess) {
                _logger.Error("failed to update data");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "unable to update data");
            }

            _logger.Info("successfully updated data, invalidating the server side cache");
            _serverSideCache.Invalidate();

            _logger.Info("responding with redirect to start page");
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var rootUri = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            response.Headers.Location = new Uri(rootUri);
            return response;
        }
    }
}
