using Cradle.Models;
using Cradle.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cradle.Controllers
{
    public class HomeController : Controller
    {

        public HomeController() : this(new CradleHomeManager()) { }

        public HomeController(IHomeManager homeManager)
        {
            HomeManager = homeManager;
        }

        public IHomeManager HomeManager { get; set; }

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

        public ActionResult Designers(SearchCriteria criteria)
        {
            List<DesignerResultsViewModel> designers = HomeManager.GetDesignerSearchResults(criteria);

            return View(designers);
        }

        public ActionResult Lookbook()
        {
            return View();
        }




        
    }
}