using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SbRotina.Migrations
{
    public partial class CriacaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nome", "Senha", "SexoUsuario" },
                values: new object[] { 1, "Usuario@gmail.com", "UsuarioAdmin", "1233456", "M" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
