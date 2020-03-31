using Backend;
using Backend.Repository;
using Backend.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BackendTest.Service
{
    [TestClass]
    public class DataReimportServiceTest
    {
        private DataReimportService _dataReimportService;
        private Mock<ICsvFileRepository> _csvFileRepository;
        private Mock<IInfectionSpreadDataPointRepository> _infectionSpreadDataPointRepository;
        private Mock<IGitRepository> _gitRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ISettings> _settings;
        private CsvFileRepository _realCsvFileRepository;

        [TestInitialize]
        public void Setup() {
            _csvFileRepository = new Mock<ICsvFileRepository>();
            _infectionSpreadDataPointRepository = new Mock<IInfectionSpreadDataPointRepository>();
            _gitRepository = new Mock<IGitRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _settings = new Mock<ISettings>();
            _dataReimportService = new DataReimportService(_csvFileRepository.Object, _infectionSpreadDataPointRepository.Object, _gitRepository.Object, _settings.Object);
            _realCsvFileRepository = new CsvFileRepository();

            _gitRepository.Setup(x => x.FetchLatestCommit(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        }

        [TestMethod]
        public void ReimportAll_NoFilesAtAll_DeleteAllGotCalled() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string>());

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.DeleteAll(_unitOfWork.Object),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForSouthKorea() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object, 
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z => 
                        z.CountryId == CountryType.SouthKorea && 
                        z.InfectedTotal == 1 && 
                        z.DeathsTotal == 0 && 
                        z.RecoveredTotal == 0))), 
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForChina() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.China &&
                        z.InfectedTotal == 548 &&
                        z.DeathsTotal == 17 &&
                        z.RecoveredTotal == 28))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForTaiwan() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Taiwan &&
                        z.InfectedTotal == 1 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 0))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForUsa() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Usa &&
                        z.InfectedTotal == 1 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 0))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForJapan() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Japan &&
                        z.InfectedTotal == 2 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 0))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectValueForThailand() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Thailand &&
                        z.InfectedTotal == 2 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 0))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileType_CorrectTimestampForThailand() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/01-22-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Thailand &&
                        z.Date == new System.DateTime(2020, 1, 22, 17, 0, 0)))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileType_CorrectValueForUsa() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Usa &&
                        z.InfectedTotal == 101657 &&
                        z.DeathsTotal == 1581 &&
                        z.RecoveredTotal == 869))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileType_CorrectTimestampForUsa() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Usa &&
                        z.Date == new System.DateTime(2020, 3, 27, 22, 14, 55)))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileType_CorrectValueForAustria() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Austria &&
                        z.InfectedTotal == 7657 &&
                        z.DeathsTotal == 58 &&
                        z.RecoveredTotal == 225))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileTypeReducedOnAustria_CorrectValueForAustria() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020_austriaOnly.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Austria &&
                        z.InfectedTotal == 7657 &&
                        z.DeathsTotal == 58 &&
                        z.RecoveredTotal == 225))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileTypeReducedOnSouthKorea_CorrectValueForSouthKorea() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020_southKoreaOnly.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.SouthKorea &&
                        z.InfectedTotal == 9332 &&
                        z.DeathsTotal == 139 &&
                        z.RecoveredTotal == 4528))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_NewFileType_EveryCountryGotImported() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/03-27-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Count() == 176)),
                Times.Once());
        }

        [TestMethod]
        public void ReimportAll_OldFileTypeWithDiamondPrincess_CorrectValueForUsa() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/02-25-2020.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Usa &&
                        z.InfectedTotal == 10052 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 105))),
                Times.Once);
        }

        [TestMethod]
        public void ReimportAll_OldFileTypeWithDiamondPrincessInTxOnly_CorrectValueForUsa() {
            _csvFileRepository.Setup(x => x.ListAllCsvFilesIn(It.IsAny<string>())).Returns(new List<string> { "blub" });
            _csvFileRepository.Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(_realCsvFileRepository.ReadFile("testdata/02-25-2020_txDiamondOnly.csv"));

            _dataReimportService.ReimportAll(_unitOfWork.Object);

            _infectionSpreadDataPointRepository.Verify(
                x => x.Insert(
                    _unitOfWork.Object,
                    It.Is<IReadOnlyList<InfectionSpreadDataPointDao>>(y => y.Any(z =>
                        z.CountryId == CountryType.Usa &&
                        z.InfectedTotal == 9999 &&
                        z.DeathsTotal == 0 &&
                        z.RecoveredTotal == 0))),
                Times.Once);
        }
    }
}
