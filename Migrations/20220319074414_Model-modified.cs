using Microsoft.EntityFrameworkCore.Migrations;

namespace GuniPortal.Migrations
{
    public partial class Modelmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacultyType",
                table: "Faculties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyType",
                table: "Faculties",
                type: "int",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);
        }
    }
}
