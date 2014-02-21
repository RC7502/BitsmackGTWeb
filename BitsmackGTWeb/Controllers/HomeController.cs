using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsmackGTWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Pedometer()
        {
            return View();
        }

        public ActionResult Cardio()
        {
            return View();
        }

        public ActionResult Goals()
        {
            return View();
        }
    }
}
