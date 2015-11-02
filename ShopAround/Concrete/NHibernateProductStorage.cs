using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Abstract;
using ShopAround.Models;

namespace ShopAround.Concrete
{
    public class NHibernateProductStorage: IProductStorage
    {
        public virtual IQueryable<UiShopItem> ShopItems 
        { 
            get
            {
                return null;
            }

        }
        public virtual void AddShopItem(UiShopItem item)
        {
            
        }
        public virtual void UpdateShopItem(UiShopItem item)
        {

        }
        public virtual void DeleteShopItem(int Id)
        {

        }
        public virtual IQueryable<UiCategory> Categories 
        {
            get
            {
                return null;
            }
        }
        public virtual UiUser FindUser(string name)
        {
            return new UiUser();
        }
        public virtual void AddUser(UiUser user)
        {

        }
        public virtual void AddRole(UiRole role)
        {

        }
        public virtual void CommitOrder(UiOrder order)
        {

        }
    }
}