using ShopAround.Abstract;
using ShopAround.Controllers;
using ShopAround.Models;
using System.Linq;

namespace ShopAround.Tests.Unit.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Web.Mvc;

    namespace ShopAround.Controllers.Tests
    {
        [TestClass()]
        public class CartControllerTests
        {
            [TestMethod()]
            public void AddItemToCart_ItemExists_CallsAddItemOnce()
            {
                Mock<IProductStorage> mock = new Mock<IProductStorage>();
                mock.Setup(m => m.ShopItems).Returns(() =>
                {
                    return new List<UiShopItem>()
                    {
                        new UiShopItem() {  Id = 1, Name = "Name1", Price = 50.0f }
                    }.AsQueryable();
                });
                CartController controller = new CartController(mock.Object);

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
