using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAround.Models
{
    public class UiShopItem
    {
        public UiShopItem()
        {
            //this.OrderShopItem = new HashSet<OrderShopItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public byte[] Image { get; set; }
        public virtual UiCategory Category { get; set; }
        //public virtual ICollection<OrderShopItem> OrderShopItem { get; set; }
    }
}