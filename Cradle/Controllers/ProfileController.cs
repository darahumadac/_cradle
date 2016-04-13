using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cradle.Models;
using Microsoft.AspNet.Identity;

namespace Cradle.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private CradleDbContext _context;

        public ProfileController()
        {
            _context = new CradleDbContext();
        }
        //
        // GET: /Profile/
        public ActionResult Dashboard()
        {
            return View();
        }

        
        public ActionResult Manage(/*string designer*/)
        {
            DesignerProfile profile = _context.DesignerProfiles.Find(User.Identity.GetUserId());
            if(profile != null && profile.DesignerProfileID == User.Identity.GetUserId())
            {
                DesignerProfileViewModel designerViewModel = new DesignerProfileViewModel()
                {
                    DesignerName = profile.BusinessName,
                    Tagline = profile.Tagline,
                    Email = profile.BusinessEmailAddress,
                    Address = profile.Address,
                    MadeType = new List<string>(),
                    DeliveryTime = new Dictionary<string,string>()
                };
                //string madeType = string.Empty;
                if(profile.IsRTW)
                {
                    designerViewModel.MadeType.Add("RTW");
                    designerViewModel.DeliveryTime.Add("RTW", profile.RTWMinDeliveryDays
                        + " - " + profile.RTWMaxDeliveryDays);
                }
                if(profile.IsCustomMade)
                {
                    designerViewModel.MadeType.Add("Custom Made");
                    designerViewModel.DeliveryTime.Add("Custom", profile.CustomMadeMinDeliveryDays
                        + " - " + profile.CustomMadeMaxDeliveryDays);
                }
                
                //Add code for setting DesignerProfile specialization here

                //Add code for setting Price Range for designer (depends on the collection price range) here

                return View(designerViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Profile/Create
        public ActionResult NewCollection()
        {
            return View();
        }

        //
        // POST: /Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
