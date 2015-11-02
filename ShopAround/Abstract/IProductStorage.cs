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
        void UpdateShopItem(UiShopItem item);
        void DeleteShopItem(int Id);
        IQueryable<UiCategory> Categories { get; }
        UiUser FindUser(string name);
        void AddUser(UiUser user);
        void AddRole(UiRole role);
        void CommitOrder(UiOrder order);
    }
}
