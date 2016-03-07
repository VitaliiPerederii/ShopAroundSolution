using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopAround.Abstract;

namespace ShopAround.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void LoginControllerTest()
        {
            Mock<IProductStorage> mock = new Mock<IProductStorage>();
            AccountController controller = new AccountController(mock.Object);

            controller.Login(new Models.LogOnModel(), null);

            Assert.Fail();
        }
    }
}