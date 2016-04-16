using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cradle.Models.Repository
{
    public class CradleProfileManager : IProfileManager
    {
        private CradleDbContext _context;

        public CradleProfileManager()
        {
            _context = new CradleDbContext();
        }

        public DesignerProfileViewModel GetDesignerProfile(string userId)
        {
            DesignerProfileViewModel designerViewModel = null;
            DesignerProfile profile = _context.DesignerProfiles.Find(userId);
            if (profile != null && profile.DesignerProfileID == userId)
            {
                designerViewModel = new DesignerProfileViewModel()
                {
                    Name = profile.BusinessName,
                    Tagline = profile.Tagline,
                    Email = profile.BusinessEmailAddress,
                    Address = profile.Address,
                    StyleDescription = profile.StyleDescription,
                    ProfStats = profile.ProfileStats,
                    MadeType = new List<string>(),
                    DeliveryTime = new Dictionary<string, string>()
                };
                //string madeType = string.Empty;
                if (profile.IsRTW)
                {
                    designerViewModel.MadeType.Add("RTW");
                    designerViewModel.DeliveryTime.Add("RTW", profile.RTWMinDeliveryDays
                        + " - " + profile.RTWMaxDeliveryDays);
                }
                if (profile.IsCustomMade)
                {
                    designerViewModel.MadeType.Add("Custom Made");
                    designerViewModel.DeliveryTime.Add("Custom", profile.CustomMadeMinDeliveryDays
                        + " - " + profile.CustomMadeMaxDeliveryDays);
                }

                //Add code for setting DesignerProfile specialization here

                //Add code for setting Price Range for designer (depends on the collection price range) here
            }

            return designerViewModel;
           
        }

        public ProfileResult UpdateDesignerProfile(DesignerProfileViewModel designerProfile)
        {
            ProfileResult result;
            if (designerProfile != null)
            {
                result = ProfileResult.Success;
            }
            else
            {
                result = new ProfileResult(new string[1] { "Profile is null" });
            }

            return result;

        }



    }
}