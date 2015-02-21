using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace BitsmackGTWeb.Models
{
    public class ChartService
    {
        private HighCharts chart { get; set; }
        private PedometerCalcService pedometerCalcService { get; set; }

        public ChartService()
        {
            pedometerCalcService = new PedometerCalcService();
        }

        public HighCharts AllGoalsByMonth()
        {
            //init chart
            chart = new HighCharts {chart = {type = "column"}, title = {text = "Goals by Month"}};
            chart.yAxis.max = 2;
            var startDate = new DateTime(2013, 3, 1);
            var now = DateTime.Now;
            var currentMonth = new DateTime(now.Year, now.Month, 1);

            var stepSeries = new Series {name = "Steps"};

            //loop months
            for (var i = startDate; i <= currentMonth; i = i.AddMonths(1))
            {
                chart.xAxis.categories.Add(i.Month.ToString() + "-" +i.Year.ToString());
                stepSeries.data.Add(pedometerCalcService.MonthPct(i.Month, i.Year));
                
            }
            chart.series.Add(stepSeries);

            return chart;
        }

        public HighCharts CurrentMonthGoalProgress()
        {
            var now = DateTime.Now;
            chart = new HighCharts { chart = { type = "bar" }, title = { text = "Month Progress" } };
            chart.xAxis.categories.Add("Steps");
            chart.yAxis.max = 1;
            chart.series.Add(new Series { name = "Expected", data = new List<double> { pedometerCalcService.MonthPctExpected(now) }, pointPadding = 0.3, pointPlacement = -0.2 });
            chart.series.Add(new Series { name = "Actual", data = new List<double> { pedometerCalcService.MonthPct(now.Month, now.Year) }, pointPadding = 0.4, pointPlacement = -0.2 });          
            chart.plotOptions = new PlotOptions {bar = new Bar {borderWidth = 0, grouping = false, shadow = false}};
            chart.tooltip.shared = true;
            return chart;
        }

        public HighCharts WeightYearProgress()
        {
            chart = new HighCharts {chart = {type = "solidgauge"}};
            var startWeight = pedometerCalcService.GetStartWeight(DateTime.Now.Year);
            const int goalWeight = 144;
            var currentWeight = pedometerCalcService.GetRecentWeight();
            var goalLoss = startWeight - goalWeight;
            var actualLoss = startWeight - currentWeight;
            var expectedPct = (DateTime.Now.DayOfYear/365.0);
            var expectedLoss = expectedPct*goalLoss;

            chart.tooltip.enabled = false;
            chart.series.Add(new Series {name = "Weight Loss", data = new List<double> {actualLoss}});

            chart.yAxis.min = 0;
            chart.yAxis.max = goalLoss;
            chart.yAxis.title.text = "Weight Loss";
            chart.yAxis.labels.y = 16;
            //chart.yAxis.title.y = -170;
            //chart.yAxis.lineWidth = 0;
            //chart.yAxis.minorTickInterval = null;
            //chart.yAxis.tickPixelInterval = 400;
            //chart.yAxis.tickWidth = 0;
            //green: actual is more than 7 days ahead of expected
            //yellow: actual is within 7 days of expected
            //red: actual is less than expected
            chart.yAxis.stops.Add(new ArrayList { 0, "#DF5353" });
            chart.yAxis.stops.Add(new ArrayList { expectedPct, "#DDDF0D" });
            chart.yAxis.stops.Add(new ArrayList { ((DateTime.Now.DayOfYear+7) / 365.0), "#55BF3B" });

            chart.pane.startAngle = -90;
            chart.pane.endAngle = 90;
            chart.pane.center = new List<string> {"50%", "85%"}.ToArray();
            chart.pane.size = "140%";

            chart.pane.background.shape = "arc";
            chart.pane.background.innerRadius = "60%";
            chart.pane.background.outerRadius = "100%";

            //chart.plotOptions.solidgauge.dataLabels = new DataLabels
            //    {
            //        y = 5,
            //        borderWidth = 0,
            //        useHTML = true
            //    };


            return chart;
        }

        public string WeightYearProgressNet()
        {
            var startWeight = pedometerCalcService.GetStartWeight(DateTime.Now.Year);
            const int goalWeight = 144;
            var currentWeight = pedometerCalcService.GetRecentWeight();
            var goalLoss = startWeight - goalWeight;
            var actualLoss = Math.Round(startWeight - currentWeight, 1);
            var expectedPct = (DateTime.Now.DayOfYear / 365.0);
            var expectedLoss = Math.Round(expectedPct * goalLoss, 1);

            var highchart = new Highcharts("weightloss");
            var chart = new DotNet.Highcharts.Options.Chart()
                {
                    Type = ChartTypes.Gauge                    
                };
            highchart.InitChart(chart);
            highchart.SetTitle(new DotNet.Highcharts.Options.Title{Text = "Weight Loss"});
            var series = new DotNet.Highcharts.Options.Series {Data = new Data(new object[] {actualLoss})};
            highchart.SetSeries(series);
            var pane = new DotNet.Highcharts.Options.Pane
                {
                    Background = new[]
                        {
                            new BackgroundObject
                                {
                                    InnerRadius = new PercentageOrPixel(60, true),
                                    OuterRadius = new PercentageOrPixel(100, true)
                                }
                        },
                        StartAngle = 0,
                        EndAngle = 360
                };
            highchart.SetPane(pane);
            var yaxis = new DotNet.Highcharts.Options.YAxis
                {
                    Min = 0,
                    Max = goalLoss,
                    //Stops = new BackColorOrGradient(new Gradient
                    //    {
                    //        LinearGradient = new[] { 0, 0, 0, 400 },
                    //        Stops = new object[,]
                    //            {
                    //                {0, Color.Red},
                    //                {expectedPct,Color.Yellow},
                    //                {((DateTime.Now.DayOfYear + 7)/365.0), Color.Green}
                    //            }
                    //    }),
                    PlotBands = new[]
                            {
                                new YAxisPlotBands { From = 0, To = expectedLoss, Color = Color.Red },
                                new YAxisPlotBands { From = expectedLoss, To = expectedLoss+0.7, Color = Color.Yellow },
                                new YAxisPlotBands { From = expectedLoss+0.7, To = goalLoss, Color = Color.Green }
                            },
                    Labels = new YAxisLabels() { Style = "color:'black'"}
                };
            highchart.SetYAxis(yaxis);
            highchart.SetTooltip(new DotNet.Highcharts.Options.Tooltip() {Enabled = false});
            highchart.SetSubtitle(new DotNet.Highcharts.Options.Subtitle()
                {
                    Text = string.Format("Actual: {0} | Expected: {1} | Difference: {2}", actualLoss, expectedLoss, actualLoss-expectedLoss)
                });
            highchart.SetLegend(new DotNet.Highcharts.Options.Legend() {Enabled = false});
            return highchart.ToHtmlString();
        }
    }
}
