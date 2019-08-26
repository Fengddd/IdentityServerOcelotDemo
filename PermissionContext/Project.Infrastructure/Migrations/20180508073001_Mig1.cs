using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectCore.Infrastructure.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppingCartId",
                table: "ShoppingCartDetails",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<double>(
                name: "CartDetailPrice",
                table: "ShoppingCartDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails");

            migrationBuilder.DropColumn(
                name: "CartDetailPrice",
                table: "ShoppingCartDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppingCartId",
                table: "ShoppingCartDetails",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartDetails_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCartDetails",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
