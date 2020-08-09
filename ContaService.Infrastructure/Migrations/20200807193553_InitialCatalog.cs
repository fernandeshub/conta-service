using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaService.Infrastructure.Migrations
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
                    Saldo = table.Column<decimal>(nullable: false),
                    Numero = table.Column<string>(nullable: false)
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
                    ContaCorrenteId = table.Column<int>(nullable: true),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    TipoLancamento = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lancamento_ContaCorrente_ContaCorrenteId",
                        column: x => x.ContaCorrenteId,
                        principalTable: "ContaCorrente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_ContaCorrenteId",
                table: "Lancamento",
                column: "ContaCorrenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropTable(
                name: "ContaCorrente");
        }
    }
}
