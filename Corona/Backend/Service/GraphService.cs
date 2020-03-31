using Backend.Repository;
using ScalableVectorGraphic;
using System.Collections.Generic;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;

        public GraphService(IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository) {
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
        }

        public string CreateGraph() {
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
    }
}
