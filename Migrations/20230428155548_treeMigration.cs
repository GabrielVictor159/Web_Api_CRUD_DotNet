using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class treeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "clientes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "clientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "clientes");
        }
    }
}
