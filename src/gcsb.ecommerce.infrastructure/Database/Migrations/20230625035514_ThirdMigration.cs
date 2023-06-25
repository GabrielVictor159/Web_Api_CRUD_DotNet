using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gcsb.ecommerce.infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Product_ProductId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Ecommerce",
                table: "OrderProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "Ecommerce",
                table: "OrderProduct",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "OrderId",
                principalSchema: "Ecommerce",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Product_ProductId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "ProductId",
                principalSchema: "Ecommerce",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
