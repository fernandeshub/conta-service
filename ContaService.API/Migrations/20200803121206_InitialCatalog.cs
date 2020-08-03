using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaService.API.Migrations
{
    public partial class InitialCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lancamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ContaId = table.Column<int>(nullable: false),
                    TipoOperacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    ContaId = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.ContaId);
                    table.ForeignKey(
                        name: "FK_ContaCorrente_Lancamentos_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Lancamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lancamentos",
                columns: new[] { "Id", "ContaId", "Data", "TipoOperacao", "Valor" },
                values: new object[] { 1, 1, new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), 1, 1000m });

            migrationBuilder.InsertData(
                table: "Lancamentos",
                columns: new[] { "Id", "ContaId", "Data", "TipoOperacao", "Valor" },
                values: new object[] { 2, 2, new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), 1, 1000m });

            migrationBuilder.InsertData(
                table: "Lancamentos",
                columns: new[] { "Id", "ContaId", "Data", "TipoOperacao", "Valor" },
                values: new object[] { 3, 3, new DateTime(2020, 8, 3, 12, 12, 6, 567, DateTimeKind.Utc), 1, 1000m });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "ContaId", "Numero", "Saldo" },
                values: new object[] { 1, "12345-6", 1000m });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "ContaId", "Numero", "Saldo" },
                values: new object[] { 2, "54321-0", 1000m });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "ContaId", "Numero", "Saldo" },
                values: new object[] { 3, "02468-9", 1000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaCorrente");

            migrationBuilder.DropTable(
                name: "Lancamentos");
        }
    }
}
