using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoFashion.Infrastructure.Data.Migrations
{
    public partial class AlteradoNomeCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_UsuarioId",
                table: "Solicitacoes");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Solicitacoes",
                newName: "TomadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_UsuarioId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_TomadorId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Servicos",
                newName: "PrestadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_UsuarioId",
                table: "Servicos",
                newName: "IX_Servicos_PrestadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_PrestadorId",
                table: "Servicos",
                column: "PrestadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes",
                column: "TomadorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_PrestadorId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Usuarios_TomadorId",
                table: "Solicitacoes");

            migrationBuilder.RenameColumn(
                name: "TomadorId",
                table: "Solicitacoes",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_TomadorId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "PrestadorId",
                table: "Servicos",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_PrestadorId",
                table: "Servicos",
                newName: "IX_Servicos_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioId",
                table: "Servicos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Usuarios_UsuarioId",
                table: "Solicitacoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
