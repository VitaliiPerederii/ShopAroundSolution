using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Models;
using DataLayer;

namespace ShopAround.Utils
{
    public class ModelCastUtils
    {
        public static UiShopItem ToUiShopItem(ShopItem item)
        {
            UiShopItem uiItem = new UiShopItem();
            uiItem.Id = item.Id;
            uiItem.Name = item.Name;
            uiItem.Description = item.Description;
            uiItem.Price = item.Price;
            uiItem.Image = item.Image;
            uiItem.Available = item.Available;
            uiItem.Category = ToUiShopCategory(item.Category);
            return uiItem;
        }

        public static ShopItem FromUiShopItem(UiShopItem uiItem)
        {
            ShopItem item = new ShopItem();
            item.Id = uiItem.Id;
            item.Name = uiItem.Name;
            item.Description = uiItem.Description;
            item.Price = uiItem.Price;
            item.Image = uiItem.Image;
            item.Available = uiItem.Available;
            item.Category = FromUiShopCategory(uiItem.Category);
            return item;
        }

        public static UiCategory ToUiShopCategory(Category cat)
        {
            UiCategory uiCat = new UiCategory();
            uiCat.Id = cat.Id;
            uiCat.Name = cat.Name;
            return uiCat;
        }

        public static Category FromUiShopCategory(UiCategory uiCat)
        {
            Category cat = new Category();
            cat.Id = uiCat.Id;
            cat.Name = uiCat.Name;
            return cat;
        }
    }
}