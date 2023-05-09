using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class tentandoA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_clientes_clienteId",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_clienteId",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "pedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "clienteId",
                table: "pedidos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_clienteId",
                table: "pedidos",
                column: "clienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_clientes_clienteId",
                table: "pedidos",
                column: "clienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
