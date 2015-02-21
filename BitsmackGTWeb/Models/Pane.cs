using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitsmackGTWeb.Models
{
    public class Pane
    {
        public string size { get; set; }
        public string[] center { get; set; }
        public int startAngle { get; set; }
        public int endAngle { get; set; }
        public Background background { get; set; }

        public Pane()
        {
            background = new Background();
        }
    }
}
