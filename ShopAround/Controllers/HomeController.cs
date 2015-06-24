using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAround.Abstract;
using ShopAround.Models;
using ShopAround.Utils;


namespace ShopAround.Controllers
{
    public class HomeController : Controller
    {
        private IProductStorage db;

        public HomeController(IProductStorage Db)
        {
            this.db = Db;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Shop Around";
            return View();
        }

        [HttpGet]
        public ActionResult AddShopItem()
        {
            SelectList list = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = list;
            return View();
        }

        [HttpPost]
        public ActionResult AddShopItem(UiShopItem item, HttpPostedFileBase file)
        {

            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, (int)file.InputStream.Length);

            item.Image = uploadedFile;
            db.AddShopItem(item);

            return RedirectToAction("Index");
        }

        public ActionResult ShowItem(int id)
        {
            UiShopItem shopItem = db.ShopItems.Where(s => s.Id == id).FirstOrDefault();
            ViewBag.CategoryName = shopItem.Category.Name;
            return View(shopItem);
        }

        public ActionResult ShowImage(int id)
        {
            UiShopItem shopItem = db.ShopItems.Where(s => s.Id == id).FirstOrDefault();
            return File(shopItem.Image, "image/jpg");
        }
    }
}
