using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAround.Models;
using ShopAround.Abstract;
using System.Web.Security;

namespace ShopAround.Controllers
{
    public class CartController : Controller
    {
        IProductStorage db;
        public CartController(IProductStorage storage)
        {
            db = storage;
        }
        
        public ActionResult Index()
        {
            return View(GetCart());
        }

        [HttpPost]
        public ActionResult AddItemToCart(int id)
        {
            Cart cart = GetCart();
            UiShopItem item = db.ShopItems.Where(i => i.Id == id).FirstOrDefault();
            if (cart != null && item != null)
                cart.AddItem(item);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteItemFromCart(int id)
        {
            Cart cart = GetCart();
            UiShopItem item = db.ShopItems.Where(i => i.Id == id).FirstOrDefault();
            if (cart != null && item != null)
                cart.DeleteItem(item);

            return RedirectToAction("Index");
        }

        public string GetCartItemsCount()
        {
            string count = "0";
            Cart cart = GetCart();
            if (cart != null)
                count = cart.Items.Count().ToString();

            return count;
        }

        public string GetCartItemsPrice()
        {
            string price = "0";
            Cart cart = GetCart();
            if (cart != null)
                price = cart.Total.ToString();

            return price;
        }

        [HttpGet]
        public ActionResult MakeOrder()
        {
            UiOrder order = new UiOrder();
            if (Request.IsAuthenticated)
            {
                UiUser user = db.FindUser(Request.RequestContext.HttpContext.User.Identity.Name);
                order.CustomerEmail = user.Email;
                order.CustomerName = user.Name;
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult MakeOrder(UiOrder order)
        {
            if (ModelState.IsValid)
            {
                Cart cart = GetCart();
                order.Cart = cart;
                order.Date = DateTime.Now;
                db.CommitOrder(order);
                cart.Clear();
            }
            else
                return View(order);


            return RedirectToAction("Index", "Home");
        }
        
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
