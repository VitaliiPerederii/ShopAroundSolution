using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopAround.Abstract;
using System.Web.Mvc;

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
        }

        [TestMethod()]
        public void Login_CredsCorrectReturnUrlNull_RedirectsToHome()
        {
            Mock<IMembershipMngr> mock = new Mock<IMembershipMngr>();
            mock.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((string one, string two) => true);
            mock.Setup(m => m.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()));

            AccountController controller = new AccountController(null, mock.Object);

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
    }
}