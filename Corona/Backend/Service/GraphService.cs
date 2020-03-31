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
            var graph = new XYGraph(1000, 800);
            return graph.ToSvg();
        }
    }
}
