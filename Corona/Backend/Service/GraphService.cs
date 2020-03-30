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
            var elements = new List<IGraphicElement> {
                new Line(new Point(10, 90), new Point(195, 90), new Color(255, 0, 0), 1),
                new Line(new Point(10, 90), new Point(10, 5), new Color(255, 0, 0), 1),
                new Text(new Point(50, 95), "x-axis", new Color(0, 255, 0), 0, "Arial, Helvetica, sans-serif", 1),
                new Circle(1, new Color(0, 0, 255), new Point(50, 50))
            };
            var image = new Image(200, 100, elements);
            return image.CreateXml();
        }
    }
}
