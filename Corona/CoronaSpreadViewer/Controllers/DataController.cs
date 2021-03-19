using Backend;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CoronaSpreadViewer.Controllers {
    [ApiController]
    public class DataController : ControllerBase {
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
        public ActionResult Reimport(Credentials credentials) {
            _logger.Info($"user {credentials.Username} tries to update the data");

            if (!_authorizationService.IsAllowedToUpdateData(credentials.Username, credentials.Password)) {
                _logger.Info($"user {credentials.Username} is not allowed to update the data");
                return new UnauthorizedResult();
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
                return new StatusCodeResult(500);
            }

            _logger.Info("successfully updated data, invalidating the server side cache");
            // @TODO invalidate server side cache

            _logger.Info("responding with redirect to start page");
            var rootUri = Request.GetUri().GetLeftPart(UriPartial.Authority);
            return new RedirectResult(url: rootUri, permanent: true, preserveMethod: false);
        }
    }
}
