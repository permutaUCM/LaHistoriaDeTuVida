using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class tagMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderDbId",
                table: "Photo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Extra",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    extras = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extra", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultPhotoId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folder_Photo_DefaultPhotoId",
                        column: x => x.DefaultPhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Folder_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagTypeMaster",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Extra1Name = table.Column<string>(nullable: true),
                    Extra2Name = table.Column<string>(nullable: true),
                    Extra3Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTypeMaster", x => x.Name);
                    table.ForeignKey(
                        name: "FK_TagTypeMaster_Extra_Extra1Name",
                        column: x => x.Extra1Name,
                        principalTable: "Extra",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagTypeMaster_Extra_Extra2Name",
                        column: x => x.Extra2Name,
                        principalTable: "Extra",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagTypeMaster_Extra_Extra3Name",
                        column: x => x.Extra3Name,
                        principalTable: "Extra",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FolderDbId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileTags_Folder_FolderDbId",
                        column: x => x.FolderDbId,
                        principalTable: "Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_FolderDbId",
                table: "Photo",
                column: "FolderDbId");

            migrationBuilder.CreateIndex(
                name: "IX_FileTags_FolderDbId",
                table: "FileTags",
                column: "FolderDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_DefaultPhotoId",
                table: "Folder",
                column: "DefaultPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_UserId",
                table: "Folder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypeMaster_Extra1Name",
                table: "TagTypeMaster",
                column: "Extra1Name");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypeMaster_Extra2Name",
                table: "TagTypeMaster",
                column: "Extra2Name");

            migrationBuilder.CreateIndex(
                name: "IX_TagTypeMaster_Extra3Name",
                table: "TagTypeMaster",
                column: "Extra3Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Folder_FolderDbId",
                table: "Photo",
                column: "FolderDbId",
                principalTable: "Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Folder_FolderDbId",
                table: "Photo");

            migrationBuilder.DropTable(
                name: "FileTags");

            migrationBuilder.DropTable(
                name: "TagTypeMaster");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropIndex(
                name: "IX_Photo_FolderDbId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "FolderDbId",
                table: "Photo");
        }
    }
}
