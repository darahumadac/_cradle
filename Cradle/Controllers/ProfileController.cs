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

        //GET: View Profile page
        public ActionResult View()
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
        public ActionResult Edit()
        {
            ManageDesignerProfileViewModel manageProfileViewModel = new ManageDesignerProfileViewModel();
            manageProfileViewModel = ProfileManager.GetDesignerInformation(User.Identity.GetUserId());
            return View(manageProfileViewModel);
        }

        //
        // POST: /Profile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManageDesignerProfileViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (TryUpdateModel(ProfileManager.GetPerson(User.Identity.GetUserId()),
                        new string[] { "FirstName", "LastName", "BirthDate" }) &&
                        TryUpdateModel(ProfileManager.GetPerson(User.Identity.GetUserId()).Address,
                        new string[] {"City", "Country"}) &&
                        TryUpdateModel(ProfileManager.GetDesigner(User.Identity.GetUserId()),
                        new string[] { "BusinessName", "BusinessEmailAddress",
                            "IsRTW", "IsCustomMade", "Tagline", "StyleDescription", 
                            "RTWMinDeliveryDays", "RTWMaxDeliveryDays", "CustomMadeMinDeliveryDays", 
                            "CustomMadeMaxDeliveryDays" }) &&
                        TryUpdateModel(ProfileManager.GetDesigner(User.Identity.GetUserId()).Address,
                        new string[] {"StreetAddress"}))
                    {
                        ProfileResult result = ProfileManager.UpdateDesignerProfile(User.Identity.GetUserId(), model);
                    }

                    return RedirectToAction("View");
                    
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("UpdateProfileError", "Unable to save changes.  Please try again.");
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
