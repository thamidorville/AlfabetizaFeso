using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AlfabetizaFeso.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCursoEAtualizarModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Usuarios_EducadorId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Usuarios_AlunoId",
                table: "Inscricoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes");

            migrationBuilder.RenameColumn(
                name: "DataInicioUtc",
                table: "Aulas",
                newName: "DataInicio");

            migrationBuilder.RenameColumn(
                name: "DataFinalUtc",
                table: "Aulas",
                newName: "DataFinal");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "Inscricoes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Inscricoes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Inscricoes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Inscricoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInscricao",
                table: "Inscricoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Presenca",
                table: "Inscricoes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Inscricoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Aulas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Aulas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    CargaHoraria = table.Column<int>(type: "integer", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    EducadorId = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Usuarios_EducadorId",
                        column: x => x.EducadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_AlunoId",
                table: "Inscricoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_CursoId",
                table: "Inscricoes",
                column: "CursoId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Inscricao_CursoOuAula",
                table: "Inscricoes",
                sql: "(\"CursoId\" IS NOT NULL AND \"AulaId\" IS NULL) OR (\"CursoId\" IS NULL AND \"AulaId\" IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_CursoId",
                table: "Aulas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EducadorId",
                table: "Cursos",
                column: "EducadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Usuarios_EducadorId",
                table: "Aulas",
                column: "EducadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Cursos_CursoId",
                table: "Inscricoes",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Usuarios_AlunoId",
                table: "Inscricoes",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Usuarios_EducadorId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Cursos_CursoId",
                table: "Inscricoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Usuarios_AlunoId",
                table: "Inscricoes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes");

            migrationBuilder.DropIndex(
                name: "IX_Inscricoes_AlunoId",
                table: "Inscricoes");

            migrationBuilder.DropIndex(
                name: "IX_Inscricoes_CursoId",
                table: "Inscricoes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Inscricao_CursoOuAula",
                table: "Inscricoes");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_CursoId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "DataInscricao",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "Presenca",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Aulas");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Aulas",
                newName: "DataInicioUtc");

            migrationBuilder.RenameColumn(
                name: "DataFinal",
                table: "Aulas",
                newName: "DataFinalUtc");

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "Inscricoes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes",
                columns: new[] { "AlunoId", "AulaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Usuarios_EducadorId",
                table: "Aulas",
                column: "EducadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Usuarios_AlunoId",
                table: "Inscricoes",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
