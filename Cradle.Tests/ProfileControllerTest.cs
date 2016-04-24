using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cradle.Controllers;
using System.Web.Mvc;
using Cradle.Models.Repository;
using System.Web;
using System.Security.Principal;
using Cradle.Models;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Cradle.Tests
{
    [TestClass]
    public class ProfileControllerTest
    {

        public class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user;

            public MockHttpContext(int role) : base()
            { 
                if(role == 2)
                {
                    _user = new GenericPrincipal(new GenericIdentity("TestDesigner"),
                    new string[] { "Designer" });
                }
                else if(role == 1)
                {
                    _user = new GenericPrincipal(new GenericIdentity("TestMember"),
                    new string[] { "Member" });
                }
                else
                {
                    _user = new GenericPrincipal(new GenericIdentity("TestAnonymous"), new string[]{"Anonymous"});
                }
            }

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }

        }

        private ProfileController _controller;
        public ProfileController Controller { 
            get
            {
                return _controller;
            }
        }

        private void InitializeProfileController(int role)
        {
            _controller = new ProfileController(new MockProfileManager(this));

            _controller.ControllerContext = new ControllerContext()
            {
                Controller = _controller,
                RequestContext =
                new RequestContext(new MockHttpContext(role), new RouteData())
            };
        }

        [TestMethod]
        public void ManageDesignerProfile_IsNotNull_DisplayProfile()
        {

            InitializeProfileController(2); //Designer

            //Act
            var result = _controller.View() as ViewResult;

            //Assert
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(result.ViewName,string.Empty);
        }

        [TestMethod]
        public void ManageDesignerProfile_IsNull_RedirectToHome()
        {
            InitializeProfileController(1);  //Member

            //Act
            RedirectToRouteResult result = _controller.View() as RedirectToRouteResult;
            
            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

     
    }
}
