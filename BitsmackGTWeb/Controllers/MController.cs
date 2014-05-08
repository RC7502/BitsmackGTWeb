using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsmackGTWeb.Controllers
{
    public class MController : Controller
    {
        //
        // GET: /M/

        public ActionResult Index()
        {
            return View("Index", "_MobileLayout");
        }

    }
}
