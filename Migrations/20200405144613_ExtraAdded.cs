using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class ExtraAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagPropDb");

            migrationBuilder.AddColumn<string>(
                name: "Extra1",
                table: "TagDb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra2",
                table: "TagDb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra3",
                table: "TagDb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extra1",
                table: "TagDb");

            migrationBuilder.DropColumn(
                name: "Extra2",
                table: "TagDb");

            migrationBuilder.DropColumn(
                name: "Extra3",
                table: "TagDb");

            migrationBuilder.CreateTable(
                name: "TagPropDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagDbId = table.Column<int>(type: "int", nullable: true),
                    propKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    propVal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPropDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagPropDb_TagDb_TagDbId",
                        column: x => x.TagDbId,
                        principalTable: "TagDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagPropDb_TagDbId",
                table: "TagPropDb",
                column: "TagDbId");
        }
    }
}
