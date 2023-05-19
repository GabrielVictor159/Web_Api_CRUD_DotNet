using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmacaoDePagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PagamentoConfirmado",
                table: "pedidos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "cupons",
                table: "pedidos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagamentoConfirmado",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "cupons",
                table: "pedidos");
        }
    }
}
