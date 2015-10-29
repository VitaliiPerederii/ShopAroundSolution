using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Abstract;
using ShopAround.Models;
using DataLayer;

namespace ShopAround.Concrete
{
    public class DatabaseProductStorage: IProductStorage
    {
        private ShopAroundEntities context = new ShopAroundEntities();
        public IQueryable<UiShopItem> ShopItems
        {
            get
            {
                List<UiShopItem> list = new List<UiShopItem>();
                foreach (ShopItem item in context.ShopItem)
                    list.Add(ToUiShopItem(item));
                
                return list.AsQueryable() ;
            }
        }
        public void AddShopItem(UiShopItem uiItem)
        {
            ShopItem item = FromUiShopItem(uiItem);
            context.ShopItem.Add(item);
            context.SaveChanges();
        }
        public void UpdateShopItem(UiShopItem uiItem)
        {
            ShopItem item = FromUiShopItem(uiItem);
            context.Entry(context.ShopItem.Where(i => i.Id == uiItem.Id).FirstOrDefault()).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
        public void DeleteShopItem(int Id)
        {
            context.ShopItem.Remove(context.ShopItem.Where(i => i.Id == Id).FirstOrDefault());
            context.SaveChanges();
        }
        public IQueryable<UiCategory> Categories
        {
            get
            {
                List<UiCategory> list = new List<UiCategory>();
                foreach (Category cat in context.Category)
                    list.Add(ToUiShopCategory(cat));

                return list.AsQueryable();
            }
        }

        public UiUser FindUser(string name)
        {
            UiUser uiUser = ToUiUser(context.User.Where(u => u.Email == name).FirstOrDefault());
            return uiUser;
        }

        public void AddUser(UiUser user)
        {
            context.User.Add(FromUiUser(user));
            context.SaveChanges();
        }

        public void AddRole(UiRole role)
        {
            context.Role.Add(FromUiRole(role));
            context.SaveChanges();
        }

        public void CommitOrder(UiOrder uiOrder)
        {
            Order order = FromUiOrder(uiOrder);
            context.Order.Add(order);
            context.SaveChanges();
        }


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

        public static UiOrder ToUiOrder(Order order)
        {
            UiOrder uiOrder = new UiOrder();
            uiOrder.Id = order.Id;
            uiOrder.ShipAddress = order.ShipAddress;
            uiOrder.Date = order.Date;
            uiOrder.Proceed = order.Proceed;
            uiOrder.CustomerName = order.CustomerName;
            uiOrder.CustomerEmail = order.CustomerEmail;
            uiOrder.Cart = null;

            return uiOrder;
        }

        public static Order FromUiOrder(UiOrder uiOrder)
        {
            Order order = new Order();
            order.Id = uiOrder.Id;
            order.ShipAddress = uiOrder.ShipAddress;
            order.Date = uiOrder.Date;
            order.Proceed = uiOrder.Proceed;
            order.CustomerName = uiOrder.CustomerName;
            order.CustomerEmail = uiOrder.CustomerEmail;
            if (uiOrder.Cart != null)
            {
                foreach (CartItem item in uiOrder.Cart.Items)
                {
                    order.OrderShopItem.Add(new OrderShopItem()
                    {
                        Quantity = item.Quantity,
                        ShopItemId = item.ShopItem.Id,
                        Order = order
                    });
                }
            }

            return order;
        }
    }
}