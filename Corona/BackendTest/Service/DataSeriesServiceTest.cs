using Backend;
using Backend.Repository;
using Backend.Service;
using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BackendTest.Service {
    [TestClass]
    public class DataSeriesServiceTest {
        private IDataSeriesService _dataSeriesService;
        private Mock<IInfectionSpreadDataPointRepository> _infectionSpreadDataPointRepository;
        private Mock<ICountryInhabitantsRepository> _countryInhabitantsRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        [TestInitialize]
        public void Setup() {
            _infectionSpreadDataPointRepository = new Mock<IInfectionSpreadDataPointRepository>();
            _countryInhabitantsRepository = new Mock<ICountryInhabitantsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _dataSeriesService = new DataSeriesService(_infectionSpreadDataPointRepository.Object, _countryInhabitantsRepository.Object);
        }

        [TestMethod]
        public void CreateEstimatedActualInfectedPerPopulation_OneDataPoint_NormalDistributionInPast() {
            _infectionSpreadDataPointRepository.Setup(x => x.GetAllForCountryOrderedByDate(It.IsAny<IUnitOfWork>(), It.IsAny<CountryType>())).Returns(new List<InfectionSpreadDataPointDao> {
                new InfectionSpreadDataPointDao { InfectedTotal = 23, Date = new DateTime(2020, 2, 1), CountryId = CountryType.Albania }
            });
            _countryInhabitantsRepository.Setup(x => x.GetAllAvailable(It.IsAny<IUnitOfWork>(), It.IsAny<IReadOnlyList<CountryType>>())).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao { CountryId = CountryType.Albania, Inhabitants = 1000 }
            });

            var dataSeries = _dataSeriesService.CreateEstimatedActualNewInfectedPersons(_unitOfWork.Object, new List<CountryType> { CountryType.Albania }, 21);

            var normalDistribution = new NormalDistribution(10, 3);
            dataSeries.Count.Should().Be(2);
            dataSeries[0].DataPoints.Count.Should().Be(8);
            dataSeries[0].DataPoints[0].XValue.Should().Be(new DateTime(2020, 1, 11));
            dataSeries[0].DataPoints[0].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(21, 1e10), 1e-5);
            dataSeries[0].DataPoints[1].XValue.Should().Be(new DateTime(2020, 1, 12));
            dataSeries[0].DataPoints[1].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(20, 21), 1e-5);
            dataSeries[0].DataPoints[2].XValue.Should().Be(new DateTime(2020, 1, 13));
            dataSeries[0].DataPoints[2].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(19, 20), 1e-5);
            dataSeries[0].DataPoints[3].XValue.Should().Be(new DateTime(2020, 1, 14));
            dataSeries[0].DataPoints[3].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(18, 19), 1e-5);
            dataSeries[0].DataPoints[4].XValue.Should().Be(new DateTime(2020, 1, 15));
            dataSeries[0].DataPoints[4].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(17, 18), 1e-5);
            dataSeries[0].DataPoints[5].XValue.Should().Be(new DateTime(2020, 1, 16));
            dataSeries[0].DataPoints[5].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(16, 17), 1e-5);
            dataSeries[0].DataPoints[6].XValue.Should().Be(new DateTime(2020, 1, 17));
            dataSeries[0].DataPoints[6].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(15, 16), 1e-5);
            dataSeries[0].DataPoints[7].XValue.Should().Be(new DateTime(2020, 1, 18));
            dataSeries[0].DataPoints[7].YValue.Should().BeApproximately(23 * normalDistribution.CalculateSumBetween(14, 15), 1e-5);
            dataSeries[1].DataPoints.Count.Should().Be(1);
            dataSeries[1].DataPoints[0].XValue.Should().Be(new DateTime(2020, 2, 1));
            dataSeries[1].DataPoints[0].YValue.Should().BeApproximately(23, 1e-6);
        }

        [TestMethod]
        public void CreateHighestAverageNewInfectionsPerPopulationRecently_OneCountry_CorrectValueForOnlyCountry() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageNewInfectionsPerPopulationRecently(_unitOfWork.Object, 10, 5);

            dataSeries.DataPoints.Count.Should().Be(1);
            dataSeries.DataPoints[0].XValue.Should().Be(CountryType.Austria);
            dataSeries.DataPoints[0].YValue.Should().BeApproximately((20.0 - 16.0) / 1000.0 / 3.0, 1e-10);
        }

        [TestMethod]
        public void CreateHighestAverageDeathsPerPopulationRecently_OneCountry_CorrectValueForOnlyCountry() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageDeathsPerPopulationRecently(_unitOfWork.Object, 10, 5);

            dataSeries.DataPoints.Count.Should().Be(1);
            dataSeries.DataPoints[0].XValue.Should().Be(CountryType.Austria);
            dataSeries.DataPoints[0].YValue.Should().BeApproximately((10.0 - 9.0) / 1000.0 / 3.0, 1e-10);
        }

        [TestMethod]
        public void CreateHighestAverageDeathsPerPopulationRecently_OneCountryWithNegativeValue_NoDataPointsAtAll() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 90,
                    InfectedTotal = 160,
                    RecoveredTotal = 30
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageDeathsPerPopulationRecently(_unitOfWork.Object, 10, 5);

            dataSeries.DataPoints.Count.Should().Be(0);
        }

        [TestMethod]
        public void CreateHighestAverageNewInfectionsPerPopulationRecently_OneCountryWithNegativeValue_NoDataPointsAtAll() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 90,
                    InfectedTotal = 160,
                    RecoveredTotal = 30
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageNewInfectionsPerPopulationRecently(_unitOfWork.Object, 10, 5);

            dataSeries.DataPoints.Count.Should().Be(0);
        }

        [TestMethod]
        public void CreateHighestAverageNewInfectionsPerPopulationRecently_ThreeCountriesAvailableButOnlyTopTwo_CorrectValueForResultCountries() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                },
                new CountryInhabitantsDao {
                    CountryId = CountryType.Australia,
                    Inhabitants = 10000
                },
                new CountryInhabitantsDao {
                    CountryId = CountryType.Afghanistan,
                    Inhabitants = 888
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Australia)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Australia,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Australia, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Australia,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Afghanistan)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Afghanistan,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Afghanistan, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Afghanistan,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageNewInfectionsPerPopulationRecently(_unitOfWork.Object, 2, 5);

            dataSeries.DataPoints.Count.Should().Be(2);
            dataSeries.DataPoints[0].XValue.Should().Be(CountryType.Afghanistan);
            dataSeries.DataPoints[0].YValue.Should().BeApproximately((20.0 - 16.0) / 888.0 / 3.0, 1e-10);
            dataSeries.DataPoints[1].XValue.Should().Be(CountryType.Austria);
            dataSeries.DataPoints[1].YValue.Should().BeApproximately((20.0 - 16.0) / 1000.0 / 3.0, 1e-10);
        }

        [TestMethod]
        public void CreateHighestAverageDeathsPerPopulationRecently_ThreeCountriesAvailableButOnlyTopTwo_CorrectValueForResultCountries() {
            _countryInhabitantsRepository.Setup(x => x.GetAll(_unitOfWork.Object)).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 1000
                },
                new CountryInhabitantsDao {
                    CountryId = CountryType.Australia,
                    Inhabitants = 10000
                },
                new CountryInhabitantsDao {
                    CountryId = CountryType.Afghanistan,
                    Inhabitants = 888
                }
            });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDateTime(_unitOfWork.Object)).Returns(new DateTime(2020, 3, 18));
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Austria)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Austria, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Austria,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Australia)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Australia,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Australia, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Australia,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetMostRecentDataPoint(_unitOfWork.Object, CountryType.Afghanistan)).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Afghanistan,
                    Date = new DateTime(2020, 3, 18),
                    DeathsTotal = 10,
                    InfectedTotal = 20,
                    RecoveredTotal = 5
                });
            _infectionSpreadDataPointRepository.Setup(x => x.GetLastDataPointBefore(_unitOfWork.Object, CountryType.Afghanistan, It.IsAny<DateTime>())).Returns(
                new InfectionSpreadDataPointDao {
                    CountryId = CountryType.Afghanistan,
                    Date = new DateTime(2020, 3, 15),
                    DeathsTotal = 9,
                    InfectedTotal = 16,
                    RecoveredTotal = 3
                });

            var dataSeries = _dataSeriesService.CreateHighestAverageDeathsPerPopulationRecently(_unitOfWork.Object, 2, 5);

            dataSeries.DataPoints.Count.Should().Be(2);
            dataSeries.DataPoints[0].XValue.Should().Be(CountryType.Afghanistan);
            dataSeries.DataPoints[0].YValue.Should().BeApproximately((10.0 - 9.0) / 888.0 / 3.0, 1e-10);
            dataSeries.DataPoints[1].XValue.Should().Be(CountryType.Austria);
            dataSeries.DataPoints[1].YValue.Should().BeApproximately((10.0 - 9.0) / 1000.0 / 3.0, 1e-10);
        }
    }
}
