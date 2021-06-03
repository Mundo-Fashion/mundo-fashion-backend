using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoFashion.Infrastructure.Data.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TomadorId",
                table: "Solicitacoes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes",
                column: "TomadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TomadorId",
                table: "Solicitacoes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes",
                column: "TomadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
