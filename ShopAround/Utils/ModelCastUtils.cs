using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Models;
using DataLayer;

namespace ShopAround.Utils
{
    public class ModelCastUtils
    {
        public static UiShopItem ToUiShopItem(ShopItem item)
        {
            UiShopItem uiItem = new UiShopItem();
            uiItem.Id = item.Id;
            uiItem.Name = item.Name;
            uiItem.CategoryId = item.CategoryId;
            uiItem.Description = item.Description;
            uiItem.Price = item.Price;
            uiItem.Image = item.Image;
            uiItem.Available = item.Available;
            uiItem.Category = ToUiShopCategory(item.Category);
            return uiItem;
        }

        public static ShopItem FromUiShopItem(UiShopItem uiItem)
        {
            ShopItem item = new ShopItem();
            item.Id = uiItem.Id;
            item.Name = uiItem.Name;
            item.CategoryId = uiItem.CategoryId;
            item.Description = uiItem.Description;
            item.Price = uiItem.Price;
            item.Image = uiItem.Image;
            item.Available = uiItem.Available;
            if (uiItem.Category != null)
                item.Category = FromUiShopCategory(uiItem.Category);
            return item;
        }

        public static UiCategory ToUiShopCategory(Category cat)
        {
            UiCategory uiCat = new UiCategory();
            uiCat.Id = cat.Id;
            uiCat.Name = cat.Name;
            return uiCat;
        }

        public static Category FromUiShopCategory(UiCategory uiCat)
        {
            Category cat = new Category();
            cat.Id = uiCat.Id;
            cat.Name = uiCat.Name;
            return cat;
        }

        public static UiUser ToUiUser(User user)
        {
            UiUser uiUser = new UiUser();
            uiUser.Id = user.Id;
            uiUser.Email = user.Email;
            uiUser.Name = user.Name;
            uiUser.RoleId = user.RoleId;
            uiUser.Password = user.Password;
            if (user.Role != null)
                uiUser.Role = ToUiRole(user.Role);

            return uiUser;
        }

        public static User FromUiUser(UiUser uiUser)
        {
            User user = new User();
            user.Id = uiUser.Id;
            user.Email = uiUser.Email;
            user.Name = uiUser.Name;
            user.RoleId = uiUser.RoleId;
            user.Password = uiUser.Password;
            if (uiUser.Role != null)
                user.Role = FromUiRole(uiUser.Role);

            return user;
        }

        public static UiRole ToUiRole(Role role)
        {
            UiRole uiRole = new UiRole();
            uiRole.Id = role.Id;
            uiRole.Name = role.Name;

            return uiRole;
        }

        public static Role FromUiRole(UiRole uiRole)
        {
            Role role = new Role();
            role.Id = uiRole.Id;
            role.Name = uiRole.Name;

            return role;
        }
    }
}