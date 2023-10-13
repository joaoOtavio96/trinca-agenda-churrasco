using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trinca.AgendaChurrasco.Data.Migrations
{
    public partial class Add_Coluna_SoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Churrascos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Churrascos");
        }
    }
}
