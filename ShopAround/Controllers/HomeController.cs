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

        public ActionResult Index(int? page, int? categoryId)
        {
            ViewBag.Title = "Shop Around";
            List<UiShopItem> list = null;
            if (categoryId != null)
                list = db.ShopItems.Where(i => i.CategoryId == categoryId).ToList();
            else
                list = db.ShopItems.ToList();


            int itemsOnPage = 3;
            int itemsCount = list.Count;
            ViewBag.PagesCount = (int)Math.Ceiling(((double)itemsCount / (double)itemsOnPage));
                        
            int startIndex = page != null? (page.Value - 1) * itemsOnPage : 0;
            int countOnPage = Math.Min(itemsCount - startIndex, 3);
            list = list.GetRange(startIndex, countOnPage);
            return View(list);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddShopItem()
        {
            SelectList list = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = list;
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult AddShopItem(UiShopItem item, HttpPostedFileBase file)
        {

            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, (int)file.InputStream.Length);

            item.Image = uploadedFile;
            db.AddShopItem(item);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditShopItem(int id)
        {
            UiShopItem shopItem = db.ShopItems.Where(s => s.Id == id).FirstOrDefault();
            SelectList list = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = list;
            return View(shopItem);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditShopItem(UiShopItem item, HttpPostedFileBase file)
        {
            byte[] uploadedFile = null;
            if (file != null)
            {
                uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, (int)file.InputStream.Length);
                item.Image = uploadedFile;
            }
            else
                item.Image = db.ShopItems.Where(i => i.Id == item.Id).FirstOrDefault().Image;
                        
            db.UpdateShopItem(item);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteShopItem(int id)
        {
            UiShopItem shopItem = db.ShopItems.Where(s => s.Id == id).FirstOrDefault();
            ViewBag.CategoryName = shopItem.Category.Name;
            return View(shopItem);
        }

        [HttpPost, ActionName("DeleteShopItem")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteShopItemConfirm(int id)
        {
            db.DeleteShopItem(id);
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
            if (shopItem.Image != null)
                return File(shopItem.Image, "image/jpg");
            else
                return null;
        }

        public ActionResult ShowCategories()
        {
            return PartialView(db.Categories);
        }
    }
}
