using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaService.API.Migrations
{
    public partial class InitialCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lancamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    SaldoAtual = table.Column<decimal>(nullable: false),
                    ContaId = table.Column<int>(nullable: false),
                    TipoOperacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccessKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "Id", "Numero", "Saldo" },
                values: new object[] { 1, "12345-6", 1000m });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "Id", "Numero", "Saldo" },
                values: new object[] { 2, "54321-0", 1000m });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "Id", "Numero", "Saldo" },
                values: new object[] { 3, "02468-9", 1000m });

            migrationBuilder.InsertData(
                table: "Lancamento",
                columns: new[] { "Id", "ContaId", "Data", "SaldoAtual", "TipoOperacao", "Valor" },
                values: new object[] { 1, 1, new DateTime(2020, 8, 5, 19, 2, 54, 569, DateTimeKind.Utc), 1000m, 1, 1000m });

            migrationBuilder.InsertData(
                table: "Lancamento",
                columns: new[] { "Id", "ContaId", "Data", "SaldoAtual", "TipoOperacao", "Valor" },
                values: new object[] { 2, 2, new DateTime(2020, 8, 5, 19, 2, 54, 569, DateTimeKind.Utc), 1000m, 1, 1000m });

            migrationBuilder.InsertData(
                table: "Lancamento",
                columns: new[] { "Id", "ContaId", "Data", "SaldoAtual", "TipoOperacao", "Valor" },
                values: new object[] { 3, 3, new DateTime(2020, 8, 5, 19, 2, 54, 569, DateTimeKind.Utc), 1000m, 1, 1000m });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AccessKey" },
                values: new object[] { 1, "6a15395e10db3e76b4a953ffb439d32da" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AccessKey" },
                values: new object[] { 2, "a4ae2f7f58dc1e0973fb79c9677a09161" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaCorrente");

            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
