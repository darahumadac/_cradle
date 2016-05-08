using Cradle.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Drawing = System.Drawing;

namespace Cradle.Models
{
    public abstract class BaseProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public bool IsProfileComplete { get; set; }

    }

    public class DesignerProfileViewModel : BaseProfileViewModel
    {
        public bool IsCurrentUser { get; set; }
        public string Username { get; set; }
        public Image ProfilePicture { get; set; }
        private List<string> _incompleteFields = new List<string>();
        private DateTime _dateEstablished;
        private string[] _fields = new string[]{"Name", "Email", "Address", "Contact_Numbers",
                    "Tagline", "Made_Type", "Specialization", "Delivery_Time", "Style_Description"};
       
        public string Tagline { get; set; }
        public virtual List<ContactNumber> Contact_Numbers { get; set; }
        public List<string> Made_Type { get; set; }
        public string Specialization { get; set; }
        public string Style_Description { get; set; }
        [DisplayName("Delivery Time")]
        public Dictionary<string, string> Delivery_Time { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Collection> Collections { get; set; }
        public Statistics ProfStats { get; set; }
        public DateTime DateEstablished 
        {
            get
            {
                return _dateEstablished;
            }
            set
            {
                _dateEstablished = value;
            }
        }
        public List<string> IncompleteFields 
        { 
            get
            {
                return _incompleteFields;
            }
        }
        public double ProfileCompletionPercentage
        {
            get
            {
                double completed = 0;
                PropertyInfo[] properties = typeof(DesignerProfileViewModel).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (_fields.FirstOrDefault(f => f == properties[i].Name) != null && 
                        (properties[i].GetValue(this, null) != null && !properties[i].GetValue(this, null).Equals(0)
                        && !string.IsNullOrEmpty(properties[i].GetValue(this, null).ToString())))
                    {
                        completed += 1;
                    }
                    else if (_fields.FirstOrDefault(f => f == properties[i].Name) != null)
                    {
                        _incompleteFields.Add(properties[i].Name.Replace('_',' '));
                    }
                }
                if (Made_Type.Count != Delivery_Time.Count)
                {
                    completed -= 1;
                    _incompleteFields.Add("Delivery Time");
                }
                   
                return Math.Round((completed / _fields.Length) * 100.0);
            }
            
        }


        
    }

    public class ManageDesignerProfileViewModel : BaseProfileViewModel, IValidatableObject
    {
        #region Personal Profile

        public Image ProfilePicture { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "State/Province")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        #endregion

        #region Designer Basic Information

        [Required]
        [Display(Name = "Designer Label Name")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "State/Province")]
        public string BusinessCity { get; set; }

        [Required]
        [Display(Name = "Main Country of Operation")]
        public string BusinessCountry { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string BusinessZipCode { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string BusinessEmailAddress { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string BusinessMobile { get; set; }

        [Display(Name = "Landline Number")]
        [DataType(DataType.PhoneNumber)]
        public string BusinessLandline { get; set; }

        [Required]
        [Display(Name = "Date Established")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEstablished { get; set; }

        [Required]
        [Display(Name = "RTW")]
        public bool IsRTW { get; set; }

        [Display(Name = "Custom Made")]
        public bool IsCustomMade { get; set; }

        #endregion

        #region Designer About Information
        [Required]
        public string Tagline { get; set; }
        [Required]
        public List<AttireCheckbox> Specialization { get; set; }
        [Required]
        [Display(Name="About My Style")]
        public string StyleDescription { get; set; }
        [Required]
        public int RTWMinDeliveryDays { get; set; }
        public int RTWMaxDeliveryDays { get; set; }
        public int CustomMadeMinDeliveryDays { get; set; }
        public int CustomMadeMaxDeliveryDays { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (String.IsNullOrEmpty(MobileNo) || String.IsNullOrWhiteSpace(MobileNo.Split(' ')[1]))
            {
                validationResults.Add(new ValidationResult("Mobile No. is required",
                    new[] { "MobileNo" }));
            }
            if ((String.IsNullOrEmpty(BusinessMobile) || String.IsNullOrWhiteSpace(BusinessMobile.Split(' ')[1]))
                    && String.IsNullOrEmpty(BusinessLandline))
            {
                validationResults.Add(new ValidationResult("At least 1 Desginer Label Contact Number is required",
                    new[] { "BusinessMobile" }));
            }
            if(IsRTW == false && IsCustomMade == false)
            {
                validationResults.Add(new ValidationResult("At least one made type should be selected",
                    new[] { "IsRTW" }));
            }
            if (IsRTW == true && (RTWMinDeliveryDays == 1 && RTWMaxDeliveryDays == 1 
                || RTWMinDeliveryDays == RTWMaxDeliveryDays))
            {
                validationResults.Add(new ValidationResult("RTW Delivery Days cannot be the same",
                    new[] { "RTWMinDeliveryDays" }));
            }
            if (IsCustomMade == true && (CustomMadeMinDeliveryDays == 1 && CustomMadeMaxDeliveryDays == 1
                || CustomMadeMinDeliveryDays == CustomMadeMaxDeliveryDays))
            {
                validationResults.Add(new ValidationResult("Custom Made Delivery Days cannot be the same",
                    new[] { "CustomMadeMinDeliveryDays" }));
            }

            return validationResults;

        }
    }

    public class AttireCheckbox
    {
        public string Attire { get; set; }
        public bool IsSelected { get; set; }
    }

}