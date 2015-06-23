using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using ShopAround.Abstract;
using ShopAround.Concrete;

namespace ShopAround.Infrastructure
{
    
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType != null ? (IController)ninjectKernel.Get(controllerType) : null;
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductStorage>().To<DatabaseProductStorage>();
        }
    }
}