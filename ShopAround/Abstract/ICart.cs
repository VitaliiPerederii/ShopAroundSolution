using ShopAround.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAround.Abstract
{
    public interface ICart
    {
        void AddItem(UiShopItem item);
        void DeleteItem(UiShopItem item);
        void Clear();
        IEnumerable<CartItem> Items { get; }
        double Total { get; }
    }
}
