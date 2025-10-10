using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlfabetizaFeso.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToEducador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Educadores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Educadores");
        }
    }
}
