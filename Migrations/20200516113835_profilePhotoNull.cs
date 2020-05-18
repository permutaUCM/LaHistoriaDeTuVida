using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class profilePhotoNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Photo_ProfilePhotoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfilePhotoId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePhotoId",
                table: "User",
                column: "ProfilePhotoId",
                unique: true,
                filter: "[ProfilePhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Photo_ProfilePhotoId",
                table: "User",
                column: "ProfilePhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Photo_ProfilePhotoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProfilePhotoId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePhotoId",
                table: "User",
                column: "ProfilePhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Photo_ProfilePhotoId",
                table: "User",
                column: "ProfilePhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
