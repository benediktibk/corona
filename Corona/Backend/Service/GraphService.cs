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
            var width = 200;
            var height = 100;
            var elements = new List<IGraphicElement> {
                new Line(new Point(10, 10), new Point(195, 10), new Color(255, 0, 0), 1),
                new Line(new Point(10, 10), new Point(10, 95), new Color(255, 0, 0), 1),
                new Text(new Point(50, 5), "x-axis", new Color(0, 255, 0), 0, "Arial, Helvetica, sans-serif", 1),
                new Circle(1, new Color(0, 0, 255), new Point(50, 50))
            };
            var transformation = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            var image = new Image(width, height, elements, transformation);
            return image.CreateXml();
        }
    }
}
