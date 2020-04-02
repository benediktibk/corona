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

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
        }

        public string CreateGraphDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = new List<DataSeries<DateTime, double>>();

            allDataSeries.Add(new DataSeries<DateTime, double>(new List<DataPoint<DateTime, double>> {
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 1), 0),
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 2), 0.5),
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 3), 1)
            }, Color.Red));

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LinearAxis<double>(_numericOperationsDouble, "Persons"),
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
                var dataPoints = _infectionSpreadDataPointRepository.GetAllForCountry(unitOfWork, countries[i]);
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

            allDataSeries.Add(new DataSeries<DateTime, double>(new List<DataPoint<DateTime, double>> {
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 1), 1),
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 2), 10),
                new DataPoint<DateTime, double>(new DateTime(2020, 3, 3), 100)
            }, Color.Red));

            var graph = new XYGraph<DateTime, double>(
                1000, 800,
                new LinearAxis<DateTime>(_numericOperationsDates, "Date"),
                new LogarithmicAxis<double>(_numericOperationsDouble, "Persons"),
                allDataSeries);
            return graph.ToSvg();
        }
    }
}
