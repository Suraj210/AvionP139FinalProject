using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avion.Migrations
{
    public partial class UpdateAboutsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heading",
                table: "Abouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
