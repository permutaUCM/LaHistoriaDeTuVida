using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class photoFolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Folder_FolderDbId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_FolderDbId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "FolderDbId",
                table: "Photo");

            migrationBuilder.CreateTable(
                name: "PhotoFolderMap",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false),
                    FolderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoFolderMap", x => new { x.PhotoId, x.FolderId });
                    table.ForeignKey(
                        name: "FK_PhotoFolderMap_Folder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoFolderMap_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoFolderMap_FolderId",
                table: "PhotoFolderMap",
                column: "FolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoFolderMap");

            migrationBuilder.AddColumn<int>(
                name: "FolderDbId",
                table: "Photo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_FolderDbId",
                table: "Photo",
                column: "FolderDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Folder_FolderDbId",
                table: "Photo",
                column: "FolderDbId",
                principalTable: "Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
