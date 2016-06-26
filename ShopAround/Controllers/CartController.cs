using System;
using System.Linq;
using System.Web.Mvc;
using ShopAround.Models;
using ShopAround.Abstract;

namespace ShopAround.Controllers
{
    public class CartController : Controller
    {
        IProductStorage db;
        public CartController(IProductStorage storage)
        {
            db = storage;
        }
        
        public ActionResult Index(Cart cart)
        {
            return View(cart);
        }

        [HttpPost]
        public ActionResult AddItemToCart(Cart cart, int id)
        {
            UiShopItem item = db.ShopItems.Where(i => i.Id == id).FirstOrDefault();
            if (cart != null && item != null)
                cart.AddItem(item);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteItemFromCart(Cart cart, int id)
        {
            UiShopItem item = db.ShopItems.Where(i => i.Id == id).FirstOrDefault();
            if (cart != null && item != null)
                cart.DeleteItem(item);

            return RedirectToAction("Index");
        }

        public string GetCartItemsCount(Cart cart)
        {
            string count = "0";
            if (cart != null)
                count = cart.Items.Count().ToString();

            return count;
        }

        public string GetCartItemsPrice(Cart cart)
        {
            string price = "0";
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
        public ActionResult MakeOrder(Cart cart, UiOrder order)
        {
            if (ModelState.IsValid)
            {
                order.Cart = cart;
                order.Date = DateTime.Now;
                db.CommitOrder(order);
                cart.Clear();
            }
            else
                return View(order);

            return RedirectToAction("Index", "Home");
        }
    }
}
