using System;
using System.Collections.Generic;

namespace HibernateDataLayer
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string ShipAddress { get; set; }
        public virtual bool Proceed { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerEmail { get; set; }
        public virtual IList<OrderShopItem> OrderShopItem { get; set; }
    }
}
