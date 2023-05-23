using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class adicionadoIdPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoConfirmado",
                table: "pedidos");

            migrationBuilder.AddColumn<Guid>(
                name: "IdPagamento",
                table: "pedidos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPagamento",
                table: "pedidos");

            migrationBuilder.AddColumn<bool>(
                name: "PagamentoConfirmado",
                table: "pedidos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
