using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class folderTransData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoStart",
                table: "Folder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ShowTime",
                table: "Folder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Transition",
                table: "Folder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoStart",
                table: "Folder");

            migrationBuilder.DropColumn(
                name: "ShowTime",
                table: "Folder");

            migrationBuilder.DropColumn(
                name: "Transition",
                table: "Folder");
        }
    }
}
