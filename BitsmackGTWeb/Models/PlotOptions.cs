using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitsmackGTWeb.Models
{
    public class PlotOptions
    {
        //public SolidGauge solidgauge;
        public Series series { get; set; }
        public Bar bar { get; set; }

        public PlotOptions()
        {
            //solidgauge = new SolidGauge();
            series = new Series();
            bar = new Bar();
        }
    }
}
