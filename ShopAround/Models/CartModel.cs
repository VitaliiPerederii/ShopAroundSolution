using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAround.Models
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();

        public void AddItem(UiShopItem item)
        {
            CartItem existingItem = items.Find(m => m.ShopItemId == item.Id);
            if (existingItem != null)
                existingItem.Quantity++;
            else
            {
                items.Add(new CartItem()
                    {
                        ShopItemId = item.Id,
                        Quantity = 1
                    });
            }
        }

        public void DeleteItem(UiShopItem item)
        {
            items.Remove(items.Find(m => m.ShopItemId == item.Id));
        }

        public IEnumerable<CartItem> Items
        {
            get
            {
                return items;
            }
        }
    }

    public class CartItem
    {
        public int ShopItemId { get; set; }
        public int Quantity { get; set; }
    }
}