using AutoMapper;

namespace ShopAround.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<HibernateDataLayer.ShopItem, Models.UiShopItem>();
            Mapper.CreateMap<Models.UiShopItem, HibernateDataLayer.ShopItem> ();
            Mapper.CreateMap<HibernateDataLayer.Category, Models.UiCategory>();

            Mapper.CreateMap<HibernateDataLayer.User, Models.UiUser>();
            Mapper.CreateMap<Models.UiUser, HibernateDataLayer.User>();

            Mapper.CreateMap<HibernateDataLayer.Role, Models.UiRole>();
        }
    }
}