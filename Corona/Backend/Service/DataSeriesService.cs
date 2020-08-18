using Backend.Repository;
using Math;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service {
    public class DataSeriesService : IDataSeriesService {
        private const int EstimationPastMaxInDays = -21;
        private const int HighestAverageRecentlyInDays = 5;
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

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.DeathsTotal > 0);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();
                var previousValue = 0;

                foreach (var dataPoint in dataPoints) {
                    var dataPointConverted = new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.DeathsTotal - previousValue) / inhabitants);
                    previousValue = dataPoint.DeathsTotal;

                    if (dataPointConverted.YValue > 0) {
                        dataPointsConverted.Add(dataPointConverted);
                    }
                }

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            referenceLines = new List<ReferenceLine<double>> {
                new ReferenceLine<double>((21.5 - 18) / 7 / 100000, "influenza based excess mortality season 2016/2017", new Color(102, 0, 51)),
                new ReferenceLine<double>(10.3 / 1000 / 365, "general mortality EU-28", new Color(102, 0, 51))
            };

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();
                var previousValue = 0;

                foreach (var dataPoint in dataPoints) {
                    var dataPointConverted = new DataPoint<DateTime, double>(dataPoint.Date, (double)(dataPoint.DeathsTotal - previousValue));
                    previousValue = dataPoint.DeathsTotal;

                    if (dataPointConverted.YValue >= 0) {
                        dataPointsConverted.Add(dataPointConverted);
                    }
                }

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }
                
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }
        
        public List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, dataPoint.InfectedTotal)).ToList();

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }
                
                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public DataSeries<CountryType, double> CreateHighestAverageDeathsPerPopulationRecently(IUnitOfWork unitOfWork, int topCountriesCount) {
            var countriesDetailed = _countryDetailedRepository.GetAll(unitOfWork);
            var previousDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(HighestAverageRecentlyInDays));
            var countriesWithAverageDeathsPerDayPerInhabitants = new List<Tuple<CountryType, double>>();

            foreach (var country in countriesDetailed) {
                var mostRecentDataPoint = _infectionSpreadDataPointRepository.GetMostRecentDataPoint(unitOfWork, country.CountryId);
                var previousDataPoint = _infectionSpreadDataPointRepository.GetLastDataPointBefore(unitOfWork, country.CountryId, previousDateTime);
                var deathsInTimeRange = mostRecentDataPoint.DeathsTotal - previousDataPoint.DeathsTotal;
                var timeRangeInDays = (mostRecentDataPoint.Date.Date - previousDataPoint.Date.Date).TotalDays;
                var result = (double)deathsInTimeRange / country.Inhabitants / timeRangeInDays;

                if (result < 0) {
                    continue;
                }

                countriesWithAverageDeathsPerDayPerInhabitants.Add(new Tuple<CountryType, double>(country.CountryId, result));
            }

            var countriesSorted = countriesWithAverageDeathsPerDayPerInhabitants.OrderBy(x => x.Item2);

            var dataPoints = new List<DataPoint<CountryType, double>>();

            foreach (var country in countriesSorted.Skip(System.Math.Max(0, countriesSorted.Count() - topCountriesCount)).Reverse()) {
                dataPoints.Add(new DataPoint<CountryType, double>(country.Item1, country.Item2));
            }

            return new DataSeries<CountryType, double>(dataPoints, Color.Black, false, false, "");
        }

        public DataSeries<CountryType, double> CreateHighestAverageNewInfectionsPerPopulationRecently(IUnitOfWork unitOfWork, int topCountriesCount) {
            var countriesDetailed = _countryDetailedRepository.GetAll(unitOfWork);
            var previousDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(HighestAverageRecentlyInDays));
            var countriesWithAverageNewInfectionsPerDayPerInhabitants = new List<Tuple<CountryType, double>>();

            foreach (var country in countriesDetailed) {
                var mostRecentDataPoint = _infectionSpreadDataPointRepository.GetMostRecentDataPoint(unitOfWork, country.CountryId);
                var previousDataPoint = _infectionSpreadDataPointRepository.GetLastDataPointBefore(unitOfWork, country.CountryId, previousDateTime);
                var infectedInTimeRange = mostRecentDataPoint.InfectedTotal - previousDataPoint.InfectedTotal;
                var timeRangeInDays = (mostRecentDataPoint.Date.Date - previousDataPoint.Date.Date).TotalDays;
                var result = (double)infectedInTimeRange / country.Inhabitants / timeRangeInDays;

                if (result < 0) {
                    continue;
                }

                countriesWithAverageNewInfectionsPerDayPerInhabitants.Add(new Tuple<CountryType, double>(country.CountryId, result));
            }

            var countriesSorted = countriesWithAverageNewInfectionsPerDayPerInhabitants.OrderBy(x => x.Item2);

            var dataPoints = new List<DataPoint<CountryType, double>>();

            foreach (var country in countriesSorted.Skip(System.Math.Max(0, countriesSorted.Count() - topCountriesCount)).Reverse()) {
                dataPoints.Add(new DataPoint<CountryType, double>(country.Item1, country.Item2));
            }

            return new DataSeries<CountryType, double>(dataPoints, Color.Black, false, false, "");
        }

        public List<DataSeries<DateTime, double>> CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();
            var availableCountries = _countryDetailedRepository.GetAllAvailable(unitOfWork, countries);
            var availableCountriesSet = availableCountries.ToDictionary(x => x.CountryId, x => x.Inhabitants);

            for (var i = 0; i < countries.Count(); ++i) {
                if (!availableCountriesSet.TryGetValue(countries[i], out var inhabitants)) {
                    continue;
                }

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = dataPoints.Select(dataPoint => new DataPoint<DateTime, double>(dataPoint.Date, (double)dataPoint.InfectedTotal / inhabitants)).ToList();

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
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

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();

                foreach (var dataPoint in dataPoints) {
                    var value = (double)(dataPoint.InfectedTotal - dataPoint.DeathsTotal - dataPoint.RecoveredTotal) / inhabitants;

                    if (value <= 0) {
                        continue;
                    }

                    dataPointsConverted.Add(new DataPoint<DateTime, double>(dataPoint.Date, value));
                }

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateStillInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
                var dataPointsConverted = new List<DataPoint<DateTime, double>>();

                foreach (var dataPoint in dataPoints) {
                    var value = dataPoint.InfectedTotal - dataPoint.DeathsTotal - dataPoint.RecoveredTotal;
                    dataPointsConverted.Add(new DataPoint<DateTime, double>(dataPoint.Date, value));
                }

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<DateTime, double>(dataPointsConverted, PredefinedColors.GetFor(i), true, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public List<DataSeries<double, double>> CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<double, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
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

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false, true, countries[i].ToString());
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

                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]).Where(x => x.InfectedTotal > 0);
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

                if (dataPointsConverted.Count <= 0) {
                    continue;
                }

                var dataSeries = new DataSeries<double, double>(dataPointsConverted, PredefinedColors.GetFor(i), false, true, countries[i].ToString());
                allDataSeries.Add(dataSeries);
            }

            return allDataSeries;
        }

        public List<DataSeries<DateTime, double>> CreateEstimatedActualNewInfectedPersons(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            for (var i = 0; i < countries.Count(); ++i) {
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountryOrderedByDate(unitOfWork, countries[i]);

                if (dataPoints.Count <= 0) {
                    continue;
                }

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

                var graphTimeRangeStart = timeRangeStart.AddDays(EstimationPastMaxInDays);
                var graphTimeRangeEnd = timeRangeEnd;
                var graphTimeLengthInDays = (int)(graphTimeRangeEnd - graphTimeRangeStart).TotalDays + 1;
                var values = new double[graphTimeLengthInDays];
                var distribution = new NormalDistribution(-10, 3);

                foreach (var additionalInfectedItem in additionalInfected) {
                    var previousDaysInPast = -1e10;
                    var peak = additionalInfectedItem.YValue;
                    for (var t = additionalInfectedItem.XValue.AddDays(EstimationPastMaxInDays); t < additionalInfectedItem.XValue; t = t.AddDays(1)) {
                        var daysInPast = (t - additionalInfectedItem.XValue).TotalDays;

                        if (daysInPast == -1) {
                            daysInPast = 1e10;
                        }

                        var distributionFactor = distribution.CalculateSumBetween(previousDaysInPast, daysInPast);
                        var partialValue = distributionFactor * peak;

                        var tAsIndex = (int)(t - graphTimeRangeStart).TotalDays;
                        values[tAsIndex] += partialValue;
                        previousDaysInPast = daysInPast;
                    }
                }

                var estimatedNewInfections = new List<DataPoint<DateTime, double>>();

                for (var t = 0; t < graphTimeLengthInDays - 14; ++t) {
                    if (values[t] > 0) {
                        estimatedNewInfections.Add(new DataPoint<DateTime, double>(graphTimeRangeStart.AddDays(t), values[t]));
                    }
                }
                
                var colorEstimated = PredefinedColors.GetFor(i);
                var colorActual = colorEstimated.ChangeAlpha(0.5);
                var dataSeriesEstimated = new DataSeries<DateTime, double>(estimatedNewInfections, colorEstimated, true, false, $"{countries[i]} - estimated");
                var dataSeriesActual = new DataSeries<DateTime, double>(additionalInfected, colorActual, false, true, $"{countries[i]} - reported");

                if (dataSeriesEstimated.DataPoints.Count > 0) {
                    allDataSeries.Add(dataSeriesEstimated);
                }

                if (dataSeriesActual.DataPoints.Count > 0) {
                    allDataSeries.Add(dataSeriesActual);
                }
            }

            return allDataSeries;
        }
    }
}
