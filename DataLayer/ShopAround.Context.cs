﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopAroundEntities : DbContext
    {
        public ShopAroundEntities()
            : base("name=ShopAroundEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderShopItem> OrderShopItem { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ShopItem> ShopItem { get; set; }
        public DbSet<User> User { get; set; }
    }
}
