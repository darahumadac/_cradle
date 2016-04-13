using Cradle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cradle.Controllers
{
    public class HomeController : Controller
    {
        private CradleDbContext _context;

        public HomeController()
        {
            _context = new CradleDbContext();
        }

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

        public ActionResult Designers(string filter)
        {
            List<DesignerResultsViewModel> designerList = new List<DesignerResultsViewModel>();
            if(string.IsNullOrWhiteSpace(filter) || !filter.Equals("custom-made"))
            {
                //All designers
                _context.DesignerProfiles.ToList()
                .ForEach(dp => designerList.Add(new DesignerResultsViewModel(dp)));
            }
            else if (filter.Equals("custom-made"))
            {
                _context.DesignerProfiles.Where(dp => dp.IsCustomMade == true)
                    .ToList().ForEach(dp => designerList.Add(new DesignerResultsViewModel(dp)));
            }

            return View(designerList);
        }

        public ActionResult Lookbook()
        {
            return View();
        }

       

    }
}