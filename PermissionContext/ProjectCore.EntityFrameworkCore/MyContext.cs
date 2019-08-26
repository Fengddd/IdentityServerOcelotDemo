using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ProjectCore.Common;
using ProjectCore.Common.DomainInterfaces;
using ProjectCore.Common.Event;
using ProjectCore.Common.Log;
using ProjectCore.Domain.Model.Entity;
using ProjectCore.EntityFrameworkCore.Mapping;

namespace ProjectCore.EntityFrameworkCore
{
    /// <summary>
    /// EF Core 上下文
    /// </summary>
    public class MyContext:DbContext,IMyContext
    {
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        {
            // Database.EnsureCreated();

        }
        /// <summary>
        /// 日志
        /// </summary>
        public DbSet<MonitorLog> MonitorLogInfo { get; set; }

        /// <summary>
        /// 事件储存
        /// </summary>
        public DbSet<BaseEvent> EventStorageInfo { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }   
        /// <summary>
        /// 用户组
        /// </summary>
        public DbSet<UserGroupInfo> UserGroupInfo { get; set; }   
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<RoleInfo> RoleInfo { get; set; }
        /// <summary>
        /// 用户和用户组关联
        /// </summary>
        public DbSet<UserUnGroup> UserUnGroupInfo { get; set; }
        /// <summary>
        /// 用户和角色关联
        /// </summary>
        public DbSet<UserUnRole> UserUnRoleInfo { get; set; }
        /// <summary>
        /// 用户组和角色关联
        /// </summary>
        public DbSet<UserGroupUnRole> UserGroupUnRoleInfo { get; set; }

        #region 购物车
        public DbSet<Customer> CustomerInfo { get; set; }
        public DbSet<Order> OrderInfo { get; set; }
        public DbSet<OrderDetails> OrderDetailInfo { get; set; }
        public DbSet<Product> ProductInfo { get; set; }
        public DbSet<ShoppingCart> ShoppingCartInfo { get; set; }
        public DbSet<ShoppingCartDetails> ShoppingCartDetailInfo { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserInfo>().OwnsOne(e => e.Address);

            //modelBuilder.Entity<UserRoleInfo>().HasOne(e => e.UserInfo).WithMany(e => e.UserRoleList)
            //    .HasForeignKey(p => p.UserId);
            //modelBuilder.Entity<UserRoleInfo>().HasOne(e => e.RoleInfo).WithMany(e => e.UserRoleList)
            //    .HasForeignKey(p => p.RoleId);

            //modelBuilder.Entity<RoleMenuInfo>().HasOne(e => e.RoleInfo).WithMany(e => e.RoleMenuList)
            //    .HasForeignKey(p => p.RoleId);
            //modelBuilder.Entity<RoleMenuInfo>().HasOne(e => e.MenuInfo).WithMany(e => e.RoleMenuList)
            //    .HasForeignKey(p => p.MenuId);
            modelBuilder.ApplyConfiguration<UserInfo>(new UserMap());         
            //设置软删除
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {           
                //// 1. Add the IsDeleted property
                //entityType.GetOrAddProperty("IsDeleted", typeof(bool));
                // 2. Create the query filter
                var parameter = Expression.Parameter(entityType.ClrType);
                //查询类上面是否有SoftDelete（值对象）的特性
                var ownedModelType = parameter.Type;
                var ownedAttribute = Attribute.GetCustomAttribute(ownedModelType, typeof(SoftDeleteAttribute));
                if (ownedAttribute == null)
                {
                    // 3. EF.Property<bool>(post, "IsDeleted")
                    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                    var isDeletedProperty =
                        Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                    // 4. EF.Property<bool>(post, "IsDeleted") == false
                    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty,
                        Expression.Constant(false));

                    // 5. post => EF.Property<bool>(post, "IsDeleted") == false
                    var lambda = Expression.Lambda(compareExpression, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }             
            }
            base.OnModelCreating(modelBuilder);
        }       
    }
    
    

}
