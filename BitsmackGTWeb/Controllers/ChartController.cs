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
        public ActionResult AllGoalsByMonth()
        {
            return Json(_service.AllGoalsByMonthNet(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CurrentMonthGoalProgress()
        {
            return Json(_service.CurrentMonthGoalProgressNet(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult WeightYearProgress()
        {
            return Json(_service.WeightYearProgressNet(), JsonRequestBehavior.AllowGet);
        }
    }
}
