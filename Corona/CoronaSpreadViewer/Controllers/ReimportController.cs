using Backend;
using Backend.Service;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoronaSpreadViewer.Controllers
{
    public class ReimportController : ApiController
    {
        private readonly IDataReimportService _dataReimportService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ReimportController(IDataReimportService dataReimportService, IAuthorizationService authorizationService, IUnitOfWorkFactory unitOfWorkFactory) {
            _dataReimportService = dataReimportService;
            _authorizationService = authorizationService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpPost]
        public HttpResponseMessage Post(Credentials credentials) {
            if (!_authorizationService.IsAllowedToUpdateData(credentials.Username, credentials.Password)) {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                unitOfWork.BeginDatabaseTransaction();
                _dataReimportService.ReimportAll(unitOfWork);
                unitOfWork.CommitDatabaseTransaction();
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
