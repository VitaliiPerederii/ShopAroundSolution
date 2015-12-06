using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace HibernateDataLayer
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    Configuration cfg = new Configuration();
                    cfg.Configure();
                    //cfg.AddAssembly(typeof(ShopItem).Assembly);
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
