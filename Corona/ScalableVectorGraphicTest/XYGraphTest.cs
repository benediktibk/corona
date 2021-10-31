using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace ScalableVectorGraphicTest {
    [TestClass]
    public class XYGraphTest {
        private XYGraph<double, double> _linearGraph;
        private XYGraph<double, double> _logarithmicGraph;
        private XYGraph<DateTime, double> _dateTimeGraph;

        [TestInitialize]
        public void Setup() {
            var doubleOperations = new NumericOperationsDouble();
            var dateTimeOperations = new NumericOperationsDateTimeForDatesOnly(new DateTime(2000, 2, 3));
            var dataSeriesOne = new DataSeriesXY<double, double>(new List<DataPoint<double, double>> {
                new DataPoint<double, double>(5, 2),
                new DataPoint<double, double>(-10, 10000)
            }, Color.Red, true, true, "blub");
            var dataSeriesTwo = new DataSeriesXY<double, double>(new List<DataPoint<double, double>> {
                new DataPoint<double, double>(-4, 123),
                new DataPoint<double, double>(6, -100),
                new DataPoint<double, double>(6, 9)
            }, Color.Blue, true, true, "blub2");
            var dataSeriesThree = new DataSeriesXY<DateTime, double>(new List<DataPoint<DateTime, double>> {
                new DataPoint<DateTime, double>(new DateTime(2000, 2, 6), 123),
                new DataPoint<DateTime, double>(new DateTime(2001, 4, 6), 100),
                new DataPoint<DateTime, double>(new DateTime(2000, 7, 6), 9)
            }, Color.Blue, true, true, "blub3");
            var linearAxisOne = new LinearAxisDouble(doubleOperations, "x", "F2");
            var linearAxisTwo = new LinearAxisDouble(doubleOperations, "y", "E0");
            var logarithmicAxis = new LogarithmicAxis<double>(doubleOperations, "y", "E0");
            var dateTimeTaxis = new LinearAxisDateTime(dateTimeOperations, "dd.MM.yyyy");
            _linearGraph = new XYGraph<double, double>(500, 300, linearAxisOne, linearAxisTwo, new List<DataSeriesXY<double, double>> { dataSeriesOne, dataSeriesTwo }, false, false, new Point(0, 0));
            _logarithmicGraph = new XYGraph<double, double>(530, 400, linearAxisOne, logarithmicAxis, new List<DataSeriesXY<double, double>> { dataSeriesOne }, false, false, new Point(0, 0));
            _dateTimeGraph = new XYGraph<DateTime, double>(530, 400, dateTimeTaxis, logarithmicAxis, new List<DataSeriesXY<DateTime, double>> { dataSeriesThree }, false, false, new Point(0, 0));
        }

        [TestMethod]
        public void ToSvg_LinearGraph_CorrectXml() {
            var result = _linearGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""300"" width=""500"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""52.50000000000001"" y1=""273.75"" x2=""477.5"" y2=""273.75"" style=""stroke:rgb(0,0,0);stroke-width:0.658407168855261"" />
<!-- horizontal axis tick mark -->
<line x1=""113.21428571428572"" y1=""275.025"" x2=""113.21428571428572"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""113.21428571428572"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 113.21428571428572,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-7.71</text>
<!-- vertical grid -->
<line x1=""113.21428571428572"" y1=""273.75"" x2=""113.21428571428572"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""173.92857142857142"" y1=""275.025"" x2=""173.92857142857142"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""173.92857142857142"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 173.92857142857142,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-5.43</text>
<!-- vertical grid -->
<line x1=""173.92857142857142"" y1=""273.75"" x2=""173.92857142857142"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""234.64285714285714"" y1=""275.025"" x2=""234.64285714285714"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""234.64285714285714"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 234.64285714285714,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-3.14</text>
<!-- vertical grid -->
<line x1=""234.64285714285714"" y1=""273.75"" x2=""234.64285714285714"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""295.35714285714283"" y1=""275.025"" x2=""295.35714285714283"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""295.35714285714283"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 295.35714285714283,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-0.86</text>
<!-- vertical grid -->
<line x1=""295.35714285714283"" y1=""273.75"" x2=""295.35714285714283"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""356.07142857142856"" y1=""275.025"" x2=""356.07142857142856"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""356.07142857142856"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 356.07142857142856,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">1.43</text>
<!-- vertical grid -->
<line x1=""356.07142857142856"" y1=""273.75"" x2=""356.07142857142856"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""416.7857142857143"" y1=""275.025"" x2=""416.7857142857143"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""416.7857142857143"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 416.7857142857143,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">3.71</text>
<!-- vertical grid -->
<line x1=""416.7857142857143"" y1=""273.75"" x2=""416.7857142857143"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis tick mark -->
<line x1=""477.5"" y1=""275.025"" x2=""477.5"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- horizontal axis tick label -->
<text x=""477.5"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 477.5,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">6.00</text>
<!-- vertical grid -->
<line x1=""477.5"" y1=""273.75"" x2=""477.5"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis label -->
<text x=""265"" y=""283.95"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 265,283.95)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""52.50000000000001"" y1=""273.75"" x2=""52.50000000000001"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.658407168855261"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""237.32142857142856"" x2=""54.62500000000001"" y2=""237.32142857142856"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""237.32142857142856"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,237.32142857142856)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""237.32142857142856"" x2=""477.5"" y2=""237.32142857142856"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""200.89285714285714"" x2=""54.62500000000001"" y2=""200.89285714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""200.89285714285714"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,200.89285714285714)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">3E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""200.89285714285714"" x2=""477.5"" y2=""200.89285714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""164.46428571428572"" x2=""54.62500000000001"" y2=""164.46428571428572"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""164.46428571428572"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,164.46428571428572)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">4E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""164.46428571428572"" x2=""477.5"" y2=""164.46428571428572"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""128.03571428571428"" x2=""54.62500000000001"" y2=""128.03571428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""128.03571428571428"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,128.03571428571428)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">6E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""128.03571428571428"" x2=""477.5"" y2=""128.03571428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""91.60714285714283"" x2=""54.62500000000001"" y2=""91.60714285714283"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""91.60714285714283"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,91.60714285714283)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">7E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""91.60714285714283"" x2=""477.5"" y2=""91.60714285714283"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""55.178571428571416"" x2=""54.62500000000001"" y2=""55.178571428571416"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""55.178571428571416"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,55.178571428571416)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">9E+003</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""55.178571428571416"" x2=""477.5"" y2=""55.178571428571416"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- vertical axis tick mark -->
<line x1=""50.37500000000001"" y1=""18.75"" x2=""54.62500000000001"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.3292035844276305"" />
<!-- vertical axis tick label -->
<text x=""48.25000000000001"" y=""18.75"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25000000000001,18.75)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""52.50000000000001"" y1=""18.75"" x2=""477.5"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.16460179221381524"" />
<!-- horizontal axis label -->
<text x=""48.25000000000001"" y=""146.25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 48.25000000000001,146.25)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""52.50000000000001"" cy=""18.75"" r=""1.6460179221381523"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""450.9375"" cy=""271.17475247524754"" r=""1.6460179221381523"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""52.50000000000001"" y1=""18.75"" x2=""450.9375"" y2=""271.17475247524754"" style=""stroke:rgb(255,0,0);stroke-width:0.658407168855261"" />
<!-- data point (-4,123) -->
<circle cx=""211.875"" cy=""268.119801980198"" r=""1.6460179221381523"" fill=""rgb(0,0,255)"" />
<!-- data point (6,-100) -->
<circle cx=""477.5"" cy=""273.75"" r=""1.6460179221381523"" fill=""rgb(0,0,255)"" />
<!-- data point (6,9) -->
<circle cx=""477.5"" cy=""270.9980198019802"" r=""1.6460179221381523"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (-4,123) to (6,-100) -->
<line x1=""211.875"" y1=""268.119801980198"" x2=""477.5"" y2=""273.75"" style=""stroke:rgb(0,0,255);stroke-width:0.658407168855261"" />
<!-- data point connection from (6,-100) to (6,9) -->
<line x1=""477.5"" y1=""273.75"" x2=""477.5"" y2=""270.9980198019802"" style=""stroke:rgb(0,0,255);stroke-width:0.658407168855261"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_LogarithmicGraph_CorrectXml() {
            var result = _logarithmicGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""55.650000000000006"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- horizontal axis tick mark -->
