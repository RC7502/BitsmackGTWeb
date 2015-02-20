using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitsmackGTWeb.Models
{
    public class XAxis
    {
        public List<string> categories { get; set; }

        public XAxis()
        {
            categories = new List<string>();
        }
    }
}
