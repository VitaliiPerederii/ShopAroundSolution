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
                        new UiShopItem() {  Id = 1, Name = "Name1", Price = 50.0f },
                        new UiShopItem() {  Id = 2, Name = "Name2", Price = 60.0f },
                        new UiShopItem() {  Id = 3, Name = "Name3", Price = 70.0f },
                        new UiShopItem() {  Id = 4, Name = "Name4", Price = 80.0f },
                    }.AsQueryable();
                });
                
                Mock<ICart> mockCart = new Mock<ICart>();
                CartController controller = new CartController(mock.Object);

                ActionResult result = controller.AddItemToCart(mockCart.Object, 4);
                mockCart.Verify(m => m.AddItem(It.Is<UiShopItem>(u => helpfunc(u))), Times.Once());
            }

            bool helpfunc(UiShopItem u)
            {
                return u.Id == 4 && u.Name == "Name4" && u.Price == 80.0f;
            }

            [TestMethod()]
            public void AddItemToCart_ItemNotExists_CallsAddItemNever()
            {
                Mock<IProductStorage> mock = new Mock<IProductStorage>();
                mock.Setup(m => m.ShopItems).Returns(() =>
                {
                    return new List<UiShopItem>()
                    {
                        new UiShopItem() {  Id = 1, Name = "Name1", Price = 50.0f },
                        new UiShopItem() {  Id = 2, Name = "Name2", Price = 60.0f },
                        new UiShopItem() {  Id = 3, Name = "Name3", Price = 70.0f },
                        new UiShopItem() {  Id = 4, Name = "Name4", Price = 80.0f },
                    }.AsQueryable();
                });

                Mock<ICart> mockCart = new Mock<ICart>();
                CartController controller = new CartController(mock.Object);

                ActionResult result = controller.AddItemToCart(mockCart.Object, 5);
                mockCart.Verify(m => m.AddItem(It.IsAny<UiShopItem>()), Times.Never());
            }


            [TestMethod()]
            public void DeleteItemFromCart_ItemExists_CallsDeleteItemOnce()
            {
                Mock<IProductStorage> mock = new Mock<IProductStorage>();
                mock.Setup(m => m.ShopItems).Returns(() =>
                {
                    return new List<UiShopItem>()
                    {
                        new UiShopItem() {  Id = 1, Name = "Name1", Price = 50.0f },
                        new UiShopItem() {  Id = 2, Name = "Name2", Price = 60.0f },
                        new UiShopItem() {  Id = 3, Name = "Name3", Price = 70.0f },
                        new UiShopItem() {  Id = 4, Name = "Name4", Price = 80.0f },
                    }.AsQueryable();
                });

                Mock<ICart> mockCart = new Mock<ICart>();
                CartController controller = new CartController(mock.Object);

                ActionResult result = controller.DeleteItemFromCart(mockCart.Object, 4);
                mockCart.Verify(m => m.DeleteItem(It.Is<UiShopItem>(u => helpfunc(u))), Times.Once());
            }

            [TestMethod()]
            public void DeleteItemFromCart_ItemNotExists_CallsDeleteItemNever()
            {
                Mock<IProductStorage> mock = new Mock<IProductStorage>();
                mock.Setup(m => m.ShopItems).Returns(() =>
                {
                    return new List<UiShopItem>()
                    {
                        new UiShopItem() {  Id = 1, Name = "Name1", Price = 50.0f },
                        new UiShopItem() {  Id = 2, Name = "Name2", Price = 60.0f },
                        new UiShopItem() {  Id = 3, Name = "Name3", Price = 70.0f },
                        new UiShopItem() {  Id = 4, Name = "Name4", Price = 80.0f },
                    }.AsQueryable();
                });

                Mock<ICart> mockCart = new Mock<ICart>();
                CartController controller = new CartController(mock.Object);

                ActionResult result = controller.DeleteItemFromCart(mockCart.Object, 5);
                mockCart.Verify(m => m.DeleteItem(It.IsAny<UiShopItem>()), Times.Never());
            }
        }
    }
}
