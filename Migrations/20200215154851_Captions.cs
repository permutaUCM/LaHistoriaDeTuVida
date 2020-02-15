using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class Captions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagpropDb_TagDb_TagDbId",
                table: "TagpropDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagpropDb",
                table: "TagpropDb");

            migrationBuilder.RenameTable(
                name: "TagpropDb",
                newName: "TagPropDb");

            migrationBuilder.RenameIndex(
                name: "IX_TagpropDb_TagDbId",
                table: "TagPropDb",
                newName: "IX_TagPropDb_TagDbId");

            migrationBuilder.AddColumn<string>(
                name: "caption",
                table: "Photo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagPropDb",
                table: "TagPropDb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagPropDb_TagDb_TagDbId",
                table: "TagPropDb",
                column: "TagDbId",
                principalTable: "TagDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagPropDb_TagDb_TagDbId",
                table: "TagPropDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagPropDb",
                table: "TagPropDb");

            migrationBuilder.DropColumn(
                name: "caption",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "TagPropDb",
                newName: "TagpropDb");

            migrationBuilder.RenameIndex(
                name: "IX_TagPropDb_TagDbId",
                table: "TagpropDb",
                newName: "IX_TagpropDb_TagDbId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagpropDb",
                table: "TagpropDb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagpropDb_TagDb_TagDbId",
                table: "TagpropDb",
                column: "TagDbId",
                principalTable: "TagDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
