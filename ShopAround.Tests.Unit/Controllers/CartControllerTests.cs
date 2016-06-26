using ShopAround.Abstract;
using ShopAround.Controllers;

namespace ShopAround.Tests.Unit.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Web.Mvc;

    namespace ShopAround.Controllers.Tests
    {
        [TestClass()]
        public class CartControllerTests
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
        }
    }
}
