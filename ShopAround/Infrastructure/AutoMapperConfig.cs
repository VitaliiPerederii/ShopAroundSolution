using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ShopAround.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<HibernateDataLayer.ShopItem, ShopAround.Models.UiShopItem>();
            Mapper.CreateMap<ShopAround.Models.UiShopItem, HibernateDataLayer.ShopItem> ();

            Mapper.CreateMap<HibernateDataLayer.Category, ShopAround.Models.UiCategory>();
        }
    }
}