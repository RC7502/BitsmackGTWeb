using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitsmackGTWeb.Models
{
    public class Series
    {
        public string name { get; set; }
        public List<double> data { get; set; }
        public string stacking { get; set; }
        public double pointPadding { get; set; }
        public double pointPlacement { get; set; }

        public Series()
        {
            data = new List<double>();
        }
    }
}
