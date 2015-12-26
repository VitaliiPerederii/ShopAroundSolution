using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ShopAround.Infrastructure;
using System.IO;

namespace ShopAround
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string root = Server.MapPath("~/");
            string parent = Path.GetDirectoryName(root);
            string grandParent = Path.GetDirectoryName(parent) + "\\Db";

            AppDomain.CurrentDomain.SetData("DataDirectory", grandParent);
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            AutoMapperConfig.Configure();
        }
    }
}