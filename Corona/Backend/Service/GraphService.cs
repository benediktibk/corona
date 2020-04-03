using Backend.Repository;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private const int _graphWidth = 1000;
        private const int _graphHeight = 500;
        private readonly NumericOperationsDateTimeForDatesOnly _numericOperationsDates;
        private readonly NumericOperationsDouble _numericOperationsDouble;
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;
        private readonly ICountryDetailedRepository _countryDetailedRepository;
        private readonly IAxis<DateTime> _dateAxis;
        private readonly IAxis<double> _linearPersonAxis;
        private readonly IAxis<double> _logarithmicPersonAxis;
        private readonly IAxis<double> _logarithmicPersonPerPopulationAxis;

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, ICountryDetailedRepository countryDetailedRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _countryDetailedRepository = countryDetailedRepository;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
            _dateAxis = new LinearAxisDateTime(_numericOperationsDates, "Date");
            _linearPersonAxis = new LinearAxisDouble(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonPerPopulationAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]", "P5");
        }

        public string CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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
                    var dataPointConverted = new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.DeathsTotal - previousValue) / inhabitants);
                    previousValue = dataPoint.DeathsTotal;

                    if (dataPointConverted.YValue > 0) {
                        dataPointsConverted.Add(dataPointConverted);
                    }
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);

                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();
                var previousValue = 0;

                foreach (var dataPoint in dataPoints) {
                    var dataPointConverted = new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.DeathsTotal - previousValue));
                    previousValue = dataPoint.DeathsTotal;
                    dataPointsConverted.Add(dataPointConverted);
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);

                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, (double)dataPoint.InfectedTotal / inhabitants)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.InfectedTotal - dataPoint.DeathsTotal - dataPoint.RecoveredTotal) / inhabitants)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true);
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<double, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = new List<DataPoint<double, double>>();
                var previousInfected = 0.0;

                foreach (var dataPoint in dataPoints) {
                    var x = dataPoint.InfectedTotal;
                    var y = dataPoint.InfectedTotal - previousInfected;

                    previousInfected = dataPoint.InfectedTotal;

                    if (y <= 0) {
                        continue;
                    }

                    dataPointsConverted.Add(new DataPoint<double, double>(x, y));
                }

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false);
                allDataSeries.Add(dataSeries);
            }

            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Total", "F0");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Growth", "F0");

            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries);
            return graph.ToSvg();
        }

        public string CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<double, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);
            
            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = new List<DataPoint<double, double>>();
                var previousInfected = 0.0;

                foreach (var dataPoint in dataPoints) {
                    var x = (double)dataPoint.InfectedTotal / inhabitants;
                    var y = (dataPoint.InfectedTotal - previousInfected) / inhabitants;

                    previousInfected = dataPoint.InfectedTotal;

                    if (y <= 0) {
                        continue;
                    }

                    dataPointsConverted.Add(new DataPoint<double, double>(x, y));
                }

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false);
                allDataSeries.Add(dataSeries);
            }

            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Total [%]", "P5");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Growth [%]", "P5");

            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries);
            return graph.ToSvg();
        }
    }
}
