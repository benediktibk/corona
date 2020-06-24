using Backend.Service;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BackendTest.Service {
    [TestClass]
    public class DataUpdateTimerServiceTest {
        private IDataUpdateTimerService _dataUpdateTimerService;

        [TestInitialize]
        public void Setup() {
            _dataUpdateTimerService = new DataUpdateTimerService();
        }

        [TestMethod]
        public void CalculateIntervalInMilliseconds_Before6_6OfThisDay() {
            var now = new DateTime(2020, 6, 24, 5, 55, 0);

            var result = _dataUpdateTimerService.CalculateIntervalInMilliseconds(now);

            result.Should().BeApproximately(5 * 60 * 1000, 1e-5);
        }

        [TestMethod]
        public void CalculateIntervalInMilliseconds_After6_6OfNextDay() {
            var now = new DateTime(2020, 6, 24, 6, 5, 0);

            var result = _dataUpdateTimerService.CalculateIntervalInMilliseconds(now);

            result.Should().BeApproximately(24 * 60 * 60 * 1000 - 5 * 60 * 1000, 1e-5);
        }
    }
}
