using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectCore.Infrastructure.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddress_CustomerAddrId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerAddrId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerAddrId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotlePrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderDetailPrice",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddr_AddrCity",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddr_AddrCounty",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddr_AddrProvince",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddr_Address",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ProductColor = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<int>(nullable: false),
                    ProductStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTotlePrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CustomerAddr_AddrCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerAddr_AddrCounty",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerAddr_AddrProvince",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerAddr_Address",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "OrderPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailPrice",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "CustomerAddrId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddrCity = table.Column<string>(nullable: true),
                    AddrCounty = table.Column<string>(nullable: true),
                    AddrProvince = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAddrId",
                table: "Customers",
                column: "CustomerAddrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddress_CustomerAddrId",
                table: "Customers",
                column: "CustomerAddrId",
                principalTable: "CustomerAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
