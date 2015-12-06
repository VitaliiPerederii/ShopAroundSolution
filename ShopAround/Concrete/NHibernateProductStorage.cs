using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopAround.Abstract;
using ShopAround.Models;
using HibernateDataLayer;
using NHibernate;
using NHibernate.Linq;
using AutoMapper;

namespace ShopAround.Concrete
{
    public class NHibernateProductStorage: IProductStorage
    {
        public virtual IQueryable<UiShopItem> ShopItems 
        { 
            get
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    IEnumerable<ShopItem> shopItems = session.Query<ShopItem>().ToArray();
                    return Mapper.Map<IEnumerable<ShopItem>, IEnumerable<UiShopItem>>(shopItems).AsQueryable();
                }
            }
        }
        public virtual void AddShopItem(UiShopItem item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Save(Mapper.Map<UiShopItem, ShopItem>(item));
            }
        }
        public virtual void UpdateShopItem(UiShopItem item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Update(Mapper.Map<UiShopItem, ShopItem>(item));
            }
        }
        public virtual void DeleteShopItem(int Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ShopItem shopItems = session.Query<ShopItem>().FirstOrDefault(si => si.Id == Id);
                session.Delete(shopItems);
            }
        }
        public virtual IQueryable<UiCategory> Categories 
        {
            get
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    IEnumerable<Category> categories = session.Query<Category>().ToArray();
                    return Mapper.Map<IEnumerable<Category>, IEnumerable<UiCategory>>(categories).AsQueryable();
                }
            }
        }
        public virtual UiUser FindUser(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User user = session.Query<User>().Where(u => u.Email == name).FirstOrDefault();
                return Mapper.Map<User, UiUser>(user);
            }

        }
        public virtual void AddUser(UiUser user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(Mapper.Map<UiUser, User>(user));
                transaction.Commit();
            }
        }
        public virtual void AddRole(UiRole role)
        {

        }
        public virtual void CommitOrder(UiOrder order)
        {

        }
    }
}