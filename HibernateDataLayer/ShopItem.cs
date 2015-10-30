using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
//http://nhibernate.info/doc/tutorials/first-nh-app/your-first-nhibernate-based-application.html

namespace HibernateDataLayer
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public byte[] Image { get; set; }
    }
}
