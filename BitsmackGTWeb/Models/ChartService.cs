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
        private PedometerCalcService pedometerCalcService { get; set; }

        public ChartService()
        {
            pedometerCalcService = new PedometerCalcService();
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
            var chart = new Chart()
                {
                    Type = ChartTypes.Gauge                    
                };
            highchart.InitChart(chart);
            highchart.SetTitle(new Title{Text = "Weight Loss"});
            var series = new Series {Data = new Data(new object[] {actualLoss})};
            highchart.SetSeries(series);
            var pane = new Pane
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
            var yaxis = new YAxis
                {
                    Min = 0,
                    Max = goalLoss,
                    PlotBands = new[]
                            {
                                new YAxisPlotBands { From = 0, To = expectedLoss, Color = Color.Red },
                                new YAxisPlotBands { From = expectedLoss, To = expectedLoss+0.7, Color = Color.Yellow },
                                new YAxisPlotBands { From = expectedLoss+0.7, To = goalLoss, Color = Color.Green }
                            },
                    Labels = new YAxisLabels() { Style = "color:'black'"}
                };
            highchart.SetYAxis(yaxis);
            highchart.SetTooltip(new Tooltip() {Enabled = false});
            highchart.SetSubtitle(new Subtitle()
                {
                    Text = string.Format("Actual: {0} | Expected: {1} | Difference: {2}", actualLoss, expectedLoss, actualLoss-expectedLoss)
                });
            highchart.SetLegend(new Legend() {Enabled = false});
            return highchart.ToHtmlString();
        }

        public string AllGoalsByMonthNet()
        {   
            var now = DateTime.Now;
            var currentMonth = new DateTime(now.Year, now.Month, 1);
            var startDate = currentMonth.AddYears(-1);                  

            var highchart = new Highcharts("AllGoalsMonth");
            var chart = new Chart()
                {
                    Type = ChartTypes.Column
                };
            highchart.InitChart(chart);
            highchart.SetTitle(new Title() {Text = "Goals by Month"});
            var yaxis = new YAxis {Max = 2};
            highchart.SetYAxis(yaxis);
            var series = new Series {Name = "Steps"};
            var xaxis = new XAxis();
            var categories = new List<string>();
            var data = new List<object>();
            for (var i = startDate; i <= currentMonth; i = i.AddMonths(1))
            {
                categories.Add(i.Month.ToString() + "-" + i.Year.ToString());
                data.Add(pedometerCalcService.MonthPct(i.Month, i.Year));
            }
            xaxis.Categories = categories.ToArray();
            series.Data = new Data(data.ToArray());
            highchart.SetXAxis(xaxis);
            highchart.SetSeries(series);

            return highchart.ToHtmlString();
        }

        public string CurrentMonthGoalProgressNet()
        {
            var now = DateTime.Now;
            var expectedPct = ((decimal)now.Day / DateTime.DaysInMonth(now.Year,now.Month));
            var highchart = new Highcharts("CurrentMonthGoal");
            var chart = new Chart() {Type = ChartTypes.Bar};
            var categories = new List<string> {"Steps"};
            var yaxis = new YAxis {Max = 1};

            var seriesArray = new List<Series>();
            var series = new Series {Name = "Expected",Color = Color.Red};
            var data = new List<object> {pedometerCalcService.MonthPctExpected(now)};
            var plotoptions = new PlotOptionsBar
                {
                    Grouping = false,
                    Shadow = false,
                    DataLabels = new PlotOptionsBarDataLabels()
                        {
                            Enabled = true,
                            Format = string.Format("Expected: {0}", (int) (pedometerCalcService.StepsPR()*expectedPct)),
                            Color = Color.White
                        }
                };
            series.Data = new Data(data.ToArray());
            series.PlotOptionsBar = plotoptions;
            seriesArray.Add(series);

            series = new Series {Name = "Actual", Color = Color.Green};
            data = new List<object> { pedometerCalcService.MonthPct(now.Month, now.Year) };
            plotoptions = new PlotOptionsBar
                {
                    Grouping = false,
                    Shadow = false,
                    DataLabels = new PlotOptionsBarDataLabels()
                        {
                            Enabled = true,
                            Format = string.Format("Actual: {0}", pedometerCalcService.MonthStepsActual()),
                            Color = Color.White
                        }
                };
            series.Data = new Data(data.ToArray());
            series.PlotOptionsBar = plotoptions;
            seriesArray.Add(series);
     
            highchart.InitChart(chart);
            highchart.SetTitle(new Title() {Text = "Month Progress"});
            highchart.SetXAxis(new XAxis() {Categories = categories.ToArray()});
            highchart.SetYAxis(yaxis);
            highchart.SetSeries(seriesArray.ToArray());
            highchart.SetTooltip(new Tooltip() {Enabled = false});

            return highchart.ToHtmlString();
        }
    }
}
