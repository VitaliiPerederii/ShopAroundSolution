using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopAround.Models;

namespace ShopAround.Controllers
{
    public class HomeController : Controller
    {
        ShopAroundEntities db = new ShopAroundEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Shop Around";
            return View();
        }

        [HttpGet]
        public ActionResult AddShopItem()
        {
            SelectList list = new SelectList(db.Category, "Id", "Name");
            ViewBag.Categories = list;
            return View();
        }

        [HttpPost]
        public ActionResult AddShopItem(ShopItem item, HttpPostedFileBase file)
        {

            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, (int)file.InputStream.Length);

            item.Image = uploadedFile;
            db.ShopItem.Add(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ShowItem(int id)
        {
            ShopItem shopItem = db.ShopItem.Where(item => item.Id == id).FirstOrDefault();

            string CategoryName = (from c in db.Category where c.Id == shopItem.CategoryId select c.Name).FirstOrDefault();

            ViewBag.CategoryName = CategoryName;

            return View(shopItem);
        }

        public ActionResult ShowImage(int id)
        {
            ShopItem shopItem = db.ShopItem.Where(item => item.Id == id).FirstOrDefault();
            return File(shopItem.Image, "image/jpg");
        }
    }
}
