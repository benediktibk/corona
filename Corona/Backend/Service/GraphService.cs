using Backend.Repository;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private readonly NumericOperationsDateTimeForDatesOnly _numericOperationsDates;
        private readonly NumericOperationsDouble _numericOperationsDouble;
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;
        private readonly ICountryDetailedRepository _countryDetailedRepository;

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, ICountryDetailedRepository countryDetailedRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _countryDetailedRepository = countryDetailedRepository;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
        }

        public string CreateGraphDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.DeathsTotal > 0);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();
                var previousValue = 0;

                foreach (var dataPoint in dataPoints) {
                    var dataPointConverted = new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.DeathsTotal - previousValue) / inhabitants * 100);
                    previousValue = dataPoint.DeathsTotal;

                    if (dataPointConverted.YValue > 0) {
                        dataPointsConverted.Add(dataPointConverted);
                    }
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i));

                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]"),
                allDataSeries);
            return graph.ToSvg();
        }

        public string CreateGraphInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i));
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LinearAxis<double>(_numericOperationsDouble, "Persons"),
                allDataSeries);
            return graph.ToSvg();
        }

        public string CreateGraphInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i));
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LogarithmicAxis<double>(_numericOperationsDouble, "Persons"),
                allDataSeries);
            return graph.ToSvg();
        }

        public string CreateGraphInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, (double)dataPoint.InfectedTotal / inhabitants * 100)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i));
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]"),
                allDataSeries);
            return graph.ToSvg();
        }
    }
}
