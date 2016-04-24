using Cradle.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
                    Style_Description = profile.StyleDescription,
                    ProfStats = profile.ProfileStats,
                    Contact_Numbers = profile.ContactNumber,
                    IsProfileComplete = profile.IsProfileComplete,
                    Collections = profile.Collection,
                    DateEstablished = profile.Birthdate,
                    Made_Type = new List<string>(),
                    Delivery_Time = new Dictionary<string, string>()
                };

                if (profile.IsRTW)
                {
                    designerViewModel.Made_Type.Add("RTW");
                    if(profile.RTWMaxDeliveryDays > 0 && profile.RTWMinDeliveryDays > 0)
                    {
                        designerViewModel.Delivery_Time.Add("RTW", profile.RTWMinDeliveryDays
                        + " - " + profile.RTWMaxDeliveryDays);
                    }
                    
                }
                if (profile.IsCustomMade)
                {
                    designerViewModel.Made_Type.Add("Custom Made");
                    if(profile.CustomMadeMaxDeliveryDays > 0 && profile.CustomMadeMinDeliveryDays > 0)
                    {
                        designerViewModel.Delivery_Time.Add("Custom Made", profile.CustomMadeMinDeliveryDays
                        + " - " + profile.CustomMadeMaxDeliveryDays);
                    }
                    
                }

                //Add code for setting DesignerProfile specialization here
                List<DesignerAttire> tempList = profile.AttireSpecialization.FindAll(s => s.IsSelected);
                foreach (DesignerAttire specialization in tempList)
                {
                    designerViewModel.Specialization += specialization.Attire.ToString().Replace('_',' ');
                    if(tempList.Last() != specialization)
                    {
                        designerViewModel.Specialization += ", ";
                    }
                }

                //Add code for setting Price Range for designer (depends on the collection price range) here
            }

            return designerViewModel;
           
        }

        public ManageDesignerProfileViewModel GetDesignerInformation(string userId)
        {
            ManageDesignerProfileViewModel designerInfo = null;
            PersonalProfile personalProfile = _context.PersonalProfiles.Find(userId);
            DesignerProfile designerProfile = _context.DesignerProfiles.Find(userId);
            if (personalProfile != null && designerProfile != null && 
                personalProfile.PersonalProfileId == userId && designerProfile.DesignerProfileID == userId)
            {
                designerInfo = new ManageDesignerProfileViewModel()
                {
                    FirstName = personalProfile.FirstName,
                    LastName = personalProfile.LastName,
                    City = personalProfile.Address.City,
                    Country = personalProfile.Address.Country,
                    BirthDate = personalProfile.Birthdate,
                    MobileNo = personalProfile.ContactNumber[0].MobileNo,
                    BusinessName = designerProfile.BusinessName,
                    StreetAddress = designerProfile.Address.StreetAddress,
                    BusinessCity = designerProfile.Address.City,
                    BusinessCountry = designerProfile.Address.Country,
                    BusinessZipCode = designerProfile.Address.ZipCode,
                    BusinessEmailAddress = designerProfile.BusinessEmailAddress,
                    BusinessMobile = designerProfile.ContactNumber[0].MobileNo,
                    BusinessLandline = designerProfile.ContactNumber[0].LandlineNo,
                    DateEstablished = designerProfile.Birthdate,
                    IsRTW = designerProfile.IsRTW,
                    IsCustomMade = designerProfile.IsCustomMade,
                    Tagline = designerProfile.Tagline,
                    StyleDescription = designerProfile.StyleDescription,
                    RTWMinDeliveryDays = designerProfile.RTWMinDeliveryDays,
                    RTWMaxDeliveryDays = designerProfile.RTWMaxDeliveryDays,
                    CustomMadeMinDeliveryDays = designerProfile.CustomMadeMinDeliveryDays,
                    CustomMadeMaxDeliveryDays = designerProfile.CustomMadeMaxDeliveryDays

                };

                designerInfo.Specialization = new List<AttireCheckbox>();
                bool isSelected = false;
                foreach(Attire attire in Enum.GetValues(typeof(Attire)))
                {
                    if(designerProfile.AttireSpecialization
                        .FirstOrDefault(a => a.Attire == attire && a.IsSelected == true) != null)
                    {
                        isSelected = true;
                    }
                    else
                    {
                        isSelected = false;
                    }

                    designerInfo.Specialization.Add(
                        new AttireCheckbox()
                        {
                            Attire = attire.ToString().Replace('_',' '),
                            IsSelected = isSelected
                        });
                }

            }
            return designerInfo;
        }

        public ProfileResult UpdateDesignerProfile(string userId, ManageDesignerProfileViewModel profile)
        {
            ProfileResult result = null;
            try
            {
                
                //Update Personal mobile number
                string[] mobileNoParts = profile.MobileNo.Split(' ');
                if (!String.IsNullOrWhiteSpace(mobileNoParts[1]))
                {
                    GetPerson(userId).ContactNumber[0].CountryCodeMobile = mobileNoParts[0];
                    GetPerson(userId).ContactNumber[0].MobileNo = mobileNoParts[1];
                }
                else
                {
                    GetPerson(userId).ContactNumber[0].CountryCodeMobile = null;
                    GetPerson(userId).ContactNumber[0].MobileNo = null;
                }

                //Update Designer fields
                if (!profile.IsCustomMade)
                {
                    GetDesigner(userId).CustomMadeMinDeliveryDays = 0;
                    GetDesigner(userId).CustomMadeMaxDeliveryDays = 0;
                }
                if (!profile.IsRTW)
                {
                    GetDesigner(userId).RTWMinDeliveryDays = 0;
                    GetDesigner(userId).RTWMaxDeliveryDays = 0;
                }


                mobileNoParts = profile.BusinessMobile.Split(' ');
                if (!String.IsNullOrWhiteSpace(mobileNoParts[1]))
                {
                    GetDesigner(userId).ContactNumber[0].CountryCodeMobile = mobileNoParts[0];
                    GetDesigner(userId).ContactNumber[0].MobileNo = mobileNoParts[1];
                }
                else
                {
                    GetDesigner(userId).ContactNumber[0].CountryCodeMobile = null;
                    GetDesigner(userId).ContactNumber[0].MobileNo = null;
                }

                if(!string.IsNullOrWhiteSpace(profile.BusinessLandline))
                {
                    GetDesigner(userId).ContactNumber[0].LandlineNo = profile.BusinessLandline;
                }
                else
                {
                    GetDesigner(userId).ContactNumber[0].LandlineNo = null;
                }

                GetDesigner(userId).Birthdate = profile.DateEstablished;
                GetDesigner(userId).Address.City = profile.BusinessCity;
                GetDesigner(userId).Address.Country = profile.BusinessCountry;
                GetDesigner(userId).Address.ZipCode = profile.BusinessZipCode;

                GetDesigner(userId).AttireSpecialization = new List<DesignerAttire>();

                Attire attire;
                GetDesigner(userId).AttireSpecialization.ForEach(s => s.IsSelected = false);
                foreach(AttireCheckbox checkbox in profile.Specialization)
                {
                    Enum.TryParse(checkbox.Attire.Replace(' ', '_'), out attire);
                    DesignerAttire tempAttire = GetDesigner(userId).AttireSpecialization
                        .FirstOrDefault(s => s.Attire == attire);
                    if (tempAttire == null && checkbox.IsSelected)
                    {
                        GetDesigner(userId).AttireSpecialization
                        .Add(new DesignerAttire() { Attire = attire, IsSelected = true });
                    }
                    else if(checkbox.IsSelected) //If tempAttire already exists, set it to true
                    {
                        GetDesigner(userId).AttireSpecialization
                            .FirstOrDefault(s => s.Attire == attire).IsSelected = true;
                    }
                    
                }

               
                if(IsProfileComplete(GetDesigner(userId)))
                {
                    GetDesigner(userId).IsProfileComplete = true;
                }
                else
                {
                    GetDesigner(userId).IsProfileComplete = false;
                }

                _context.SaveChanges();

                
            }
            catch(DataException ex)
            {
                //Add error
            }

            

            
            return result;

        }


        public DesignerProfile GetDesigner(string userId)
        {
           if(!string.IsNullOrWhiteSpace(userId))
           {
               return _context.DesignerProfiles.Find(userId);
           }
           else
           {
               return null;
           }
        }

        public PersonalProfile GetPerson(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return _context.PersonalProfiles.Find(userId);
            }
            else
            {
                return null;
            }
        }

        private bool IsProfileComplete(DesignerProfile designerProfile)
        {
            string[] _fields = new string[]{"BusinessName", "BusinessEmailAddress", "Tagline",  
                "StyleDescription", "IsRTW", "IsCustomMade", "Address", "Birthdate", "ContactNumber",
            "AttireSpecialization", "RTWMinDeliveryDays", "RTWMaxDeliveryDays", "CustomMadeMinDeliveryDays", 
            "CustomMadeMaxDeliveryDays"};

            bool isComplete = true;
            int madeTypeCount = 0;
            int madeTypeDeliveryTime = 0;

            PropertyInfo[] properties = typeof(DesignerProfile).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (_fields.FirstOrDefault(f => f == properties[i].Name) != null &&
                    (properties[i].GetValue(designerProfile, null) != null && 
                    !properties[i].GetValue(designerProfile, null).Equals(0)
                    && !string.IsNullOrEmpty(properties[i].GetValue(designerProfile, null).ToString())))
                {
                    if(properties[i].Name.Equals("AttireSpecialization") && 
                        ((List<DesignerAttire>)properties[i].GetValue(designerProfile,null))
                        .FindAll( a => a.IsSelected == true).Count == 0)
                    {
                        isComplete = false;
                        break;
                    }
                    else if (properties[i].Name.Equals("IsRTW") || properties[i].Name.Equals("IsCustomMade"))
                    {
                        madeTypeCount++;
                    }

                }
                else if (_fields.FirstOrDefault(f => f == properties[i].Name) != null)
                {
                    if (((properties[i].Name.Equals("RTWMinDeliveryDays") || properties[i].Name.Equals("RTWMaxDeliveryDays"))
                        && designerProfile.IsRTW && !properties[i].GetValue(designerProfile, null).Equals(0)))
                    {
                        madeTypeDeliveryTime++;
                    }
                    if ((properties[i].Name.Equals("CustomMadeMinDeliveryDays") || properties[i].Name.Equals("CustomMadeMaxDeliveryDays"))
                        && designerProfile.IsCustomMade && !properties[i].GetValue(designerProfile,null).Equals(0))
                    {
                        madeTypeDeliveryTime++;
                    }
                    else if (!properties[i].Name.Equals("RTWMinDeliveryDays") && !properties[i].Name.Equals("RTWMaxDeliveryDays")
                        && !properties[i].Name.Equals("CustomMadeMinDeliveryDays") && !properties[i].Name.Equals("CustomMadeMaxDeliveryDays"))
                    {
                        isComplete = false;
                        break;
                    }

                    
                }

            }

            if ((madeTypeCount * 2) < madeTypeDeliveryTime)
            {
                isComplete = false;
            }

            return isComplete;
        }
    }
}