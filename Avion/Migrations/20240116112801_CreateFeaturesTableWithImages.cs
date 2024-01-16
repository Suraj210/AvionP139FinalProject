using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avion.Migrations
{
    public partial class CreateFeaturesTableWithImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Features");
        }
    }
}
