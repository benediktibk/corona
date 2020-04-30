using Backend;
using Backend.Repository;
using Backend.Service;
using FluentAssertions;
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
            _infectionSpreadDataPointRepository.Setup(x => x.GetAllForCountry(It.IsAny<IUnitOfWork>(), It.IsAny<CountryType>())).Returns(new List<InfectionSpreadDataPointDao> {
                new InfectionSpreadDataPointDao { InfectedTotal = 23, Date = new DateTime(2020, 2, 1), CountryId = CountryType.Albania }
            });
            _countryInhabitantsRepository.Setup(x => x.GetAllAvailable(It.IsAny<IUnitOfWork>(), It.IsAny<IReadOnlyList<CountryType>>())).Returns(new List<CountryInhabitantsDao> {
                new CountryInhabitantsDao { CountryId = CountryType.Albania, Inhabitants = 1000 }
            });

            var dataSeries = _dataSeriesService.CreateEstimatedActualInfectedPerPopulation(_unitOfWork.Object, new List<CountryType> { CountryType.Albania });

            var peak = 23.0 / 1000;
            dataSeries.Count.Should().Be(1);
            dataSeries[0].DataPoints.Count.Should().Be(21);
            dataSeries[0].DataPoints[0].XValue.Should().Be(new DateTime(2020, 1, 11));
            dataSeries[0].DataPoints[0].YValue.Should().BeApproximately(peak * 0.0001, 1e-6);
            dataSeries[0].DataPoints[1].XValue.Should().Be(new DateTime(2020, 1, 12));
            dataSeries[0].DataPoints[1].YValue.Should().BeApproximately(peak * 0.0003, 1e-6);
            dataSeries[0].DataPoints[2].XValue.Should().Be(new DateTime(2020, 1, 13));
            dataSeries[0].DataPoints[2].YValue.Should().BeApproximately(peak * 0.0009, 1e-6);
            dataSeries[0].DataPoints[3].XValue.Should().Be(new DateTime(2020, 1, 14));
            dataSeries[0].DataPoints[3].YValue.Should().BeApproximately(peak * 0.0025, 1e-6);
            dataSeries[0].DataPoints[4].XValue.Should().Be(new DateTime(2020, 1, 15));
            dataSeries[0].DataPoints[4].YValue.Should().BeApproximately(peak * 0.006, 1e-6);
            dataSeries[0].DataPoints[5].XValue.Should().Be(new DateTime(2020, 1, 16));
            dataSeries[0].DataPoints[5].YValue.Should().BeApproximately(peak * 0.0129, 1e-6);
            dataSeries[0].DataPoints[6].XValue.Should().Be(new DateTime(2020, 1, 17));
            dataSeries[0].DataPoints[6].YValue.Should().BeApproximately(peak * 0.025, 1e-6);
            dataSeries[0].DataPoints[7].XValue.Should().Be(new DateTime(2020, 1, 18));
            dataSeries[0].DataPoints[7].YValue.Should().BeApproximately(peak * 0.0434, 1e-6);
            dataSeries[0].DataPoints[8].XValue.Should().Be(new DateTime(2020, 1, 19));
            dataSeries[0].DataPoints[8].YValue.Should().BeApproximately(peak * 0.0674, 1e-5);
            dataSeries[0].DataPoints[9].XValue.Should().Be(new DateTime(2020, 1, 20));
            dataSeries[0].DataPoints[9].YValue.Should().BeApproximately(peak * 0.0938, 1e-5);
            dataSeries[0].DataPoints[10].XValue.Should().Be(new DateTime(2020, 1, 21));
            dataSeries[0].DataPoints[10].YValue.Should().BeApproximately(peak * 0.1169, 1e-5);
            dataSeries[0].DataPoints[11].XValue.Should().Be(new DateTime(2020, 1, 22));
            dataSeries[0].DataPoints[11].YValue.Should().BeApproximately(peak * 0.1306, 1e-5);
            dataSeries[0].DataPoints[12].XValue.Should().Be(new DateTime(2020, 1, 23));
            dataSeries[0].DataPoints[12].YValue.Should().BeApproximately(peak * 0.1306, 1e-5);
            dataSeries[0].DataPoints[13].XValue.Should().Be(new DateTime(2020, 1, 24));
            dataSeries[0].DataPoints[13].YValue.Should().BeApproximately(peak * 0.1169, 1e-5);
            dataSeries[0].DataPoints[14].XValue.Should().Be(new DateTime(2020, 1, 25));
            dataSeries[0].DataPoints[14].YValue.Should().BeApproximately(peak * 0.0938, 1e-5);
            dataSeries[0].DataPoints[15].XValue.Should().Be(new DateTime(2020, 1, 26));
            dataSeries[0].DataPoints[15].YValue.Should().BeApproximately(peak * 0.0674, 1e-5);
            dataSeries[0].DataPoints[16].XValue.Should().Be(new DateTime(2020, 1, 27));
            dataSeries[0].DataPoints[16].YValue.Should().BeApproximately(peak * 0.0434, 1e-6);
            dataSeries[0].DataPoints[17].XValue.Should().Be(new DateTime(2020, 1, 28));
            dataSeries[0].DataPoints[17].YValue.Should().BeApproximately(peak * 0.025, 1e-6);
            dataSeries[0].DataPoints[18].XValue.Should().Be(new DateTime(2020, 1, 29));
            dataSeries[0].DataPoints[18].YValue.Should().BeApproximately(peak * 0.0129, 1e-6);
            dataSeries[0].DataPoints[19].XValue.Should().Be(new DateTime(2020, 1, 30));
            dataSeries[0].DataPoints[19].YValue.Should().BeApproximately(peak * 0.006, 1e-6);
            dataSeries[0].DataPoints[20].XValue.Should().Be(new DateTime(2020, 1, 31));
            dataSeries[0].DataPoints[20].YValue.Should().BeApproximately(peak * 0.0038, 1e-6);
        }
    }
}
