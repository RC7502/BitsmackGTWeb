using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitsmackGTWeb.Models
{
    public class YAxis
    {
        //public int lineWidth { get; set; }
        //public int? minorTickInterval { get; set; }
        //public int tickPixelInterval { get; set; }
        //public int tickWidth { get; set; }
        public Labels labels { get; set; }
        public List<ArrayList> stops { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public Title title { get; set; }

        public YAxis()
        {
            title = new Title();
            stops = new List<ArrayList>();
            labels = new Labels();
        }
    }
}
