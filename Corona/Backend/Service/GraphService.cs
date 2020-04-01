using Backend.Repository;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
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
                new LinearAxis<DateTime>(new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1))),
                new LinearAxis<double>(new NumericOperationsDouble()),
                allDataSeries,
                new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1)).Reference.AddDays(10), 10000);
            return graph.ToSvg();
        }
    }
}
