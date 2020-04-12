using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class UserTokenAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName1 = table.Column<string>(nullable: true),
                    LastName2 = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Dni = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RecovertyToken = table.Column<string>(nullable: true),
                    ExpirationTokenDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 25, nullable: true),
                    Caption = table.Column<string>(maxLength: 50, nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    RealDate = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Size = table.Column<decimal>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    FolderDbId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Folder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagDb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Extra1 = table.Column<string>(nullable: true),
                    Extra2 = table.Column<string>(nullable: true),
                    Extra3 = table.Column<string>(nullable: true),
                    PhotoDbId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagDb_Photo_PhotoDbId",
                        column: x => x.PhotoDbId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Photo_FolderDbId",
                table: "Photo",
                column: "FolderDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDb_PhotoDbId",
                table: "TagDb",
                column: "PhotoDbId");

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
                name: "FK_FileTags_Folder_FolderDbId",
                table: "FileTags",
                column: "FolderDbId",
                principalTable: "Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "TagDb");

            migrationBuilder.DropTable(
                name: "TagTypeMaster");

            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
