﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectCore.EntityFrameworkCore;

namespace ProjectCore.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectCore.Common.Event.BaseEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.HasKey("Id");

                    b.ToTable("EventStorageInfo");
                });

            modelBuilder.Entity("ProjectCore.Common.Log.MonitorLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName");

                    b.Property<string>("AddressIp");

                    b.Property<string>("ControllerName");

                    b.Property<string>("ErrorMsg");

                    b.Property<DateTime>("ExecuteEndTime");

                    b.Property<DateTime>("ExecuteStartTime");

                    b.Property<long>("ExecutionTime");

                    b.Property<int>("LogLevel");

                    b.Property<int>("LogType");

                    b.Property<Guid>("MonitorLogId");

                    b.Property<string>("RequestParameters");

                    b.Property<Guid>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MonitorLogInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("CustomerAge");

                    b.Property<string>("CustomerName");

                    b.Property<string>("CustomerPhone");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("CustomerInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<Guid>("CustomrId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("OrderCreateDate");

                    b.Property<decimal>("OrderTotlePrice");

                    b.HasKey("Id");

                    b.ToTable("OrderInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<decimal>("OrderDetailPrice");

                    b.Property<Guid>("OrderId");

                    b.Property<int>("OrderNumBer");

                    b.Property<Guid>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetailInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProductColor");

                    b.Property<string>("ProductName");

                    b.Property<int>("ProductPrice");

                    b.Property<int>("ProductStock");

                    b.HasKey("Id");

                    b.ToTable("ProductInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.RoleInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatDateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("CreateUserName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("RoleDescription")
                        .HasMaxLength(1000);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RoleShot");

                    b.Property<string>("RoleType")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("RoleInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CartTotalPrice");

                    b.Property<string>("Code");

                    b.Property<Guid>("CustomerId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCartInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.ShoppingCartDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CartDetailPrice");

                    b.Property<string>("Code");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("ShopProductColor");

                    b.Property<string>("ShopProductName");

                    b.Property<int>("ShopProductNumBer");

                    b.Property<double>("ShopProductPrice");

                    b.Property<Guid?>("ShoppingCartId");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartDetailInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserGroupInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatDateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("CreateUserName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("UserGroupName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserGroupInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserGroupUnRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("UserGroupId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("UserGroupUnRoleInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatDateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("CreateUserName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDisable");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserPhone")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IsDisable", "IsDeleted", "UserName");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserUnGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UserGroupId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserUnGroupInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserUnRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserUnRoleInfo");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.Customer", b =>
                {
                    b.OwnsOne("ProjectCore.Domain.Model.ValueObject.Address", "CustomerAddress", b1 =>
                        {
                            b1.Property<Guid>("CustomerId1");

                            b1.Property<string>("AddressDetails")
                                .HasMaxLength(200);

                            b1.Property<string>("City")
                                .HasMaxLength(25);

                            b1.Property<string>("County")
                                .HasMaxLength(25);

                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Province")
                                .HasMaxLength(25);

                            b1.ToTable("CustomerInfo");

                            b1.HasOne("ProjectCore.Domain.Model.Entity.Customer")
                                .WithOne("CustomerAddress")
                                .HasForeignKey("ProjectCore.Domain.Model.ValueObject.Address", "CustomerId1")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.OrderDetails", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.Entity.Order", "OrderInfo")
                        .WithMany("OrderDetailList")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.ShoppingCartDetails", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.Entity.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartDetailList")
                        .HasForeignKey("ShoppingCartId");
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserGroupUnRole", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.Entity.RoleInfo", "RoleInfo")
                        .WithMany("UserGroupUnRoleList")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectCore.Domain.Model.Entity.UserGroupInfo", "UserGroupInfo")
                        .WithMany("UserGroupUnRoleList")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserInfo", b =>
                {
                    b.OwnsOne("ProjectCore.Domain.Model.ValueObject.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserInfoId");

                            b1.Property<string>("AddressDetails")
                                .HasMaxLength(200);

                            b1.Property<string>("City")
                                .HasMaxLength(25);

                            b1.Property<string>("County")
                                .HasMaxLength(25);

                            b1.Property<string>("Province")
                                .HasMaxLength(25);

                            b1.ToTable("UserInfo");

                            b1.HasOne("ProjectCore.Domain.Model.Entity.UserInfo")
                                .WithOne("Address")
                                .HasForeignKey("ProjectCore.Domain.Model.ValueObject.Address", "UserInfoId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserUnGroup", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.Entity.UserGroupInfo", "UserGroupInfo")
                        .WithMany("UserUnGroupList")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectCore.Domain.Model.Entity.UserInfo", "UserInfo")
                        .WithMany("UserUnGroupList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectCore.Domain.Model.Entity.UserUnRole", b =>
                {
                    b.HasOne("ProjectCore.Domain.Model.Entity.RoleInfo", "RoleInfo")
                        .WithMany("UserUnRoleList")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectCore.Domain.Model.Entity.UserInfo", "UserInfo")
                        .WithMany("UserUnRoleList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
