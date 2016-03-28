using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cradle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Designers()
        {
            return View();
        }

        public ActionResult Lookbook()
        {
            return View();
        }

        [ActionName("Custom-Made")]
        public ActionResult CustomMade()
        {
            return View();
        }
    }
}