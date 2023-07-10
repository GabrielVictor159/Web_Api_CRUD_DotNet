using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gcsb.ecommerce.infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "OrderId",
                principalSchema: "Ecommerce",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Ecommerce",
                table: "OrderProduct");
        }
    }
}