<line x1=""120.00714285714284"" y1=""366.7"" x2=""120.00714285714284"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""120.00714285714284"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.00714285714284,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-7.86</text>
<!-- vertical grid -->
<line x1=""120.00714285714284"" y1=""365"" x2=""120.00714285714284"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""184.36428571428567"" y1=""366.7"" x2=""184.36428571428567"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""184.36428571428567"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.36428571428567,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-5.71</text>
<!-- vertical grid -->
<line x1=""184.36428571428567"" y1=""365"" x2=""184.36428571428567"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""248.72142857142853"" y1=""366.7"" x2=""248.72142857142853"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""248.72142857142853"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.72142857142853,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-3.57</text>
<!-- vertical grid -->
<line x1=""248.72142857142853"" y1=""365"" x2=""248.72142857142853"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""313.0785714285714"" y1=""366.7"" x2=""313.0785714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""313.0785714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.0785714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-1.43</text>
<!-- vertical grid -->
<line x1=""313.0785714285714"" y1=""365"" x2=""313.0785714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""377.4357142857142"" y1=""366.7"" x2=""377.4357142857142"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""377.4357142857142"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.4357142857142,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">0.71</text>
<!-- vertical grid -->
<line x1=""377.4357142857142"" y1=""365"" x2=""377.4357142857142"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""441.7928571428571"" y1=""366.7"" x2=""441.7928571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""441.7928571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.7928571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">2.86</text>
<!-- vertical grid -->
<line x1=""441.7928571428571"" y1=""365"" x2=""441.7928571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""506.14999999999986"" y1=""366.7"" x2=""506.14999999999986"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""506.14999999999986"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.14999999999986,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">5.00</text>
<!-- vertical grid -->
<line x1=""506.14999999999986"" y1=""365"" x2=""506.14999999999986"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis label -->
<text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""55.650000000000006"" y1=""365"" x2=""55.650000000000006"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""280"" x2=""57.9025"" y2=""280"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""280"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,280)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""280"" x2=""506.15"" y2=""280"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""195"" x2=""57.9025"" y2=""195"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""195"" x2=""506.15"" y2=""195"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""110"" x2=""57.9025"" y2=""110"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""110"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,110)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""110"" x2=""506.15"" y2=""110"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis label -->
<text x=""51.145"" y=""152.5"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,152.5)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""55.650000000000006"" cy=""25"" r=""1.9568469536476274"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""506.15"" cy=""339.4124503685616"" r=""1.9568469536476274"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""55.650000000000006"" y1=""25"" x2=""506.15"" y2=""339.4124503685616"" style=""stroke:rgb(255,0,0);stroke-width:0.782738781459051"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""55.650000000000006"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- horizontal axis tick mark -->
<line x1=""120.00714285714285"" y1=""366.7"" x2=""120.00714285714285"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""120.00714285714285"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.00714285714285,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text>
<!-- vertical grid -->
<line x1=""120.00714285714285"" y1=""365"" x2=""120.00714285714285"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""184.3642857142857"" y1=""366.7"" x2=""184.3642857142857"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""184.3642857142857"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.3642857142857,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text>
<!-- vertical grid -->
<line x1=""184.3642857142857"" y1=""365"" x2=""184.3642857142857"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""248.72142857142853"" y1=""366.7"" x2=""248.72142857142853"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""248.72142857142853"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.72142857142853,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text>
<!-- vertical grid -->
<line x1=""248.72142857142853"" y1=""365"" x2=""248.72142857142853"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""313.0785714285714"" y1=""366.7"" x2=""313.0785714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""313.0785714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.0785714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text>
<!-- vertical grid -->
<line x1=""313.0785714285714"" y1=""365"" x2=""313.0785714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""377.4357142857142"" y1=""366.7"" x2=""377.4357142857142"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""377.4357142857142"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.4357142857142,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text>
<!-- vertical grid -->
<line x1=""377.4357142857142"" y1=""365"" x2=""377.4357142857142"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""441.7928571428571"" y1=""366.7"" x2=""441.7928571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""441.7928571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.7928571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text>
<!-- vertical grid -->
<line x1=""441.7928571428571"" y1=""365"" x2=""441.7928571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis tick mark -->
<line x1=""506.15"" y1=""366.7"" x2=""506.15"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- horizontal axis tick label -->
<text x=""506.15"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.15,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text>
<!-- vertical grid -->
<line x1=""506.15"" y1=""365"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis label -->
<text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text>
<!-- vertical axis -->
<line x1=""55.650000000000006"" y1=""365"" x2=""55.650000000000006"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""251.66666666666669"" x2=""57.9025"" y2=""251.66666666666669"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""251.66666666666669"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,251.66666666666669)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""251.66666666666669"" x2=""506.15"" y2=""251.66666666666669"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""138.33333333333337"" x2=""57.9025"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""138.33333333333337"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,138.33333333333337)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""138.33333333333337"" x2=""506.15"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- vertical axis tick mark -->
<line x1=""53.39750000000001"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""55.650000000000006"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" />
<!-- horizontal axis label -->
<text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (2/6/2000 12:00:00 AM,123) -->
<circle cx=""55.650000000000006"" cy=""128.14408737020153"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" />
<!-- data point (4/6/2001 12:00:00 AM,100) -->
<circle cx=""215.70999999999998"" cy=""256.8525155968765"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" />
<!-- data point (7/6/2000 12:00:00 AM,9) -->
<circle cx=""506.15"" cy=""138.33333333333337"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (2/6/2000 12:00:00 AM,123) to (4/6/2001 12:00:00 AM,100) -->
<line x1=""55.650000000000006"" y1=""128.14408737020153"" x2=""215.70999999999998"" y2=""256.8525155968765"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" />
<!-- data point connection from (4/6/2001 12:00:00 AM,100) to (7/6/2000 12:00:00 AM,9) -->
<line x1=""215.70999999999998"" y1=""256.8525155968765"" x2=""506.15"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" />
</svg>");
        }

        [TestMethod]
        public void ToSvgCompressed_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvgCompressed();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1""><line x1=""55.650000000000006"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" /><line x1=""120.00714285714285"" y1=""366.7"" x2=""120.00714285714285"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""120.00714285714285"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.00714285714285,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text><line x1=""120.00714285714285"" y1=""365"" x2=""120.00714285714285"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""184.3642857142857"" y1=""366.7"" x2=""184.3642857142857"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""184.3642857142857"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.3642857142857,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text><line x1=""184.3642857142857"" y1=""365"" x2=""184.3642857142857"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""248.72142857142853"" y1=""366.7"" x2=""248.72142857142853"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""248.72142857142853"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.72142857142853,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text><line x1=""248.72142857142853"" y1=""365"" x2=""248.72142857142853"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""313.0785714285714"" y1=""366.7"" x2=""313.0785714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""313.0785714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.0785714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text><line x1=""313.0785714285714"" y1=""365"" x2=""313.0785714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""377.4357142857142"" y1=""366.7"" x2=""377.4357142857142"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""377.4357142857142"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.4357142857142,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text><line x1=""377.4357142857142"" y1=""365"" x2=""377.4357142857142"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""441.7928571428571"" y1=""366.7"" x2=""441.7928571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""441.7928571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.7928571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text><line x1=""441.7928571428571"" y1=""365"" x2=""441.7928571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""506.15"" y1=""366.7"" x2=""506.15"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""506.15"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.15,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text><line x1=""506.15"" y1=""365"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text><line x1=""55.650000000000006"" y1=""365"" x2=""55.650000000000006"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" /><line x1=""53.39750000000001"" y1=""251.66666666666669"" x2=""57.9025"" y2=""251.66666666666669"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""51.145"" y=""251.66666666666669"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,251.66666666666669)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text><line x1=""55.650000000000006"" y1=""251.66666666666669"" x2=""506.15"" y2=""251.66666666666669"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""53.39750000000001"" y1=""138.33333333333337"" x2=""57.9025"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""51.145"" y=""138.33333333333337"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,138.33333333333337)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text><line x1=""55.650000000000006"" y1=""138.33333333333337"" x2=""506.15"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><line x1=""53.39750000000001"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.3913693907295255"" /><text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text><line x1=""55.650000000000006"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.19568469536476274"" /><text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text><circle cx=""55.650000000000006"" cy=""128.14408737020153"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" /><circle cx=""215.70999999999998"" cy=""256.8525155968765"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" /><circle cx=""506.15"" cy=""138.33333333333337"" r=""1.9568469536476274"" fill=""rgb(0,0,255)"" /><line x1=""55.650000000000006"" y1=""128.14408737020153"" x2=""215.70999999999998"" y2=""256.8525155968765"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" /><line x1=""215.70999999999998"" y1=""256.8525155968765"" x2=""506.15"" y2=""138.33333333333337"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" /></svg>");
        }
    }
}
