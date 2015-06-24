using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAround.Models;

namespace ShopAround.Abstract
{
    public interface IProductStorage
    {
        IQueryable<UiShopItem> ShopItems { get; }
        void AddShopItem(UiShopItem item);

        IQueryable<UiCategory> Categories { get; }

        UiUser FindUser(string name);
        void AddUser(UiUser user);
    }
}
