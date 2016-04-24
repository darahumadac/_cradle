using Cradle.Controllers;
using Cradle.Models;
using Cradle.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cradle.Tests
{
    public class MockProfileManager : IProfileManager
    {
        public MockProfileManager(ProfileControllerTest controllerTest)
        {
            ProfileControllerTest = controllerTest;

        }
        public ProfileControllerTest ProfileControllerTest { get; set; }

        public DesignerProfileViewModel GetDesignerProfile(string userId)
        {
            if(ProfileControllerTest.Controller.ControllerContext.RequestContext.HttpContext.User.IsInRole("Anonymous") ||
                ProfileControllerTest.Controller.ControllerContext.RequestContext.HttpContext.User.IsInRole("Member"))
            {
                return null;
            }
            else
            {
                return new DesignerProfileViewModel()
                {
                    Name = "Darah",
                    Address = new Address() { City = "Mandaluyong" },
                    Email = "darahfumadac@gmail.com",
                    Collections = null
                };
            }
           
        }

        public ManageDesignerProfileViewModel GetDesignerInformation(string userId)
        {
            throw new NotImplementedException();
        }

        public ProfileResult UpdateDesignerProfile(ManageDesignerProfileViewModel profile)
        {
            throw new NotImplementedException();
        }


        public ProfileResult UpdateDesignerProfile(string userId, ManageDesignerProfileViewModel profile)
        {
            throw new NotImplementedException();
        }

        public DesignerProfile GetDesigner(string userId)
        {
            throw new NotImplementedException();
        }

        public PersonalProfile GetPerson(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
