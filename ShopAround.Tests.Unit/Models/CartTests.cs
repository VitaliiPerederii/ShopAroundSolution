using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopAround.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAround.Models.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void AddItem_Create_CountZero()
        {
            Cart cart = new Cart();

            Assert.AreEqual(cart.Items.Count(), 0);
            Assert.AreEqual(cart.Total, 0.0f);
        }

        [TestMethod()]
        public void AddItem_AddOne_CountOne()
        {
            Cart cart = new Cart();

            cart.AddItem(new UiShopItem() { Id = 1, Price = 10.0f });

            Assert.AreEqual(cart.Items.Count(), 1);
            Assert.AreEqual(cart.Total, 10.0f);
        }

        [TestMethod()]
        public void AddItem_AddTwoDifferent_CountTwo()
        {
            Cart cart = new Cart();

            cart.AddItem(new UiShopItem() { Id = 1, Price = 10.0f });
            cart.AddItem(new UiShopItem() { Id = 2, Price = 20.0f });

            Assert.AreEqual(cart.Items.Count(), 2);
            Assert.AreEqual(cart.Total, 30.0f);
        }

        [TestMethod()]
        public void AddItem_AddTwoSame_CountOne()
        {
            Cart cart = new Cart();

            cart.AddItem(new UiShopItem() { Id = 1, Price = 10.0f });
            cart.AddItem(new UiShopItem() { Id = 1, Price = 10.0f });

            Assert.AreEqual(cart.Items.Count(), 1);
            Assert.AreEqual(cart.Total, 20.0f);
        }

        [TestMethod()]
        public void DeleteItem_DeleteOne_CountMinusOne()
        {
            Cart cart = new Cart();

            cart.AddItem(new UiShopItem() { Id = 1, Price = 10.0f });
            cart.AddItem(new UiShopItem() { Id = 2, Price = 20.0f });

            cart.DeleteItem(new UiShopItem() { Id = 2 });

            Assert.AreEqual(cart.Items.Count(), 1);
            Assert.IsFalse(cart.Items.Any(i => i.ShopItem.Id == 2));
            Assert.AreEqual(cart.Total, 10.0f);
        }

        [TestMethod()]
        public void Clear_Clear_CountZero()
        {
            Cart cart = new Cart();

            cart.AddItem(new UiShopItem() { Id = 1 });
            cart.AddItem(new UiShopItem() { Id = 2 });

            cart.Clear();
            Assert.AreEqual(cart.Items.Count(), 0);
            Assert.AreEqual(cart.Total, 0.0f);
        }
    }
}