using Backend;
using Backend.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class DataController : ApiController
    {
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
            if (!_authorizationService.IsAllowedToUpdateData(credentials.Username, credentials.Password)) {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                unitOfWork.BeginDatabaseTransaction();
                _dataReimportService.ReimportAll(unitOfWork);
                unitOfWork.CommitDatabaseTransaction();
            }

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            var rootUri = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            response.Headers.Location = new Uri(rootUri);
            return response;
        }
    }
}
