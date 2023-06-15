using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SbRotina.Migrations
{
    public partial class Ultima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "Id", "DataTermino", "Descricao", "Nome", "Status", "UsuarioId" },
                values: new object[] { 1, "09/04/2024", "Teste Api", "Tarefa Teste", "Pendente", 1 });
        }
    }
}
