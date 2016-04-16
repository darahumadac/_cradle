using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Cradle.Models;
using Cradle.Models.Repository;

namespace Cradle.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        public ProfileController() : this(new CradleProfileManager()){}

        public ProfileController(IProfileManager profileManager)
        {
            ProfileManager = profileManager;
        }

        public IProfileManager ProfileManager { get; set; }

        // GET: /Profile/
        public ActionResult Dashboard()
        {
            return View();
        }

        //GET: Manage Profile page
        public ActionResult Manage()
        {
            //Dsiplay Profile
            DesignerProfileViewModel designerViewModel 
                = ProfileManager.GetDesignerProfile(User.Identity.GetUserId());
            
            if(designerViewModel != null)
            {
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
