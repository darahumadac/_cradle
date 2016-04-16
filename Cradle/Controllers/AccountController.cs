using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Cradle.Models.Repository;
using Cradle.Models;

#region To-Do list
//Site Functionality
//TODO: Add Profiles when register via social login: Facebook
//TODO: Update external registration saving contact number with country code
//TODO: Create private methods for filling in data for creating users
//TODO: Add Size property for Items (E.G. Waistline, etc.)
//TODO: Add the following fields in all tables:
 //   - Created Date
 //   - Created By
 //   - Updated Date
 //   - Updated By
 //

//Security / Optimization
//TODO: If another user successfully logs in already logged in account, user must be disconnected; check claims for this
//TODO: Add session handling / session timeout
//TODO: Add failed login count functionality / password reset / forgot password
//TODO: Configure getting data from db. avoid always making connections; Get all counts and lookup values and save them in a class.
//TODO: SQL Injection, Direct Object Reference, Cross site scripting issues

#endregion

namespace Cradle.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController() 
            : this(new CradleUserManager(new CradleUserStore(new CradleDbContext())))
        {
            
        }


        public AccountController(CradleUserManager userManager)
        {
            UserManager = userManager;
        }

        public CradleUserManager UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    if (returnUrl != null)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        if (!user.DesignerProfile.IsProfileComplete)
                        {
                            return RedirectToAction("Manage", "Profile");
                        }

                        return RedirectToAction("Dashboard", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Account() 
                { 
                    UserName = model.UserName,
                    EmailAddress = model.EmailAddress,
                    SecurityQuestion = model.SecurityQuestion,
                    SecurityAnswer = model.SecurityAnswer,
                    IsActive = true,
                    FailedLoginCount = 0
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.MemberAccountType == Models.Enums.Role.Member)
                    { 
                        UserManager.AddToRole(user.Id, "Member");
                    }
                    else if (model.MemberAccountType == Models.Enums.Role.Designer)
                    {
                        UserManager.AddToRole(user.Id, "Designer");
                    }

                    //Add Personal Address
                    var userAddress = new Address()
                    {
                        City = model.City,
                        Country = model.Country
                    };
                    UserManager.UserStore.AddAddress(userAddress);

                    //Add Personal Profile
                    var userProfile = new PersonalProfile()
                    {
                        PersonalProfileId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Birthdate = model.BirthDate,
                        Address = userAddress
                    };

                    UserManager.UserStore.AddPersonalProfile(userProfile);

                    //Add Personal Contact No
                    string[] mobileNoParts = model.MobileNo.Split(' ');
                    var personalContact = new ContactNumber()
                    {
                        CountryCodeMobile = mobileNoParts[0],
                        MobileNo = mobileNoParts[1],
                        PersonalProfile = userProfile
                    };
                    UserManager.UserStore.AddContactNo(personalContact);

                    if(model.MemberAccountType == Models.Enums.Role.Designer)
                    {
                        mobileNoParts = model.BusinessMobile.Split(' ');
                        var designerContact = new ContactNumber();
                        if (!String.IsNullOrWhiteSpace(mobileNoParts[1]))
                        {
                            designerContact.CountryCodeMobile = mobileNoParts[0];
                            designerContact.MobileNo = mobileNoParts[1];
                        }
                        designerContact.LandlineNo = model.BusinessLandline;
                      

                        var designerAddress = new Address()
                        {
                            City = model.BusinessCity,
                            Country = model.BusinessCountry,
                            Municipality = model.Municipality,
                            StreetName = model.StreetName,
                            StreetNo = model.StreetNo,
                            ZipCode = model.BusinessZipCode

                        };

                        UserManager.UserStore.AddAddress(designerAddress);

                        var designerProfile = new DesignerProfile()
                        {
                            DesignerProfileID = user.Id,
                            Address = designerAddress,
                            Birthdate = model.DateEstablished,
                            BusinessEmailAddress = model.BusinessEmailAddresss,
                            BusinessName = model.BusinessName,
                            ContactNumber = new List<ContactNumber>() { designerContact },
                            ProfileStats = new Statistics()
                            {
                                AveRating = 0,
                                LikeCount = 0,
                                TagCount = 0,
                                ViewCount = 0
                            },
                            IsRTW = model.IsRTW,
                            IsCustomMade = model.IsCustomMade,
                            IsProfileComplete = false

                        };

                        UserManager.UserStore.AddDesignerProfile(designerProfile);

                    }

                    await SignInAsync(user, isPersistent: false);
                    if (!user.DesignerProfile.IsProfileComplete)
                    {
                        return RedirectToAction("Manage", "Profile");
                    }
                    return RedirectToAction("Dashboard", "Profile");

                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", 
                "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
           
            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                if(returnUrl != null)
                { 
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    if(!user.DesignerProfile.IsProfileComplete)
                    {
                        return RedirectToAction("Manage", "Profile");
                    }

                    return RedirectToAction("Dashboard", "Profile");
                    
                }
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                var identityInfo = await AuthenticationManager.GetExternalIdentityAsync("ExternalCookie");

                ExternalLoginConfirmationViewModel confirmationModel = new ExternalLoginConfirmationViewModel();
                foreach (var userClaim in identityInfo.Claims)
                {
                    switch (userClaim.Type)
                    { 
                        case "email":
                            confirmationModel.EmailAddress = userClaim.Value;
                            break;
                        case "familyName":
                            confirmationModel.LastName = userClaim.Value;
                            break;
                        case "givenName":
                            confirmationModel.FirstName = userClaim.Value;
                            break;
                        //case "birthday":
                        //    DateTime userBirthdate = DateTime.Today;
                        //    DateTime.TryParse(userClaim.Value, out userBirthdate);
                        //    confirmationModel.BirthDate = userBirthdate;
                        //    break;
                        //case "city":
                        //    confirmationModel.City = userClaim.Value;
                        //    break;
                        //case "country":
                        //    confirmationModel.Country = userClaim.Value;
                        //    break;
                        //case "mobileNo":
                        //    confirmationModel.MobileNo = userClaim.Value;
                        //    break;
                    }
                }
                confirmationModel.UserName = loginInfo.DefaultUserName;
                //set all other personal profile to textfields

                return View("ExternalLoginConfirmation", confirmationModel);
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new Account() 
                { 
                    UserName = model.UserName,
                    EmailAddress = model.EmailAddress,
                    IsActive = true,
                    FailedLoginCount = 0
                };
                
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        if(model.MemberAccountType == Models.Enums.Role.Member)
                        {
                            UserManager.AddToRole(user.Id, "Member");
                        }
                        else
                        {
                            UserManager.AddToRole(user.Id, "Designer");
                        }

                        //Add Personal Address
                        var userAddress = new Address()
                        {
                            City = model.City,
                            Country = model.Country
                        };

                        UserManager.UserStore.AddAddress(userAddress);

                        //Add Personal Profile
                        var userProfile = new PersonalProfile()
                        {
                            PersonalProfileId = user.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Birthdate = model.BirthDate,
                            Address = userAddress
                        };

                        UserManager.UserStore.AddPersonalProfile(userProfile);

                        //Add Personal Contact No
                        var personalContact = new ContactNumber()
                        {
                            MobileNo = model.MobileNo,
                            PersonalProfile = userProfile
                        };
                        UserManager.UserStore.AddContactNo(personalContact);

                        //If Designer Account...
                        if (model.MemberAccountType == Models.Enums.Role.Designer)
                        {
                            var designerContact = new ContactNumber()
                            {
                                MobileNo = model.BusinessMobile,
                                LandlineNo = model.BusinessLandline
                            };

                            var designerAddress = new Address()
                            {
                                City = model.BusinessCity,
                                Country = model.BusinessCountry,
                                Municipality = model.Municipality,
                                StreetName = model.StreetName,
                                StreetNo = model.StreetNo,
                                ZipCode = model.BusinessZipCode

                            };

                            UserManager.UserStore.AddAddress(designerAddress);

                            var designerProfile = new DesignerProfile()
                            {
                                DesignerProfileID = user.Id,
                                Address = designerAddress,
                                Birthdate = model.DateEstablished,
                                BusinessEmailAddress = model.BusinessEmailAddresss,
                                BusinessName = model.BusinessName,
                                ContactNumber = new List<ContactNumber>() { designerContact },
                                ProfileStats = new Statistics()
                                {
                                    AveRating = 0,
                                    LikeCount = 0,
                                    TagCount = 0,
                                    ViewCount = 0
                                },
                                IsRTW = model.IsRTW,
                                IsCustomMade = model.IsCustomMade,
                                IsProfileComplete = false

                            };

                            UserManager.UserStore.AddDesignerProfile(designerProfile);

                        }

                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(Account user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region Functions
        private void FillUserDetails(BaseRegistrationViewModel model, string userID)
        {

            if (model.MemberAccountType == Models.Enums.Role.Member)
            {
                UserManager.AddToRole(userID, "Member");
            }
            else if (model.MemberAccountType == Models.Enums.Role.Designer)
            {
                UserManager.AddToRole(userID, "Designer");
            }

            //Add Personal Address
            var userAddress = new Address()
            {
                City = model.City,
                Country = model.Country
            };
            UserManager.UserStore.AddAddress(userAddress);

            //Add Personal Profile
            var userProfile = new PersonalProfile()
            {
                PersonalProfileId = userID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthdate = model.BirthDate,
                Address = userAddress
            };

            UserManager.UserStore.AddPersonalProfile(userProfile);

            //Add Personal Contact No
            string[] mobileNoParts = model.MobileNo.Split(' ');
            var personalContact = new ContactNumber()
            {
                CountryCodeMobile = mobileNoParts[0],
                MobileNo = mobileNoParts[1],
                PersonalProfile = userProfile
            };
            UserManager.UserStore.AddContactNo(personalContact);

            if (model.MemberAccountType == Models.Enums.Role.Designer)
            {
                mobileNoParts = model.BusinessMobile.Split(' ');
                var designerContact = new ContactNumber();
                if (!String.IsNullOrWhiteSpace(mobileNoParts[1]))
                {
                    designerContact.CountryCodeMobile = mobileNoParts[0];
                    designerContact.MobileNo = mobileNoParts[1];
                }
                designerContact.LandlineNo = model.BusinessLandline;


                var designerAddress = new Address()
                {
                    City = model.BusinessCity,
                    Country = model.BusinessCountry,
                    Municipality = model.Municipality,
                    StreetName = model.StreetName,
                    StreetNo = model.StreetNo,
                    ZipCode = model.BusinessZipCode

                };

                UserManager.UserStore.AddAddress(designerAddress);

                var designerProfile = new DesignerProfile()
                {
                    DesignerProfileID = userID,
                    Address = designerAddress,
                    Birthdate = model.DateEstablished,
                    BusinessEmailAddress = model.BusinessEmailAddresss,
                    BusinessName = model.BusinessName,
                    ContactNumber = new List<ContactNumber>() { designerContact },
                    ProfileStats = new Statistics()
                    {
                        AveRating = 0,
                        LikeCount = 0,
                        TagCount = 0,
                        ViewCount = 0
                    },
                    IsRTW = model.IsRTW,
                    IsCustomMade = model.IsCustomMade,
                    IsProfileComplete = false

                };

                UserManager.UserStore.AddDesignerProfile(designerProfile);

            }

        }

        #endregion
    }
}