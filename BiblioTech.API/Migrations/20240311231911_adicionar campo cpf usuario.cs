using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTech.API.Migrations
{
    /// <inheritdoc />
    public partial class adicionarcampocpfusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Users");
        }
    }
}
