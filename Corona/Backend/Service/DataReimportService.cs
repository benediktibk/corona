using Backend.Repository;

namespace Backend.Service
{
    public class DataReimportService
    {
        private readonly ICsvFileRepository _csvFileRepository;
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;
        private readonly string _sourceFilePath;

        public DataReimportService(ICsvFileRepository csvFileRepository, IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, string sourceFilePath) {
            _csvFileRepository = csvFileRepository;
            _sourceFilePath = sourceFilePath;
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
        }

        public void ReimportAll(IUnitOfWork unitOfWork) {
            _infectionSpreadDataPointRepository.DeleteAll(unitOfWork);

            var files = _csvFileRepository.ListAllCsvFilesIn(_sourceFilePath);
        }
    }
}
