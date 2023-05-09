using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class tirandoCirculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidoProdutos_pedidos_idPedido",
                table: "pedidoProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidoProdutos_produtos_idProduto",
                table: "pedidoProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_clientes_Id",
                table: "pedidos");

            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "pedidoProdutos",
                newName: "IdProduto");

            migrationBuilder.RenameColumn(
                name: "idPedido",
                table: "pedidoProdutos",
                newName: "IdPedido");

            migrationBuilder.RenameIndex(
                name: "IX_pedidoProdutos_idProduto",
                table: "pedidoProdutos",
                newName: "IX_pedidoProdutos_IdProduto");

            migrationBuilder.AddColumn<Guid>(
                name: "clienteId",
                table: "pedidos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_produtos_Nome",
                table: "produtos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_clienteId",
                table: "pedidos",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_idCliente",
                table: "pedidos",
                column: "idCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoProdutos_pedidos_IdPedido",
                table: "pedidoProdutos",
                column: "IdPedido",
                principalTable: "pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoProdutos_produtos_IdProduto",
                table: "pedidoProdutos",
                column: "IdProduto",
                principalTable: "produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_clientes_clienteId",
                table: "pedidos",
                column: "clienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_clientes_idCliente",
                table: "pedidos",
                column: "idCliente",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidoProdutos_pedidos_IdPedido",
                table: "pedidoProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidoProdutos_produtos_IdProduto",
                table: "pedidoProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_clientes_clienteId",
                table: "pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_clientes_idCliente",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_produtos_Nome",
                table: "produtos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_clienteId",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_idCliente",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "pedidos");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "pedidoProdutos",
                newName: "idProduto");

            migrationBuilder.RenameColumn(
                name: "IdPedido",
                table: "pedidoProdutos",
                newName: "idPedido");

            migrationBuilder.RenameIndex(
                name: "IX_pedidoProdutos_IdProduto",
                table: "pedidoProdutos",
                newName: "IX_pedidoProdutos_idProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoProdutos_pedidos_idPedido",
                table: "pedidoProdutos",
                column: "idPedido",
                principalTable: "pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidoProdutos_produtos_idProduto",
                table: "pedidoProdutos",
                column: "idProduto",
                principalTable: "produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_clientes_Id",
                table: "pedidos",
                column: "Id",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
