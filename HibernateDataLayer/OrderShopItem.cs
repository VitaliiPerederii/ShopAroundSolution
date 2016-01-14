using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibernateDataLayer
{
    public class OrderShopItem
    {
        public virtual CompositeId Id { get; set; }
        public virtual int Quantity { get; set; }
    }

    public class CompositeId
    {
        public virtual int OrderId { get; set; }
        public virtual int ShopItemId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            CompositeId id;
            id = (CompositeId)obj;
            if (id == null)
                return false;
            if (OrderId == id.OrderId && ShopItemId == id.ShopItemId)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (OrderId + "|" + ShopItemId).GetHashCode();
        }
    }
}
