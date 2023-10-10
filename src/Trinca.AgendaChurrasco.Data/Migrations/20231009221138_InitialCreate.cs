using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trinca.AgendaChurrasco.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Churrascos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorSugeridoSemBebida = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ValorSugeridoComBebida = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churrascos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Pago = table.Column<bool>(type: "bit", nullable: false),
                    ChurrascoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participantes_Churrascos_ChurrascoId",
                        column: x => x.ChurrascoId,
                        principalTable: "Churrascos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_ChurrascoId",
                table: "Participantes",
                column: "ChurrascoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Churrascos");
        }
    }
}
