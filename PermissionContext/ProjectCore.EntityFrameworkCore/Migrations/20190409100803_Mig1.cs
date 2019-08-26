using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectCore.EntityFrameworkCore.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAge = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    CustomerAddress_Province = table.Column<string>(maxLength: 25, nullable: true),
                    CustomerAddress_City = table.Column<string>(maxLength: 25, nullable: true),
                    CustomerAddress_County = table.Column<string>(maxLength: 25, nullable: true),
                    CustomerAddress_AddressDetails = table.Column<string>(maxLength: 200, nullable: true),
                    CustomerAddress_CustomerId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStorageInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStorageInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitorLogInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MonitorLogId = table.Column<Guid>(nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    RequestParameters = table.Column<string>(nullable: true),
                    ExecuteStartTime = table.Column<DateTime>(nullable: false),
                    ExecuteEndTime = table.Column<DateTime>(nullable: false),
                    ExecutionTime = table.Column<long>(nullable: false),
                    ErrorMsg = table.Column<string>(nullable: true),
                    LogType = table.Column<int>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    AddressIp = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorLogInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    OrderCreateDate = table.Column<string>(nullable: true),
                    OrderTotlePrice = table.Column<decimal>(nullable: false),
                    CustomrId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<int>(nullable: false),
                    ProductStock = table.Column<int>(nullable: false),
                    ProductColor = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    RoleType = table.Column<string>(maxLength: 50, nullable: true),
                    RoleDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    RoleShot = table.Column<int>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CartTotalPrice = table.Column<double>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserGroupName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 50, nullable: false),
                    UserPhone = table.Column<string>(maxLength: 50, nullable: true),
                    Address_Province = table.Column<string>(maxLength: 25, nullable: true),
                    Address_City = table.Column<string>(maxLength: 25, nullable: true),
                    Address_County = table.Column<string>(maxLength: 25, nullable: true),
                    Address_AddressDetails = table.Column<string>(maxLength: 200, nullable: true),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    CreateUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    OrderNumBer = table.Column<int>(nullable: false),
                    OrderDetailPrice = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailInfo_OrderInfo_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartDetailInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ShopProductName = table.Column<string>(nullable: true),
                    ShopProductColor = table.Column<string>(nullable: true),
                    ShopProductPrice = table.Column<double>(nullable: false),
                    ShopProductNumBer = table.Column<int>(nullable: false),
                    CartDetailPrice = table.Column<double>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ShoppingCartId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartDetailInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartDetailInfo_ShoppingCartInfo_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCartInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupUnRoleInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserGroupId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupUnRoleInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupUnRoleInfo_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupUnRoleInfo_UserGroupInfo_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroupInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUnGroupInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnGroupInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUnGroupInfo_UserGroupInfo_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroupInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnGroupInfo_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUnRoleInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnRoleInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUnRoleInfo_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnRoleInfo_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailInfo_OrderId",
                table: "OrderDetailInfo",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartDetailInfo_ShoppingCartId",
                table: "ShoppingCartDetailInfo",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupUnRoleInfo_RoleId",
                table: "UserGroupUnRoleInfo",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupUnRoleInfo_UserGroupId",
                table: "UserGroupUnRoleInfo",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_IsDisable_IsDeleted_UserName",
                table: "UserInfo",
                columns: new[] { "IsDisable", "IsDeleted", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_UserUnGroupInfo_UserGroupId",
                table: "UserUnGroupInfo",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnGroupInfo_UserId",
                table: "UserUnGroupInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnRoleInfo_RoleId",
                table: "UserUnRoleInfo",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnRoleInfo_UserId",
                table: "UserUnRoleInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "EventStorageInfo");

            migrationBuilder.DropTable(
                name: "MonitorLogInfo");

            migrationBuilder.DropTable(
                name: "OrderDetailInfo");

            migrationBuilder.DropTable(
                name: "ProductInfo");

            migrationBuilder.DropTable(
                name: "ShoppingCartDetailInfo");

            migrationBuilder.DropTable(
                name: "UserGroupUnRoleInfo");

            migrationBuilder.DropTable(
                name: "UserUnGroupInfo");

            migrationBuilder.DropTable(
                name: "UserUnRoleInfo");

            migrationBuilder.DropTable(
                name: "OrderInfo");

            migrationBuilder.DropTable(
                name: "ShoppingCartInfo");

            migrationBuilder.DropTable(
                name: "UserGroupInfo");

            migrationBuilder.DropTable(
                name: "RoleInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
