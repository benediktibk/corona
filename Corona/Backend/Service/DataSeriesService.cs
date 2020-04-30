using Backend.Repository;
using Math;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service {
    public class DataSeriesService : IDataSeriesService {
        private const int _estimationPastMaxInDays = -21;
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;
        private readonly ICountryInhabitantsRepository _countryDetailedRepository;

        public DataSeriesService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, ICountryInhabitantsRepository countryDetailedRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _countryDetailedRepository = countryDetailedRepository;
        }

        public List<DataSeries<DateTime, double>> CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries, out List<ReferenceLine<double>> referenceLines) {
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

            referenceLines = new List<ReferenceLine<double>>();
            referenceLines.Add(new ReferenceLine<double>((21.5 - 18) / 7 / 100000, "influenza based excess mortality season 2016/2017", new Color(102, 0, 51)));
            referenceLines.Add(new ReferenceLine<double>(10.3 / 1000 / 365, "general mortality EU-28", new Color(102, 0, 51)));

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }
        public List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

            return allDataSeries;
        }

        public List<DataSeries<double, double>> CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

            return allDataSeries;
        }

        public List<DataSeries<double, double>> CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateEstimatedActualInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
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

                var graphTimeRangeStart = timeRangeStart.AddDays(_estimationPastMaxInDays);
                var graphTimeRangeEnd = timeRangeEnd;
                var graphTimeLengthInDays = (int)(graphTimeRangeEnd - graphTimeRangeStart).TotalDays + 1;
                var values = new double[graphTimeLengthInDays];
                var distribution = new NormalDistribution(-10, 3);

                foreach (var additionalInfectedItem in additionalInfected) {
                    var previousDaysInPast = -1e10;
                    var peak = additionalInfectedItem.YValue / inhabitants;
                    for (var t = additionalInfectedItem.XValue.AddDays(_estimationPastMaxInDays); t < additionalInfectedItem.XValue; t = t.AddDays(1)) {
                        var daysInPast = (t - additionalInfectedItem.XValue).TotalDays;

                        if (daysInPast == 0) {
                            daysInPast = 1e10;
                        }

                        var partialValue = distribution.CalculateSumBetween(previousDaysInPast, daysInPast) * peak;

                        var tAsIndex = (int)(t - graphTimeRangeStart).TotalDays;
                        values[tAsIndex] += partialValue;
                        previousDaysInPast = daysInPast;
                    }
                }

                var estimatedNewInfections = new List<DataPoint<DateTime, double>>();

                for (var t = 0; t < graphTimeLengthInDays; ++t) {
                    if (values[t] > 0) {
                        estimatedNewInfections.Add(new DataPoint<DateTime, double>(graphTimeRangeStart.AddDays(t), values[t]));
                    }
                }

                var dataSeries = new DataSeries<DateTime, double>(estimatedNewInfections, PredefinedColors.GetFor(i), true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }
    }
}
