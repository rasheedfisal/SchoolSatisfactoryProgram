using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSatisfactory.UI.Migrations
{
    public partial class pagenameInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageName",
                table: "AspNetNavigationMenu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageName",
                table: "AspNetNavigationMenu");
        }
    }
}
