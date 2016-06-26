using ShopAround.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace ShopAround.Models
{
    public class Cart: ICart
    {
        private List<CartItem> items = new List<CartItem>();

        public void AddItem(UiShopItem item)
        {
            CartItem existingItem = items.Find(m => m.ShopItem.Id == item.Id);
            if (existingItem != null)
                existingItem.Quantity++;
            else
            {
                items.Add(new CartItem()
                    {
                        ShopItem = item,
                        Quantity = 1
                    });
            }
        }

        public void DeleteItem(UiShopItem item)
        {
            items.Remove(items.Find(m => m.ShopItem.Id == item.Id));
        }

        public void Clear()
        {
            items.Clear();
        }

        public IEnumerable<CartItem> Items
        {
            get
            {
                return items;
            }
        }

        public double Total
        {
            get
            {
                return items.Sum(i =>
                    {
                        return i.Amount;
                    });
            }
        }
    }

    public class CartItem
    {
        public UiShopItem ShopItem { get; set; }
        public int Quantity { get; set; }

        public double Amount
        {
            get
            {
                return ShopItem.Price * Quantity;
            }
        }
    }
}