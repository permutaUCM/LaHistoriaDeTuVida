using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class newDB : Migration
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
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 25, nullable: true),
                    Caption = table.Column<string>(maxLength: 50, nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    RealDate = table.Column<DateTime>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Size = table.Column<decimal>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionName = table.Column<string>(nullable: true),
                    TransitionUserName = table.Column<string>(nullable: true),
                    DefaultTransitionTime = table.Column<int>(nullable: false),
                    DefaultTransitionAutoStart = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoTransition", x => x.Id);
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
                    Role = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RecovertyToken = table.Column<string>(nullable: true),
                    ExpirationTokenDate = table.Column<DateTime>(nullable: true),
                    ProfilePhotoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Photo_ProfilePhotoId",
                        column: x => x.ProfilePhotoId,
                        principalTable: "Photo",
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
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Transition = table.Column<string>(nullable: true),
                    ShowTime = table.Column<int>(nullable: false),
                    AutoStart = table.Column<bool>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_PhotoFolderMap_FolderId",
                table: "PhotoFolderMap",
                column: "FolderId");

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

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfilePhotoId",
                table: "User",
                column: "ProfilePhotoId",
                unique: true,
                filter: "[ProfilePhotoId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileTags");

            migrationBuilder.DropTable(
                name: "PhotoFolderMap");

            migrationBuilder.DropTable(
                name: "PhotoTransition");

            migrationBuilder.DropTable(
                name: "TagDb");

            migrationBuilder.DropTable(
                name: "TagTypeMaster");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Photo");
        }
    }
}
