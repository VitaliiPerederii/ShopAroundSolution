using System.Collections.Generic;
using System.Linq;
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
                ShopItem coreShopItem = Mapper.Map<UiShopItem, ShopItem>(item);
                coreShopItem.Category = session.Load<Category>(coreShopItem.CategoryId);
                session.Save(coreShopItem);
            }
        }
        public virtual void UpdateShopItem(UiShopItem item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                ShopItem shopItem = session.Get<ShopItem>(item.Id);
                Mapper.Map(item, shopItem);

                shopItem.Category = session.Load<Category>(shopItem.CategoryId);
                session.SaveOrUpdate(shopItem);
                transaction.Commit();
            }
        }
        public virtual void DeleteShopItem(int Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                ShopItem shopItems = session.Query<ShopItem>().FirstOrDefault(si => si.Id == Id);
                session.Delete(shopItems);
                transaction.Commit();
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
            { 
                User coreUser = Mapper.Map<UiUser, User>(user);
                coreUser.Role = session.Load<Role>(coreUser.RoleId);
                session.Save(coreUser);
            }
        }
        public virtual void AddRole(UiRole role)
        {

        }
        public virtual void CommitOrder(UiOrder order)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                Order coreOrder = Mapper.Map<UiOrder, Order>(order);
                session.Save(coreOrder);
                if (order.Cart != null)
                {
                    coreOrder.OrderShopItem = new List<OrderShopItem>();
                    foreach (CartItem item in order.Cart.Items)
                    {
                        OrderShopItem orsh = new OrderShopItem()
                        {
                            Id = new CompositeId()
                            {
                                ShopItemId = item.ShopItem.Id,
                                OrderId = coreOrder.Id
                            },
                            Quantity = item.Quantity,
                        };
                        coreOrder.OrderShopItem.Add(orsh);
                        session.Save(orsh);
                    }
                }
                transaction.Commit();
            }
        }
    }
}