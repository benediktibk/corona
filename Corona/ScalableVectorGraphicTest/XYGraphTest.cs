﻿using FluentAssertions;
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
            }, Color.Red, true, "blub");
            var dataSeriesTwo = new DataSeries<double, double>(new List<DataPoint<double, double>> {
                new DataPoint<double, double>(-4, 123),
                new DataPoint<double, double>(6, -100),
                new DataPoint<double, double>(6, 9)
            }, Color.Blue, true, "blub2");
            var dataSeriesThree = new DataSeries<DateTime, double>(new List<DataPoint<DateTime, double>> {
                new DataPoint<DateTime, double>(new DateTime(2000, 2, 6), 123),
                new DataPoint<DateTime, double>(new DateTime(2001, 4, 6), 100),
                new DataPoint<DateTime, double>(new DateTime(2000, 7, 6), 9)
            }, Color.Blue, true, "blub3");
            var linearAxisOne = new LinearAxisDouble(doubleOperations, "x", "F2");
            var linearAxisTwo = new LinearAxisDouble(doubleOperations, "y", "E0");
            var logarithmicAxis = new LogarithmicAxis<double>(doubleOperations, "y", "E0");
            var dateTimeTaxis = new LinearAxisDateTime(dateTimeOperations, "dd.MM.yyyy");
            _linearGraph = new XYGraph<double, double>(500, 300, linearAxisOne, linearAxisTwo, new List<DataSeries<double, double>> { dataSeriesOne, dataSeriesTwo }, false, false, new Point(0, 0));
            _logarithmicGraph = new XYGraph<double, double>(530, 400, linearAxisOne, logarithmicAxis, new List<DataSeries<double, double>> { dataSeriesOne }, false, false, new Point(0, 0));
            _dateTimeGraph = new XYGraph<DateTime, double>(530, 400, dateTimeTaxis, logarithmicAxis, new List<DataSeries<DateTime, double>> { dataSeriesThree }, false, false, new Point(0, 0));
        }

        [TestMethod]
        public void ToSvg_LinearGraph_CorrectXml() {
            var result = _linearGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""300"" width=""500"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""52.5"" y1=""273.75"" x2=""477.5"" y2=""273.75"" style=""stroke:rgb(0,0,0);stroke-width:0.658407168855261"" />
<!-- horizontal axis tick mark -->
<line x1=""113.214285714286"" y1=""275.025"" x2=""113.214285714286"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""113.214285714286"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 113.214285714286,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-7,71</text>
<!-- vertical grid -->
<line x1=""113.214285714286"" y1=""273.75"" x2=""113.214285714286"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""173.928571428571"" y1=""275.025"" x2=""173.928571428571"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""173.928571428571"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 173.928571428571,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-5,43</text>
<!-- vertical grid -->
<line x1=""173.928571428571"" y1=""273.75"" x2=""173.928571428571"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""234.642857142857"" y1=""275.025"" x2=""234.642857142857"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""234.642857142857"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 234.642857142857,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-3,14</text>
<!-- vertical grid -->
<line x1=""234.642857142857"" y1=""273.75"" x2=""234.642857142857"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""295.357142857143"" y1=""275.025"" x2=""295.357142857143"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""295.357142857143"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 295.357142857143,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">-0,86</text>
<!-- vertical grid -->
<line x1=""295.357142857143"" y1=""273.75"" x2=""295.357142857143"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""356.071428571429"" y1=""275.025"" x2=""356.071428571429"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""356.071428571429"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 356.071428571429,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">1,43</text>
<!-- vertical grid -->
<line x1=""356.071428571429"" y1=""273.75"" x2=""356.071428571429"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""416.785714285714"" y1=""275.025"" x2=""416.785714285714"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""416.785714285714"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 416.785714285714,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">3,71</text>
<!-- vertical grid -->
<line x1=""416.785714285714"" y1=""273.75"" x2=""416.785714285714"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis tick mark -->
<line x1=""477.5"" y1=""275.025"" x2=""477.5"" y2=""272.475"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- horizontal axis tick label -->
<text x=""477.5"" y=""276.3"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 477.5,276.3)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">6,00</text>
<!-- vertical grid -->
<line x1=""477.5"" y1=""273.75"" x2=""477.5"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis label -->
<text x=""265"" y=""283.95"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 265,283.95)"" font-size=""6"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""52.5"" y1=""273.75"" x2=""52.5"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.658407168855261"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""237.321428571429"" x2=""54.625"" y2=""237.321428571429"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""237.321428571429"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,237.321428571429)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""237.321428571429"" x2=""477.5"" y2=""237.321428571429"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""200.892857142857"" x2=""54.625"" y2=""200.892857142857"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""200.892857142857"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,200.892857142857)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">3E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""200.892857142857"" x2=""477.5"" y2=""200.892857142857"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""164.464285714286"" x2=""54.625"" y2=""164.464285714286"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""164.464285714286"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,164.464285714286)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">4E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""164.464285714286"" x2=""477.5"" y2=""164.464285714286"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""128.035714285714"" x2=""54.625"" y2=""128.035714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""128.035714285714"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,128.035714285714)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">6E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""128.035714285714"" x2=""477.5"" y2=""128.035714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""91.6071428571428"" x2=""54.625"" y2=""91.6071428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""91.6071428571428"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,91.6071428571428)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">7E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""91.6071428571428"" x2=""477.5"" y2=""91.6071428571428"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""55.1785714285714"" x2=""54.625"" y2=""55.1785714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""55.1785714285714"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,55.1785714285714)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">9E+003</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""55.1785714285714"" x2=""477.5"" y2=""55.1785714285714"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- vertical axis tick mark -->
<line x1=""50.375"" y1=""18.75"" x2=""54.625"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.32920358442763"" />
<!-- vertical axis tick label -->
<text x=""48.25"" y=""18.75"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 48.25,18.75)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""52.5"" y1=""18.75"" x2=""477.5"" y2=""18.75"" style=""stroke:rgb(0,0,0);stroke-width:0.164601792213815"" />
<!-- horizontal axis label -->
<text x=""48.25"" y=""146.25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 48.25,146.25)"" font-size=""6"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""52.5"" cy=""18.75"" r=""1.64601792213815"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""450.9375"" cy=""271.174752475248"" r=""1.64601792213815"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""52.5"" y1=""18.75"" x2=""450.9375"" y2=""271.174752475248"" style=""stroke:rgb(255,0,0);stroke-width:0.658407168855261"" />
<!-- data point (-4,123) -->
<circle cx=""211.875"" cy=""268.119801980198"" r=""1.64601792213815"" fill=""rgb(0,0,255)"" />
<!-- data point (6,-100) -->
<circle cx=""477.5"" cy=""273.75"" r=""1.64601792213815"" fill=""rgb(0,0,255)"" />
<!-- data point (6,9) -->
<circle cx=""477.5"" cy=""270.99801980198"" r=""1.64601792213815"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (-4,123) to (6,-100) -->
<line x1=""211.875"" y1=""268.119801980198"" x2=""477.5"" y2=""273.75"" style=""stroke:rgb(0,0,255);stroke-width:0.658407168855261"" />
<!-- data point connection from (6,-100) to (6,9) -->
<line x1=""477.5"" y1=""273.75"" x2=""477.5"" y2=""270.99801980198"" style=""stroke:rgb(0,0,255);stroke-width:0.658407168855261"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_LogarithmicGraph_CorrectXml() {
            var result = _logarithmicGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""55.65"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- horizontal axis tick mark -->
