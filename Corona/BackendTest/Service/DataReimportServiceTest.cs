using Backend;
using Backend.Repository;
using Backend.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

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
        private CsvFileRepository _realCsvFileRepository;

        [TestInitialize]
        public void Setup() {
            _csvFileRepository = new Mock<ICsvFileRepository>();
            _infectionSpreadDataPointRepository = new Mock<IInfectionSpreadDataPointRepository>();
            _gitRepository = new Mock<IGitRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _dataReimportService = new DataReimportService(_csvFileRepository.Object, _infectionSpreadDataPointRepository.Object, _gitRepository.Object, "filePath", "gitRepo");
            _realCsvFileRepository = new CsvFileRepository();
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
                    It.Is<InfectionSpreadDataPointDao>(y => 
                        y.Country == CountryType.SouthKorea && 
                        y.InfectedTotal == 1 && 
                        y.DeathsTotal == 0 && 
                        y.RecoveredTotal == 0)), 
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
                    It.Is<InfectionSpreadDataPointDao>(y =>
                        y.Country == CountryType.China &&
                        y.InfectedTotal == 549 &&
                        y.DeathsTotal == 17 &&
                        y.RecoveredTotal == 28)),
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
                    It.Is<InfectionSpreadDataPointDao>(y =>
                        y.Country == CountryType.Taiwan &&
                        y.InfectedTotal == 1 &&
                        y.DeathsTotal == 0 &&
                        y.RecoveredTotal == 0)),
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
                    It.Is<InfectionSpreadDataPointDao>(y =>
                        y.Country == CountryType.Usa &&
                        y.InfectedTotal == 1 &&
                        y.DeathsTotal == 0 &&
                        y.RecoveredTotal == 0)),
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
                    It.Is<InfectionSpreadDataPointDao>(y =>
                        y.Country == CountryType.Japan &&
                        y.InfectedTotal == 2 &&
                        y.DeathsTotal == 0 &&
                        y.RecoveredTotal == 0)),
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
                    It.Is<InfectionSpreadDataPointDao>(y =>
                        y.Country == CountryType.Thailand &&
                        y.InfectedTotal == 2 &&
                        y.DeathsTotal == 0 &&
                        y.RecoveredTotal == 0)),
                Times.Once);
        }
    }
}
