using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gcsb.ecommerce.infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FirtMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ecommerce");

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Ecommerce",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Ecommerce",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Ecommerce",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClient = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalOrder = table.Column<decimal>(type: "numeric", nullable: false),
                    Cupons = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdPayment = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Client_IdClient",
                        column: x => x.IdClient,
                        principalSchema: "Ecommerce",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                schema: "Ecommerce",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    TotalOrderLine = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.IdOrder, x.IdProduct });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_IdOrder",
                        column: x => x.IdOrder,
                        principalSchema: "Ecommerce",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalSchema: "Ecommerce",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Ecommerce",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_IdClient",
                schema: "Ecommerce",
                table: "Order",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_IdProduct",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                schema: "Ecommerce",
                table: "OrderProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Ecommerce");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "Ecommerce");
        }
    }
}
