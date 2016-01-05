using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibernateDataLayer
{
    public class OrderShopItem
    {
        public int OrderId { get; set; }
        public int ShopItemId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ShopItem ShopItem { get; set; }
    }
}
