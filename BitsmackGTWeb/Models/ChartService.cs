using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}