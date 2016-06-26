using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAround.Abstract
{
    public interface IPlatformProvider
    {
        bool IsLocalUrl(string url);
    }
}