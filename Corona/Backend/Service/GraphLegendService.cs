using ScalableVectorGraphic;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class GraphLegendService : IGraphLegendService
    {
        private const int _borderTopAndBottom = 10;
        private const int _heightPerCountry = 20;
        private const double _lineWidth = 1.65;
        private const double _radius = 4.13;
        private const int _fontSize = 15;
        private const int _lineLength = 20;
        private const string _font = "monospace";

        public string CreateLegend(IReadOnlyList<CountryType> countries) {
            var elements = new List<IGraphicElement>();

            for (var i = 0; i < countries.Count(); ++i) {
                var currentColor = PredefinedColors.GetFor(i);
                var y = _borderTopAndBottom + i * _heightPerCountry;

                elements.Add(new Text($"label {countries[i]}", new Point(26, y), countries[i].ToString(), Color.Black, 0, _font, _fontSize, "middle", "start"));
                elements.Add(new Circle($"dot for {countries[i]}", _radius, currentColor, new Point(13, y)));
                elements.Add(new Line($"line for {countries[i]}", new Point(13 - _lineLength / 2, y), new Point(13 + _lineLength / 2, y), currentColor, _lineWidth));
            }

            var legend = new Image(300, 2 * _borderTopAndBottom + _heightPerCountry * countries.Count(), elements);
            return legend.CreateXml();
        }
    }
}
