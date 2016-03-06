using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using ShopAround.Abstract;
using ShopAround.Concrete;
using System.Web.Security;

namespace ShopAround.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            string dataProvider = ConfigurationManager.AppSettings["DataProvider"];
            kernel.Bind<IProductStorage>().To<DatabaseProductStorage>().When((a) => dataProvider == "EntityFramework");
            kernel.Bind<IProductStorage>().To<NHibernateProductStorage>().When((a) => dataProvider == "Hibernate");
            kernel.Inject(Membership.Provider);
            kernel.Inject(Roles.Provider);
        }
    }
}