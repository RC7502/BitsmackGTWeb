using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitsmackGTWeb.Models;


namespace BitsmackGTWeb.Controllers
{
    public class ChartController : Controller
    {
        private readonly ChartService _service;

        public ChartController()
        {
            _service = new ChartService();
        }


        //
        // GET: /Chart/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TestChart()
        {
            var chart = new HighCharts
                {
                    chart = new Chart { type = "bar" },
                    title = new Title { text = "Test Chart" },
                    xAxis = new XAxis
                        {
                            categories = new List<string>
                                {
                                    "Apples", "Oranges", "Pears", "Grapes", "Bananas"
                                },
                        },
                    yAxis = new YAxis
                        {
                            min = 0,
                            title = new Title { text = "Total fruit consumption" }
                        },
                    legend = new Legend { reversed = true },
                    plotOptions = new PlotOptions
                        {
                            series = new Series { stacking = "normal" }
                        },
                    series = new List<Series>
                        {
                            new Series{name = "John", data = new List<double>{5, 3, 4, 7, 2}},
                            new Series{name = "Jane", data = new List<double>{2, 2, 3, 2, 1}},
                            new Series{name = "Joe", data = new List<double>{3, 4, 4, 2, 5}}
                        }
                };

            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllGoalsByMonth()
        {
            return Json(_service.AllGoalsByMonth(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CurrentMonthGoalProgress()
        {
            return Json(_service.CurrentMonthGoalProgress(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult WeightYearProgress()
        {
            return Json(_service.WeightYearProgressNet(), JsonRequestBehavior.AllowGet);
        }
    }
}
