using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class twoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Lista = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pedidoProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    ValorTotalLinha = table.Column<decimal>(type: "numeric", nullable: false),
                    idPedido = table.Column<Guid>(type: "uuid", nullable: false),
                    idProduto = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidoProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pedidoProdutos_pedidos_idPedido",
                        column: x => x.idPedido,
                        principalTable: "pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedidoProdutos_produtos_idProduto",
                        column: x => x.idProduto,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedidoProdutos_idPedido",
                table: "pedidoProdutos",
                column: "idPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedidoProdutos_idProduto",
                table: "pedidoProdutos",
                column: "idProduto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedidoProdutos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "produtos");
        }
    }
}
