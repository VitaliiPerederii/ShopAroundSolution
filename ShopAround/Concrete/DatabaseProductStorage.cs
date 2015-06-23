using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Abstract;
using DataLayer;

namespace ShopAround.Concrete
{
    public class DatabaseProductStorage: IProductStorage
    {
        private ShopAroundEntities context = new ShopAroundEntities();
        public IQueryable<ShopItem> ShopItems
        {
            get
            {
                return context.ShopItem;
            }
        }
    }
}