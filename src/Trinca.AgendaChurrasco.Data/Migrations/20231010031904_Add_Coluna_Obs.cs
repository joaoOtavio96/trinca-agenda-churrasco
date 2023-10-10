using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trinca.AgendaChurrasco.Data.Migrations
{
    public partial class Add_Coluna_Obs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Participantes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Churrascos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Churrascos");
        }
    }
}
