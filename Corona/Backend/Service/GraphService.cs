using Backend.Repository;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
        }

        public string CreateGraph(int id) {
            if (id == 0) {
                var numericOperations = new NumericOperationsDouble();
                var dataSeries = new DataSeries<double, double>(new List<DataPoint<double, double>> {
                    new DataPoint<double, double>(-5, 7),
                    new DataPoint<double, double>(6, 9),
                    new DataPoint<double, double>(25, 7),
                    new DataPoint<double, double>(13, -1),
                }, Color.Red);
                var graph = new XYGraph<double, double>(
                    1000, 800,
                    new LinearAxis<double>(numericOperations),
                    new LinearAxis<double>(numericOperations),
                    new List<DataSeries<double, double>> { dataSeries },
                    5, 1);
                return graph.ToSvg();
            }
            else if (id == 1) {
                var numericOperationsDouble = new NumericOperationsDouble();
                var numericOperationsDateTime = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
                var dataSeries = new DataSeries<DateTime, double>(new List<DataPoint<DateTime, double>> {
                    new DataPoint<DateTime, double>(new DateTime(2020, 3, 1), 7),
                    new DataPoint<DateTime, double>(new DateTime(2020, 3, 2), 9),
                    new DataPoint<DateTime, double>(new DateTime(2020, 3, 5), 5),
                    new DataPoint<DateTime, double>(new DateTime(2020, 4, 20), -1),
                }, Color.Red);
                var graph = new XYGraph<DateTime, double>(
                    1000, 800,
                    new LinearAxis<DateTime>(numericOperationsDateTime),
                    new LinearAxis<double>(numericOperationsDouble),
                    new List<DataSeries<DateTime, double>> { dataSeries },
                    numericOperationsDateTime.Reference.AddDays(10), 1);
                return graph.ToSvg();
            }

            return string.Empty;
        }
    }
}
