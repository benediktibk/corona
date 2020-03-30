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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ReimportController(IDataReimportService dataReimportService, IUnitOfWorkFactory unitOfWorkFactory) {
            _dataReimportService = dataReimportService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [HttpPost]
        public HttpResponseMessage Post() {
            using (var unitOfWork = _unitOfWorkFactory.Create()) {
                unitOfWork.BeginDatabaseTransaction();
                _dataReimportService.ReimportAll(unitOfWork);
                unitOfWork.CommitDatabaseTransaction();
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
