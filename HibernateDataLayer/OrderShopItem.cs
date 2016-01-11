using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibernateDataLayer
{
    public class OrderShopItem
    {
        public virtual int OrderId { get; set; }
        public virtual int ShopItemId { get; set; }
        public virtual int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ShopItem ShopItem { get; set; }
    }
}
