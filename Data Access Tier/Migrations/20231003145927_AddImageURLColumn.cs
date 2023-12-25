using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Tier.Migrations
{
    public partial class AddImageURLColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Employees");
        }
    }
}
