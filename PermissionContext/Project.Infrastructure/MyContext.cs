using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjectCore.Domain.Model;
using ProjectCore.Domain.Model.DomainModel;
using ProjectCore.Infrastructure.Interfaces;

namespace ProjectCore.Infrastructure
{
   public class MyContext:DbContext,IMyContext
    {
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        {           
            // Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartDetails> ShoppingCartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(e => e.CustomerAddr);

            //modelBuilder.Entity<UserRoleInfo>().HasOne(e => e.UserInfo).WithMany(e => e.UserRoleList)
            //    .HasForeignKey(p => p.UserId);
            //modelBuilder.Entity<UserRoleInfo>().HasOne(e => e.RoleInfo).WithMany(e => e.UserRoleList)
            //    .HasForeignKey(p => p.RoleId);

            //modelBuilder.Entity<RoleMenuInfo>().HasOne(e => e.RoleInfo).WithMany(e => e.RoleMenuList)
            //    .HasForeignKey(p => p.RoleId);
            //modelBuilder.Entity<RoleMenuInfo>().HasOne(e => e.MenuInfo).WithMany(e => e.RoleMenuList)
            //    .HasForeignKey(p => p.MenuId);
        }
    }
    
}
