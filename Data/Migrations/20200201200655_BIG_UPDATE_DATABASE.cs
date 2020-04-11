using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey.Data.Migrations
{
    public partial class BIG_UPDATE_DATABASE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "userAccounts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "photoUrl",
                table: "userAccounts",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "userAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Places",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Places",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "deleteState",
                table: "Places",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "deleteState",
                table: "Districts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Cities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Explain",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Cities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "deleteState",
                table: "Cities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    PlaceID = table.Column<int>(nullable: false),
                    userComment = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()"),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Explain = table.Column<string>(nullable: true),
                    photoUrl = table.Column<string>(nullable: true),
                    GalleryID = table.Column<int>(nullable: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryID = table.Column<int>(nullable: false),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "userAccounts");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "deleteState",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "deleteState",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Explain",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "deleteState",
                table: "Cities");

            migrationBuilder.AlterColumn<bool>(
                name: "photoUrl",
                table: "userAccounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "userAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    Explain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    deleteState = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.ID);
                });
        }
    }
}
