﻿using Cradle.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Cradle.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : IValidatableObject
    {

        private CradleDbContext _context;

        public RegisterViewModel()
        {
            _context = new CradleDbContext();
            SecurityQuestions = _context.SecurityQuestions;
            BirthDate = DateTime.Today;
            DateEstablished = DateTime.Today;

        }

        #region Account Profile
        //Account Profile
        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //Password: Use at least one lowercase letter, one numeral, and seven characters.
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Register As")]
        public Role MemberAccountType { get; set; }

        [Required]
        [Display(Name = "Security Question")]
        public int SecurityQuestion { get; set; }
        public DbSet<SecurityQuestions> SecurityQuestions { get; set; }

        [Required]
        [Display(Name = "Answer")]
        public string SecurityAnswer { get; set; }
        #endregion

        #region Personal Profile
        //Personal Profile
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "City")]
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

        [Required]
        [Display(Name = "Landline Number")]
        [DataType(DataType.PhoneNumber)]
        public string LandlineNo { get; set; }

        #endregion

        #region Business Profile
        [Display(Name = "Designer Label Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Street No.")]
        public string StreetNo { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "Barangay")]
        public string Municipality { get; set; }

        [Display(Name = "City/Province")]
        public string BusinessCity { get; set; }

        [Display(Name = "Main Country of Operation")]
        public string BusinessCountry { get; set; }

        [Display(Name = "Zip Code")]
        public string BusinessZipCode { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string BusinessEmailAddresss { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string BusinessMobile { get; set; }

        [Display(Name = "Landline Number")]
        [DataType(DataType.PhoneNumber)]
        public string BusinessLandline { get; set; }

        [Display(Name = "Date Established")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEstablished { get; set; }
        #endregion

        #region Validate RegisterViewModel
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (MemberAccountType == Role.Designer)
            {
                List<string> addressFieldsInvalid = new List<string>();
                if(String.IsNullOrEmpty(BusinessName))
                {
                    validationResults.Add(new ValidationResult("Designer Label Name is required",
                        new[] { "BusinessName" }));
                }
                if (String.IsNullOrEmpty(BusinessEmailAddresss))
                {
                    validationResults.Add(new ValidationResult("Designer Label E-mail is required",
                        new[] { "BusinessEmailAddresss" }));
                }
                if (String.IsNullOrEmpty(BusinessMobile) && String.IsNullOrEmpty(BusinessLandline))
                {
                    validationResults.Add(new ValidationResult("At least 1 Desginer Label Contact Number is required",
                        new[] { "BusinessMobile" }));
                }
                if (String.IsNullOrEmpty(StreetNo))
                {
                    validationResults.Add(new ValidationResult("Street No. is required",
                        new[] { "StreetNo" }));
                }
                if (String.IsNullOrEmpty(StreetName))
                {
                    validationResults.Add(new ValidationResult("Street Name is required",
                        new[] { "StreetName" }));
                }
                if (String.IsNullOrEmpty(Municipality))
                {
                    validationResults.Add(new ValidationResult("Municipality is required",
                        new[] { "Municipality" }));
                }
                if (String.IsNullOrEmpty(BusinessCity))
                {
                    validationResults.Add(new ValidationResult("City is required",
                        new[] { "BusinessCity" }));
                }
                if (String.IsNullOrEmpty(BusinessZipCode))
                {
                    validationResults.Add(new ValidationResult("Zip Code is required",
                        new[] { "BusinessZipCode" }));
                }

            }

            return validationResults;

        }
        #endregion

    }

}
