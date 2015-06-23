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
            //SelectList list = new SelectList(db.Category, "Id", "Name");
            //ViewBag.Categories = list;
            return View();
        }

        [HttpPost]
        public ActionResult AddShopItem(/*ShopItem item,*/ HttpPostedFileBase file)
        {

            //byte[] uploadedFile = new byte[file.InputStream.Length];
            //file.InputStream.Read(uploadedFile, 0, (int)file.InputStream.Length);

            //item.Image = uploadedFile;
            //db.ShopItem.Add(item);
            //db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ShowItem(int id)
        {
            db.ShopItems.Count();
            UiShopItem shopItem = (from s in db.ShopItems where s.Id == id select new ModelCastUtils.ToUiShopItem(s)).FirstOrDefault();
            ViewBag.CategoryName = shopItem.Category.Name;

            return View(shopItem);
        }

        public ActionResult ShowImage(int id)
        {
            //ShopItem shopItem = db.ShopItem.Where(item => item.Id == id).FirstOrDefault();
            //return File(shopItem.Image, "image/jpg");
            return View();
        }
    }
}
