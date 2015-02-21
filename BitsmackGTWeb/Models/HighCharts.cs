using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitsmackGTWeb.Models
{
    public class HighCharts
    {
        public Pane pane { get; set; }
        public Tooltip tooltip { get; set; }
        public Chart chart { get; set; }
        public Title title { get; set; }
        public XAxis xAxis { get; set; }
        public YAxis yAxis { get; set; }
        public Legend legend { get; set; }
        public PlotOptions plotOptions { get; set; }
        public List<Series> series { get; set; }

        public HighCharts()
        {
            chart = new Chart();
            title = new Title();
            xAxis = new XAxis();
            yAxis = new YAxis();
            legend = new Legend();
            plotOptions = new PlotOptions();
            series = new List<Series>();
            tooltip = new Tooltip();
            pane = new Pane();
        }
 
    }
}