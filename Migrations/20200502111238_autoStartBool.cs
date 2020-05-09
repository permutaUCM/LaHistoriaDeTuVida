using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class autoStartBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DefaultTransitionAutoStart",
                table: "PhotoTransition",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DefaultTransitionAutoStart",
                table: "PhotoTransition",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
