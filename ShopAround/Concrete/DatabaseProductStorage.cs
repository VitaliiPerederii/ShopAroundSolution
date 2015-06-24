﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Abstract;
using ShopAround.Utils;
using ShopAround.Models;
using DataLayer;

namespace ShopAround.Concrete
{
    public class DatabaseProductStorage: IProductStorage
    {
        private ShopAroundEntities context = new ShopAroundEntities();
        public IQueryable<UiShopItem> ShopItems
        {
            get
            {
                List<UiShopItem> list = new List<UiShopItem>();
                foreach (ShopItem item in context.ShopItem)
                    list.Add(ModelCastUtils.ToUiShopItem(item));
                
                return list.AsQueryable() ;
            }
        }

        public void AddShopItem(UiShopItem uiItem)
        {
            ShopItem item = ModelCastUtils.FromUiShopItem(uiItem);
            context.ShopItem.Add(item);
            context.SaveChanges();
        }

        public IQueryable<UiCategory> Categories
        {
            get
            {
                List<UiCategory> list = new List<UiCategory>();
                foreach (Category cat in context.Category)
                    list.Add(ModelCastUtils.ToUiShopCategory(cat));

                return list.AsQueryable();
            }
        }

        public UiUser FindUser(string name)
        {
            UiUser uiUser = ModelCastUtils.ToUiUser(context.User.Where(u => u.Email == name).FirstOrDefault());
            return uiUser;
        }

        public void AddUser(UiUser user)
        {
            context.User.Add(ModelCastUtils.FromUiUser(user));
            context.SaveChanges();
        }
    }
}