using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class refatorando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pedidoProdutos",
                table: "pedidoProdutos");

            migrationBuilder.DropIndex(
                name: "IX_pedidoProdutos_idPedido",
                table: "pedidoProdutos");

            migrationBuilder.DropIndex(
                name: "IX_pedidoProdutos_idProduto",
                table: "pedidoProdutos");

            migrationBuilder.DropColumn(
                name: "Lista",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "pedidoProdutos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedidoProdutos",
                table: "pedidoProdutos",
                columns: new[] { "idPedido", "idProduto" });

            migrationBuilder.CreateIndex(
                name: "IX_pedidoProdutos_idProduto",
                table: "pedidoProdutos",
                column: "idProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pedidoProdutos",
                table: "pedidoProdutos");

            migrationBuilder.DropIndex(
                name: "IX_pedidoProdutos_idProduto",
                table: "pedidoProdutos");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Lista",
                table: "pedidos",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "pedidoProdutos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedidoProdutos",
                table: "pedidoProdutos",
                column: "Id");

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
    }
}
