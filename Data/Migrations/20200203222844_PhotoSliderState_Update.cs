using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey.Data.Migrations
{
    public partial class PhotoSliderState_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "sliderState",
                table: "Photos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sliderState",
                table: "Photos");
        }
    }
}
