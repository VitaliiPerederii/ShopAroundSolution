using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibernateDataLayer
{
    public class Order
    {
        public Order()
        {
            this.OrderShopItem = new HashSet<OrderShopItem>();
        }

        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string ShipAddress { get; set; }
        public bool Proceed { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public virtual ICollection<OrderShopItem> OrderShopItem { get; set; }
    }
}
