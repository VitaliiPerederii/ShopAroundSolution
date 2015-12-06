using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
//http://nhibernate.info/doc/tutorials/first-nh-app/your-first-nhibernate-based-application.html

namespace HibernateDataLayer
{
    public class ShopItem
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
        public virtual bool Available { get; set; }
        public virtual byte[] Image { get; set; }
    }
}
