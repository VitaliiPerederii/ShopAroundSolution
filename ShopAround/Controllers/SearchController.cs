using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ShopAround.Abstract;
using ShopAround.Models;

namespace ShopAround.Controllers
{
    public class SearchController : Controller
    {
        IProductStorage db;

        public SearchController(IProductStorage storage)
        {
            db = storage;
        }

        [HttpGet]
        public ActionResult Search()
        {
            List<UiCategory> catList = db.Categories.ToList();
            catList.Insert(0, new UiCategory() { Id = -1, Name = "None" });

            SelectList list = new SelectList(catList, "Id", "Name");
            
            ViewBag.Categories = list;
            
            double minPrice = db.ShopItems.Min(m => m.Price);
            double maxPrice = db.ShopItems.Max(m => m.Price);

            double increment = (maxPrice - minPrice) / 10;
            List<double> priceList = new List<double>();

            for (double value = minPrice; value < maxPrice; value += increment)
                priceList.Add(value);

            priceList.Add(maxPrice);

            ViewBag.PriceList = new SelectList(priceList);

            return PartialView(new SearchModel() { MinPrice = minPrice, MaxPrice = maxPrice });
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            SelectList list = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = list;
            return PartialView();
        }

    }
}
