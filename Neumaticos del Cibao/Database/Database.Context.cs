﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Neumaticos_del_Cibao.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class databaseEntities : DbContext
    {
        public databaseEntities()
            : base("name=databaseEntities")
        {
            this.Configuration.ProxyCreationEnabled = true;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ShoppingBill> ShoppingBills { get; set; }
        public virtual DbSet<ShoppingBillsArticle> ShoppingBillsArticles { get; set; }
        public virtual DbSet<CreditShoppingBill> CreditShoppingBills { get; set; }
        public virtual DbSet<CreditShoppingBillsRegister> CreditShoppingBillsRegisters { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<CreditSalesBill> CreditSalesBills { get; set; }
        public virtual DbSet<CreditSalesBillsRegister> CreditSalesBillsRegisters { get; set; }
        public virtual DbSet<SalesBill> SalesBills { get; set; }
        public virtual DbSet<SalesBillArticle> SalesBillArticles { get; set; }
    }
}
