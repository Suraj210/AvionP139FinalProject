using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avion.Migrations
{
    public partial class UpdateTestimonialsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Testimonials",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Testimonials");
        }
    }
}
