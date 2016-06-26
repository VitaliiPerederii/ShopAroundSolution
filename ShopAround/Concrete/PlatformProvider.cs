using ShopAround.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopAround.Concrete
{
    public class PlatformProvider: IPlatformProvider
    {
        public bool IsLocalUrl(string url)
        {
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var helper = new UrlHelper(new RequestContext(httpContextBase, routeData));
            return helper.IsLocalUrl(url);
        }
    }
}