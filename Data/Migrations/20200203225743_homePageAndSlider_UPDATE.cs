using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey.Data.Migrations
{
    public partial class homePageAndSlider_UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "homepageState",
                table: "Types",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "homepageState",
                table: "Cities",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "homepageState",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "homepageState",
                table: "Cities");
        }
    }
}
