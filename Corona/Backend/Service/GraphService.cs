using Backend.Repository;
using Math;
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
        private readonly ICountryInhabitantsRepository _countryDetailedRepository;
        private readonly IAxis<DateTime> _dateAxis;
        private readonly IAxis<double> _linearPersonAxis;
        private readonly IAxis<double> _logarithmicPersonAxis;
        private readonly IAxis<double> _logarithmicPersonPerPopulationAxis;
        private readonly bool _compressed;

        public GraphService(ISettings settings, IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, ICountryInhabitantsRepository countryDetailedRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _countryDetailedRepository = countryDetailedRepository;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
            _dateAxis = new LinearAxisDateTime(_numericOperationsDates, "Date");
            _linearPersonAxis = new LinearAxisDouble(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonPerPopulationAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]", "P5");
            _compressed = settings.SvgCompressed;
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

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());

                allDataSeries.Add(dataSeries);
            }

            var referenceLines = new List<ReferenceLine<double>>();
            referenceLines.Add(new ReferenceLine<double>((21.5 - 18) / 7 / 100000, "influenza based excess mortality season 2016/2017", new Color(102, 0, 51)));
            referenceLines.Add(new ReferenceLine<double>(10.3/1000/365, "general mortality EU-28", new Color(102, 0, 51)));

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, referenceLines, true, true, new Point(0.4, 0.8));
            return ConvertGraphToSvg(graph);
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

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());

                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
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
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
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
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();

                foreach (var dataPoint in dataPoints) {
                    var value = (double)(dataPoint.InfectedTotal - dataPoint.DeathsTotal - dataPoint.RecoveredTotal) / inhabitants;

                    if (value <= 0) {
                        continue;
                    }

                    dataPointsConverted.Add(new DataPoint<DateTime, double>(dataPoint.Date, value));
                }
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
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

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Total", "F0");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Growth", "F0");

            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries, true, true, new Point(0.2, 0.6));
            return ConvertGraphToSvg(graph);
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

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Total [%]", "P5");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Growth [%]", "P5");

            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries, true, true, new Point(0.2, 0.6));
            return ConvertGraphToSvg(graph);
        }

        public string CreateEstimatedActualInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var timeRangeStart = dataPoints.Select(x => x.Date).Min().Date;
                var timeRangeEnd = dataPoints.Select(x => x.Date).Max().Date;
                var dataPointsPerDay = new Dictionary<DateTime, int>();

                foreach (var dataPoint in dataPoints.OrderBy(x => x.Date)) {
                    if (!dataPointsPerDay.ContainsKey(dataPoint.Date.Date)) {
                        dataPointsPerDay.Add(dataPoint.Date.Date, dataPoint.InfectedTotal);
                    }
                    else {
                        dataPointsPerDay[dataPoint.Date.Date] = dataPoint.InfectedTotal;
                    }
                }

                var additionalInfected = new List<DataPoint<DateTime, double>>();
                var previousInfected = 0;

                for (var t = timeRangeStart; t <= timeRangeEnd; t = t.AddDays(1)) {
                    if (!dataPointsPerDay.TryGetValue(t, out int value)) {
                        additionalInfected.Add(new DataPoint<DateTime, double>(t, 0));
                        continue;
                    }

                    var nextValue = System.Math.Max(0, value - previousInfected);

                    additionalInfected.Add(new DataPoint<DateTime, double>(t, nextValue));
                    previousInfected = value;
                }

                var graphTimeRangeStart = timeRangeStart.AddDays(-21);
                var graphTimeRangeEnd = timeRangeEnd;
                var graphTimeLengthInDays = (int)(graphTimeRangeEnd - graphTimeRangeStart).TotalDays + 1;
                var values = new double[graphTimeLengthInDays];
                var distribution = new NormalDistribution(-10, 3);

                foreach (var additionalInfectedItem in additionalInfected) {
                    for (var t = additionalInfectedItem.XValue.AddDays(-21); t < additionalInfectedItem.XValue; t = t.AddDays(1)) {
                        var tAsIndex = (int)(t - graphTimeRangeStart).TotalDays;
                        var partialValue = distribution.CalculateSumBetween(tAsIndex - 1, tAsIndex) * additionalInfectedItem.YValue;
                        values[tAsIndex] += partialValue;
                    }
                }

                var estimatedNewInfections = new List<DataPoint<DateTime, double>>();

                for (var t = 0; t < graphTimeLengthInDays; ++t) {
                    var value = values[t] / inhabitants;

                    if (value > 0) {
                        estimatedNewInfections.Add(new DataPoint<DateTime, double>(graphTimeRangeStart.AddDays(t), value));
                    }
                }

                var dataSeries = new DataSeries<DateTime, double>(estimatedNewInfections, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        private string ConvertGraphToSvg<X, Y>(XYGraph<X, Y> graph) {
            if (_compressed) {
                return graph.ToSvgCompressed();
            }
            else {
                return graph.ToSvg();
            }
        }
    }
}
