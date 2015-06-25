using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItemToCart(int id)
        {
            Cart cart = GetCart();
            if (cart != null)
                cart.AddItem(id);

            return RedirectToAction("Index");
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
