using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoFashion.Infrastructure.Data.Migrations
{
    public partial class RemovidoEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Empresas_EmpresaId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Empresas_EmpresaId",
                table: "Solicitacoes");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_EmpresaId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_EmpresaId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Servicos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Servicos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Solicitacoes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Servicos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Servicos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    ServicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EmpresaId",
                table: "Solicitacoes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_EmpresaId",
                table: "Servicos",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_UsuarioId",
                table: "Empresas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Empresas_EmpresaId",
                table: "Servicos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Empresas_EmpresaId",
                table: "Solicitacoes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
