using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlfabetizaFeso.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoCargaHorariaAdicionandoLinkAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Cursos");

            migrationBuilder.AddColumn<string>(
                name: "LinkAula",
                table: "Aulas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkAula",
                table: "Aulas");

            migrationBuilder.AddColumn<int>(
                name: "CargaHoraria",
                table: "Cursos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
