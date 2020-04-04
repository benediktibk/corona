using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace ScalableVectorGraphicTest
{
    [TestClass]
    public class XYGraphTest
    {
        private XYGraph<double, double> _linearGraph;
        private XYGraph<double, double> _logarithmicGraph;
        private XYGraph<DateTime, double> _dateTimeGraph;

        [TestInitialize]
        public void Setup() {
            var doubleOperations = new NumericOperationsDouble();
            var dateTimeOperations = new NumericOperationsDateTimeForDatesOnly(new DateTime(2000, 2, 3));
            var dataSeriesOne = new DataSeries<double, double>(new List<DataPoint<double, double>> {
                new DataPoint<double, double>(5, 2),
                new DataPoint<double, double>(-10, 10000)
            }, Color.Red, true);
            var dataSeriesTwo = new DataSeries<double, double>(new List<DataPoint<double, double>> {
                new DataPoint<double, double>(-4, 123),
                new DataPoint<double, double>(6, -100),
                new DataPoint<double, double>(6, 9)
            }, Color.Blue, true);
            var dataSeriesThree = new DataSeries<DateTime, double>(new List<DataPoint<DateTime, double>> {
                new DataPoint<DateTime, double>(new DateTime(2000, 2, 6), 123),
                new DataPoint<DateTime, double>(new DateTime(2001, 4, 6), 100),
                new DataPoint<DateTime, double>(new DateTime(2000, 7, 6), 9)
            }, Color.Blue, true);
            var linearAxisOne = new LinearAxisDouble(doubleOperations, "x", "F2");
            var linearAxisTwo = new LinearAxisDouble(doubleOperations, "y", "E0");
            var logarithmicAxis = new LogarithmicAxis<double>(doubleOperations, "y", "E0");
            var dateTimeTaxis = new LinearAxisDateTime(dateTimeOperations, "dd.MM.yyyy");
            _linearGraph = new XYGraph<double, double>(500, 300, linearAxisOne, linearAxisTwo, new List<DataSeries<double, double>> { dataSeriesOne, dataSeriesTwo });
            _logarithmicGraph = new XYGraph<double, double>(530, 400, linearAxisOne, logarithmicAxis, new List<DataSeries<double, double>> { dataSeriesOne });
            _dateTimeGraph = new XYGraph<DateTime, double>(530, 400, dateTimeTaxis, logarithmicAxis, new List<DataSeries<DateTime, double>> { dataSeriesThree });
        }

        [TestMethod]
        public void ToSvg_LinearGraph_CorrectXml() {
            var result = _linearGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""300"" width=""500"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""75"" y1=""262.5"" x2=""475"" y2=""262.5"" style=""stroke:rgb(0,0,0);stroke-width:0.638748776906853"" />
<!-- horizontal axis tick mark -->
<line x1=""132.142857142857"" y1=""263.775"" x2=""132.142857142857"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""132.142857142857"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 132.142857142857,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-7,71</text>
<!-- vertical grid -->
<line x1=""132.142857142857"" y1=""262.5"" x2=""132.142857142857"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""189.285714285714"" y1=""263.775"" x2=""189.285714285714"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""189.285714285714"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 189.285714285714,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-5,43</text>
<!-- vertical grid -->
<line x1=""189.285714285714"" y1=""262.5"" x2=""189.285714285714"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""246.428571428571"" y1=""263.775"" x2=""246.428571428571"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""246.428571428571"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 246.428571428571,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-3,14</text>
<!-- vertical grid -->
<line x1=""246.428571428571"" y1=""262.5"" x2=""246.428571428571"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""303.571428571429"" y1=""263.775"" x2=""303.571428571429"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""303.571428571429"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 303.571428571429,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-0,86</text>
<!-- vertical grid -->
<line x1=""303.571428571429"" y1=""262.5"" x2=""303.571428571429"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""360.714285714286"" y1=""263.775"" x2=""360.714285714286"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""360.714285714286"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 360.714285714286,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">1,43</text>
<!-- vertical grid -->
<line x1=""360.714285714286"" y1=""262.5"" x2=""360.714285714286"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""417.857142857143"" y1=""263.775"" x2=""417.857142857143"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""417.857142857143"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 417.857142857143,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">3,71</text>
<!-- vertical grid -->
<line x1=""417.857142857143"" y1=""262.5"" x2=""417.857142857143"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis tick mark -->
<line x1=""475"" y1=""263.775"" x2=""475"" y2=""261.225"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- horizontal axis tick label -->
<text x=""475"" y=""265.05"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 475,265.05)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">6,00</text>
<!-- vertical grid -->
<line x1=""475"" y1=""262.5"" x2=""475"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis label -->
<text x=""275"" y=""272.7"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 275,272.7)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""75"" y1=""262.5"" x2=""75"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.638748776906853"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""226.071428571429"" x2=""77"" y2=""226.071428571429"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""226.071428571429"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,226.071428571429)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""226.071428571429"" x2=""475"" y2=""226.071428571429"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""189.642857142857"" x2=""77"" y2=""189.642857142857"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""189.642857142857"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,189.642857142857)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">3E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""189.642857142857"" x2=""475"" y2=""189.642857142857"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""153.214285714286"" x2=""77"" y2=""153.214285714286"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""153.214285714286"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,153.214285714286)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">4E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""153.214285714286"" x2=""475"" y2=""153.214285714286"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""116.785714285714"" x2=""77"" y2=""116.785714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""116.785714285714"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,116.785714285714)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">6E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""116.785714285714"" x2=""475"" y2=""116.785714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""80.3571428571428"" x2=""77"" y2=""80.3571428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""80.3571428571428"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,80.3571428571428)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">7E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""80.3571428571428"" x2=""475"" y2=""80.3571428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""43.9285714285714"" x2=""77"" y2=""43.9285714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""43.9285714285714"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,43.9285714285714)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">9E+003</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""43.9285714285714"" x2=""475"" y2=""43.9285714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- vertical axis tick mark -->
<line x1=""73"" y1=""7.5"" x2=""77"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.319374388453426"" />
<!-- vertical axis tick label -->
<text x=""71"" y=""7.5"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 71,7.5)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""75"" y1=""7.5"" x2=""475"" y2=""7.5"" style=""stroke:rgb(0,0,0);stroke-width:0.159687194226713"" />
<!-- horizontal axis label -->
<text x=""71"" y=""135"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 71,135)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""75"" cy=""7.5"" r=""1.59687194226713"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""450"" cy=""259.924752475248"" r=""1.59687194226713"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""75"" y1=""7.5"" x2=""450"" y2=""259.924752475248"" style=""stroke:rgb(255,0,0);stroke-width:0.638748776906853"" />
<!-- data point (-4,123) -->
<circle cx=""225"" cy=""256.869801980198"" r=""1.59687194226713"" fill=""rgb(0,0,255)"" />
<!-- data point (6,-100) -->
<circle cx=""475"" cy=""262.5"" r=""1.59687194226713"" fill=""rgb(0,0,255)"" />
<!-- data point (6,9) -->
<circle cx=""475"" cy=""259.74801980198"" r=""1.59687194226713"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (-4,123) to (6,-100) -->
<line x1=""225"" y1=""256.869801980198"" x2=""475"" y2=""262.5"" style=""stroke:rgb(0,0,255);stroke-width:0.638748776906853"" />
<!-- data point connection from (6,-100) to (6,9) -->
<line x1=""475"" y1=""262.5"" x2=""475"" y2=""259.74801980198"" style=""stroke:rgb(0,0,255);stroke-width:0.638748776906853"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_LogarithmicGraph_CorrectXml() {
            var result = _logarithmicGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""79.5"" y1=""350"" x2=""503.5"" y2=""350"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" />
<!-- horizontal axis tick mark -->
<line x1=""140.071428571429"" y1=""351.7"" x2=""140.071428571429"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""140.071428571429"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 140.071428571429,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-7,86</text>
<!-- vertical grid -->
<line x1=""140.071428571429"" y1=""350"" x2=""140.071428571429"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""200.642857142857"" y1=""351.7"" x2=""200.642857142857"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""200.642857142857"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 200.642857142857,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-5,71</text>
<!-- vertical grid -->
<line x1=""200.642857142857"" y1=""350"" x2=""200.642857142857"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""261.214285714286"" y1=""351.7"" x2=""261.214285714286"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""261.214285714286"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 261.214285714286,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-3,57</text>
<!-- vertical grid -->
<line x1=""261.214285714286"" y1=""350"" x2=""261.214285714286"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""321.785714285714"" y1=""351.7"" x2=""321.785714285714"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""321.785714285714"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 321.785714285714,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-1,43</text>
<!-- vertical grid -->
<line x1=""321.785714285714"" y1=""350"" x2=""321.785714285714"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""382.357142857143"" y1=""351.7"" x2=""382.357142857143"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""382.357142857143"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 382.357142857143,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">0,71</text>
<!-- vertical grid -->
<line x1=""382.357142857143"" y1=""350"" x2=""382.357142857143"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""442.928571428571"" y1=""351.7"" x2=""442.928571428571"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""442.928571428571"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 442.928571428571,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">2,86</text>
<!-- vertical grid -->
<line x1=""442.928571428571"" y1=""350"" x2=""442.928571428571"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""503.5"" y1=""351.7"" x2=""503.5"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""503.5"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 503.5,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">5,00</text>
<!-- vertical grid -->
<line x1=""503.5"" y1=""350"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis label -->
<text x=""291.5"" y=""363.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 291.5,363.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""79.5"" y1=""350"" x2=""79.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""265"" x2=""81.62"" y2=""265"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""265"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,265)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""265"" x2=""503.5"" y2=""265"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""180"" x2=""81.62"" y2=""180"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""180"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,180)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""180"" x2=""503.5"" y2=""180"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""95"" x2=""81.62"" y2=""95"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""95"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,95)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""95"" x2=""503.5"" y2=""95"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""10"" x2=""81.62"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""10"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,10)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""10"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis label -->
<text x=""75.26"" y=""137.5"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 75.26,137.5)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""79.5"" cy=""10"" r=""1.89842039601349"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""503.5"" cy=""324.412450368562"" r=""1.89842039601349"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""79.5"" y1=""10"" x2=""503.5"" y2=""324.412450368562"" style=""stroke:rgb(255,0,0);stroke-width:0.759368158405394"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""79.5"" y1=""350"" x2=""503.5"" y2=""350"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" />
<!-- horizontal axis tick mark -->
<line x1=""140.071428571429"" y1=""351.7"" x2=""140.071428571429"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""140.071428571429"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 140.071428571429,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text>
<!-- vertical grid -->
<line x1=""140.071428571429"" y1=""350"" x2=""140.071428571429"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""200.642857142857"" y1=""351.7"" x2=""200.642857142857"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""200.642857142857"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 200.642857142857,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text>
<!-- vertical grid -->
<line x1=""200.642857142857"" y1=""350"" x2=""200.642857142857"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""261.214285714286"" y1=""351.7"" x2=""261.214285714286"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""261.214285714286"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 261.214285714286,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text>
<!-- vertical grid -->
<line x1=""261.214285714286"" y1=""350"" x2=""261.214285714286"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""321.785714285714"" y1=""351.7"" x2=""321.785714285714"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""321.785714285714"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 321.785714285714,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text>
<!-- vertical grid -->
<line x1=""321.785714285714"" y1=""350"" x2=""321.785714285714"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""382.357142857143"" y1=""351.7"" x2=""382.357142857143"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""382.357142857143"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 382.357142857143,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text>
<!-- vertical grid -->
<line x1=""382.357142857143"" y1=""350"" x2=""382.357142857143"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""442.928571428571"" y1=""351.7"" x2=""442.928571428571"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""442.928571428571"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 442.928571428571,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text>
<!-- vertical grid -->
<line x1=""442.928571428571"" y1=""350"" x2=""442.928571428571"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis tick mark -->
<line x1=""503.5"" y1=""351.7"" x2=""503.5"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- horizontal axis tick label -->
<text x=""503.5"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 503.5,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text>
<!-- vertical grid -->
<line x1=""503.5"" y1=""350"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis label -->
<text x=""291.5"" y=""363.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 291.5,363.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text>
<!-- vertical axis -->
<line x1=""79.5"" y1=""350"" x2=""79.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""236.666666666667"" x2=""81.62"" y2=""236.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""236.666666666667"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,236.666666666667)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""236.666666666667"" x2=""503.5"" y2=""236.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""123.333333333333"" x2=""81.62"" y2=""123.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""123.333333333333"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,123.333333333333)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""123.333333333333"" x2=""503.5"" y2=""123.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- vertical axis tick mark -->
<line x1=""77.38"" y1=""10"" x2=""81.62"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" />
<!-- vertical axis tick label -->
<text x=""75.26"" y=""10"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,10)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""79.5"" y1=""10"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" />
<!-- horizontal axis label -->
<text x=""75.26"" y=""180"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 75.26,180)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (06.02.2000 00:00:00,123) -->
<circle cx=""79.5"" cy=""113.144087370202"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" />
<!-- data point (06.04.2001 00:00:00,100) -->
<circle cx=""230.144705882353"" cy=""241.852515596877"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" />
<!-- data point (06.07.2000 00:00:00,9) -->
<circle cx=""503.5"" cy=""123.333333333333"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (06.02.2000 00:00:00,123) to (06.04.2001 00:00:00,100) -->
<line x1=""79.5"" y1=""113.144087370202"" x2=""230.144705882353"" y2=""241.852515596877"" style=""stroke:rgb(0,0,255);stroke-width:0.759368158405394"" />
<!-- data point connection from (06.04.2001 00:00:00,100) to (06.07.2000 00:00:00,9) -->
<line x1=""230.144705882353"" y1=""241.852515596877"" x2=""503.5"" y2=""123.333333333333"" style=""stroke:rgb(0,0,255);stroke-width:0.759368158405394"" />
</svg>");
        }

        [TestMethod]
        public void ToSvgCompressed_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvgCompressed();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1""><line x1=""79.5"" y1=""350"" x2=""503.5"" y2=""350"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" /><line x1=""140.071428571429"" y1=""351.7"" x2=""140.071428571429"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""140.071428571429"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 140.071428571429,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text><line x1=""140.071428571429"" y1=""350"" x2=""140.071428571429"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""200.642857142857"" y1=""351.7"" x2=""200.642857142857"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""200.642857142857"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 200.642857142857,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text><line x1=""200.642857142857"" y1=""350"" x2=""200.642857142857"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""261.214285714286"" y1=""351.7"" x2=""261.214285714286"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""261.214285714286"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 261.214285714286,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text><line x1=""261.214285714286"" y1=""350"" x2=""261.214285714286"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""321.785714285714"" y1=""351.7"" x2=""321.785714285714"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""321.785714285714"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 321.785714285714,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text><line x1=""321.785714285714"" y1=""350"" x2=""321.785714285714"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""382.357142857143"" y1=""351.7"" x2=""382.357142857143"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""382.357142857143"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 382.357142857143,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text><line x1=""382.357142857143"" y1=""350"" x2=""382.357142857143"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""442.928571428571"" y1=""351.7"" x2=""442.928571428571"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""442.928571428571"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 442.928571428571,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text><line x1=""442.928571428571"" y1=""350"" x2=""442.928571428571"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""503.5"" y1=""351.7"" x2=""503.5"" y2=""348.3"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""503.5"" y=""353.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 503.5,353.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text><line x1=""503.5"" y1=""350"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><text x=""291.5"" y=""363.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 291.5,363.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text><line x1=""79.5"" y1=""350"" x2=""79.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.759368158405394"" /><line x1=""77.38"" y1=""236.666666666667"" x2=""81.62"" y2=""236.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""75.26"" y=""236.666666666667"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,236.666666666667)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text><line x1=""79.5"" y1=""236.666666666667"" x2=""503.5"" y2=""236.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""77.38"" y1=""123.333333333333"" x2=""81.62"" y2=""123.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""75.26"" y=""123.333333333333"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,123.333333333333)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text><line x1=""79.5"" y1=""123.333333333333"" x2=""503.5"" y2=""123.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><line x1=""77.38"" y1=""10"" x2=""81.62"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.379684079202697"" /><text x=""75.26"" y=""10"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 75.26,10)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text><line x1=""79.5"" y1=""10"" x2=""503.5"" y2=""10"" style=""stroke:rgb(0,0,0);stroke-width:0.189842039601349"" /><text x=""75.26"" y=""180"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 75.26,180)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text><circle cx=""79.5"" cy=""113.144087370202"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" /><circle cx=""230.144705882353"" cy=""241.852515596877"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" /><circle cx=""503.5"" cy=""123.333333333333"" r=""1.89842039601349"" fill=""rgb(0,0,255)"" /><line x1=""79.5"" y1=""113.144087370202"" x2=""230.144705882353"" y2=""241.852515596877"" style=""stroke:rgb(0,0,255);stroke-width:0.759368158405394"" /><line x1=""230.144705882353"" y1=""241.852515596877"" x2=""503.5"" y2=""123.333333333333"" style=""stroke:rgb(0,0,255);stroke-width:0.759368158405394"" /></svg>");
        }
    }
}
