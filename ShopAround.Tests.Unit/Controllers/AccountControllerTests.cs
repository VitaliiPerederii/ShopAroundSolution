using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopAround.Abstract;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopAround.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void Login_CredsIncorrect_ReturnsViewWithModel()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two) => false);
            AccountController controller = new AccountController(null, mock.Object);

            Models.LogOnModel model = new Models.LogOnModel()
            {
                Email = "test",
                Password = "test1"
            };

            ActionResult result = controller.Login(model, null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult viewRes = result as ViewResult;
            Assert.AreSame(viewRes.Model, model);
            Assert.IsFalse(viewRes.ViewData.ModelState.IsValid);
        }

        [TestMethod()]
        public void Login_CredsCorrectReturnUrlNull_RedirectsToHome()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two) => true);
            mock.Setup(m => m.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()));

            Mock<IPlatformProvider> mockPlatform = new Mock<IPlatformProvider>();
            mockPlatform.Setup(m => m.IsLocalUrl(It.IsAny<string>())).Returns(false);

            AccountController controller = new AccountController(null, mock.Object);
            controller.PlatformProvider = mockPlatform.Object;

            Models.LogOnModel model = new Models.LogOnModel()
            {
                Email = "test",
                Password = "test1"
            };

            ActionResult result = controller.Login(model, null);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            RedirectToRouteResult viewRes = result as RedirectToRouteResult;
            Assert.AreEqual(viewRes.RouteValues["action"], "Index");
            Assert.AreEqual(viewRes.RouteValues["controller"], "Home");
        }

        [TestMethod()]
        public void Login_CredsCorrectReturnUrlNotNull_RedirectsUrl()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two) => true);
            mock.Setup(m => m.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()));

            Mock<IPlatformProvider> mockPlatform = new Mock<IPlatformProvider>();
            mockPlatform.Setup(m => m.IsLocalUrl(It.IsAny<string>())).Returns(true);

            AccountController controller = new AccountController(null, mock.Object);
            controller.PlatformProvider = mockPlatform.Object;

            Models.LogOnModel model = new Models.LogOnModel()
            {
                Email = "test",
                Password = "test1"
            };

            ActionResult result = controller.Login(model, "zeep.com");
            Assert.IsInstanceOfType(result, typeof(RedirectResult));

            RedirectResult viewRes = result as RedirectResult;
            Assert.AreEqual(viewRes.Url, "zeep.com");
        }


        [TestMethod()]
        public void Register_CredsIncorrect_ReturnsViewWithModel()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two, string three) => null);

            AccountController controller = new AccountController(null, mock.Object);

            Models.RegisterModel model = new Models.RegisterModel()
            {
                Email = "test",
                Password = "test1",
                Name = "test"
            };

            ActionResult result = controller.Register(model);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            ViewResult viewRes = result as ViewResult;
            Assert.AreSame(viewRes.Model, model);
            Assert.IsFalse(viewRes.ViewData.ModelState.IsValid);
        }


        class CustomUser: MembershipUser { }

        [TestMethod()]
        public void Register_CredsСorrect_RedirectsToHome()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two, string three) =>
            {
               return new CustomUser();
            });
            mock.Setup(m => m.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()));

            AccountController controller = new AccountController(null, mock.Object);

            Models.RegisterModel model = new Models.RegisterModel()
            {
                Email = "test",
                Password = "test1",
                Name = "test"
            };

            ActionResult result = controller.Register(model);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            RedirectToRouteResult viewRes = result as RedirectToRouteResult;
            Assert.AreEqual(viewRes.RouteValues["action"], "Index");
            Assert.AreEqual(viewRes.RouteValues["controller"], "Home");
        }

    }
}