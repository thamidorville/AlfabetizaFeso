using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlfabetizaFeso.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaAlunosInscricoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Educadores_Email",
                table: "Educadores",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Educadores_Email",
                table: "Educadores");
        }
    }
}