<line x1=""120.007142857143"" y1=""366.7"" x2=""120.007142857143"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""120.007142857143"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.007142857143,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-7,86</text>
<!-- vertical grid -->
<line x1=""120.007142857143"" y1=""365"" x2=""120.007142857143"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""184.364285714286"" y1=""366.7"" x2=""184.364285714286"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""184.364285714286"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.364285714286,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-5,71</text>
<!-- vertical grid -->
<line x1=""184.364285714286"" y1=""365"" x2=""184.364285714286"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""248.721428571429"" y1=""366.7"" x2=""248.721428571429"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""248.721428571429"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.721428571429,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-3,57</text>
<!-- vertical grid -->
<line x1=""248.721428571429"" y1=""365"" x2=""248.721428571429"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""313.078571428571"" y1=""366.7"" x2=""313.078571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""313.078571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.078571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">-1,43</text>
<!-- vertical grid -->
<line x1=""313.078571428571"" y1=""365"" x2=""313.078571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""377.435714285714"" y1=""366.7"" x2=""377.435714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""377.435714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.435714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">0,71</text>
<!-- vertical grid -->
<line x1=""377.435714285714"" y1=""365"" x2=""377.435714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""441.792857142857"" y1=""366.7"" x2=""441.792857142857"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""441.792857142857"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.792857142857,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">2,86</text>
<!-- vertical grid -->
<line x1=""441.792857142857"" y1=""365"" x2=""441.792857142857"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""506.15"" y1=""366.7"" x2=""506.15"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""506.15"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.15,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">5,00</text>
<!-- vertical grid -->
<line x1=""506.15"" y1=""365"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis label -->
<text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">x</text>
<!-- vertical axis -->
<line x1=""55.65"" y1=""365"" x2=""55.65"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""280"" x2=""57.9025"" y2=""280"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""280"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,280)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""280"" x2=""506.15"" y2=""280"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""195"" x2=""57.9025"" y2=""195"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""195"" x2=""506.15"" y2=""195"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""110"" x2=""57.9025"" y2=""110"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""110"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,110)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""110"" x2=""506.15"" y2=""110"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+004</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis label -->
<text x=""51.145"" y=""152.5"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,152.5)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (5,2) -->
<circle cx=""55.65"" cy=""25"" r=""1.95684695364763"" fill=""rgb(255,0,0)"" />
<!-- data point (-10,10000) -->
<circle cx=""506.15"" cy=""339.412450368562"" r=""1.95684695364763"" fill=""rgb(255,0,0)"" />
<!-- data point connection from (5,2) to (-10,10000) -->
<line x1=""55.65"" y1=""25"" x2=""506.15"" y2=""339.412450368562"" style=""stroke:rgb(255,0,0);stroke-width:0.782738781459051"" />
</svg>");
        }

        [TestMethod]
        public void ToSvg_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvg();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
<svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">
<!-- horizontal axis -->
<line x1=""55.65"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- horizontal axis tick mark -->
<line x1=""120.007142857143"" y1=""366.7"" x2=""120.007142857143"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""120.007142857143"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.007142857143,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text>
<!-- vertical grid -->
<line x1=""120.007142857143"" y1=""365"" x2=""120.007142857143"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""184.364285714286"" y1=""366.7"" x2=""184.364285714286"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""184.364285714286"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.364285714286,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text>
<!-- vertical grid -->
<line x1=""184.364285714286"" y1=""365"" x2=""184.364285714286"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""248.721428571429"" y1=""366.7"" x2=""248.721428571429"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""248.721428571429"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.721428571429,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text>
<!-- vertical grid -->
<line x1=""248.721428571429"" y1=""365"" x2=""248.721428571429"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""313.078571428571"" y1=""366.7"" x2=""313.078571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""313.078571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.078571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text>
<!-- vertical grid -->
<line x1=""313.078571428571"" y1=""365"" x2=""313.078571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""377.435714285714"" y1=""366.7"" x2=""377.435714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""377.435714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.435714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text>
<!-- vertical grid -->
<line x1=""377.435714285714"" y1=""365"" x2=""377.435714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""441.792857142857"" y1=""366.7"" x2=""441.792857142857"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""441.792857142857"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.792857142857,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text>
<!-- vertical grid -->
<line x1=""441.792857142857"" y1=""365"" x2=""441.792857142857"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis tick mark -->
<line x1=""506.15"" y1=""366.7"" x2=""506.15"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- horizontal axis tick label -->
<text x=""506.15"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.15,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text>
<!-- vertical grid -->
<line x1=""506.15"" y1=""365"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis label -->
<text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text>
<!-- vertical axis -->
<line x1=""55.65"" y1=""365"" x2=""55.65"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""251.666666666667"" x2=""57.9025"" y2=""251.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""251.666666666667"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,251.666666666667)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""251.666666666667"" x2=""506.15"" y2=""251.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""138.333333333333"" x2=""57.9025"" y2=""138.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""138.333333333333"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,138.333333333333)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""138.333333333333"" x2=""506.15"" y2=""138.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- vertical axis tick mark -->
<line x1=""53.3975"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" />
<!-- vertical axis tick label -->
<text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text>
<!-- horizontal grid -->
<line x1=""55.65"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" />
<!-- horizontal axis label -->
<text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text>
<!-- data point (06.02.2000 00:00:00,123) -->
<circle cx=""55.65"" cy=""128.144087370202"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" />
<!-- data point (06.04.2001 00:00:00,100) -->
<circle cx=""215.71"" cy=""256.852515596877"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" />
<!-- data point (06.07.2000 00:00:00,9) -->
<circle cx=""506.15"" cy=""138.333333333333"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" />
<!-- data point connection from (06.02.2000 00:00:00,123) to (06.04.2001 00:00:00,100) -->
<line x1=""55.65"" y1=""128.144087370202"" x2=""215.71"" y2=""256.852515596877"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" />
<!-- data point connection from (06.04.2001 00:00:00,100) to (06.07.2000 00:00:00,9) -->
<line x1=""215.71"" y1=""256.852515596877"" x2=""506.15"" y2=""138.333333333333"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" />
</svg>");
        }

        [TestMethod]
        public void ToSvgCompressed_DateTimeGraph_CorrectXml() {
            var result = _dateTimeGraph.ToSvgCompressed();

            result.Should().Be(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?><svg height=""400"" width=""530"" xmlns=""http://www.w3.org/2000/svg"" version=""1.1""><line x1=""55.65"" y1=""365"" x2=""506.15"" y2=""365"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" /><line x1=""120.007142857143"" y1=""366.7"" x2=""120.007142857143"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""120.007142857143"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 120.007142857143,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2000</text><line x1=""120.007142857143"" y1=""365"" x2=""120.007142857143"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""184.364285714286"" y1=""366.7"" x2=""184.364285714286"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""184.364285714286"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 184.364285714286,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.06.2000</text><line x1=""184.364285714286"" y1=""365"" x2=""184.364285714286"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""248.721428571429"" y1=""366.7"" x2=""248.721428571429"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""248.721428571429"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 248.721428571429,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.08.2000</text><line x1=""248.721428571429"" y1=""365"" x2=""248.721428571429"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""313.078571428571"" y1=""366.7"" x2=""313.078571428571"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""313.078571428571"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 313.078571428571,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.10.2000</text><line x1=""313.078571428571"" y1=""365"" x2=""313.078571428571"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""377.435714285714"" y1=""366.7"" x2=""377.435714285714"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""377.435714285714"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 377.435714285714,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">05.12.2000</text><line x1=""377.435714285714"" y1=""365"" x2=""377.435714285714"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""441.792857142857"" y1=""366.7"" x2=""441.792857142857"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""441.792857142857"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 441.792857142857,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">04.02.2001</text><line x1=""441.792857142857"" y1=""365"" x2=""441.792857142857"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""506.15"" y1=""366.7"" x2=""506.15"" y2=""363.3"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""506.15"" y=""368.4"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 506.15,368.4)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">06.04.2001</text><line x1=""506.15"" y1=""365"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><text x=""280.9"" y=""378.6"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 280.9,378.6)"" font-size=""7"" dominant-baseline=""hanging"" text-anchor=""middle"">dd.MM.yyyy</text><line x1=""55.65"" y1=""365"" x2=""55.65"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.782738781459051"" /><line x1=""53.3975"" y1=""251.666666666667"" x2=""57.9025"" y2=""251.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""51.145"" y=""251.666666666667"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,251.666666666667)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+001</text><line x1=""55.65"" y1=""251.666666666667"" x2=""506.15"" y2=""251.666666666667"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""53.3975"" y1=""138.333333333333"" x2=""57.9025"" y2=""138.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""51.145"" y=""138.333333333333"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,138.333333333333)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+002</text><line x1=""55.65"" y1=""138.333333333333"" x2=""506.15"" y2=""138.333333333333"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><line x1=""53.3975"" y1=""25"" x2=""57.9025"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.391369390729525"" /><text x=""51.145"" y=""25"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(0 51.145,25)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""end"">1E+003</text><line x1=""55.65"" y1=""25"" x2=""506.15"" y2=""25"" style=""stroke:rgb(0,0,0);stroke-width:0.195684695364763"" /><text x=""51.145"" y=""195"" font-family=""monospace"" fill=""rgb(0,0,0)"" transform=""rotate(270 51.145,195)"" font-size=""7"" dominant-baseline=""middle"" text-anchor=""middle"">y</text><circle cx=""55.65"" cy=""128.144087370202"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" /><circle cx=""215.71"" cy=""256.852515596877"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" /><circle cx=""506.15"" cy=""138.333333333333"" r=""1.95684695364763"" fill=""rgb(0,0,255)"" /><line x1=""55.65"" y1=""128.144087370202"" x2=""215.71"" y2=""256.852515596877"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" /><line x1=""215.71"" y1=""256.852515596877"" x2=""506.15"" y2=""138.333333333333"" style=""stroke:rgb(0,0,255);stroke-width:0.782738781459051"" /></svg>");
        }
    }
}
