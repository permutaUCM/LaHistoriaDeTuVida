using Microsoft.EntityFrameworkCore.Migrations;

namespace LHDTV.Migrations
{
    public partial class photoTransitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionName = table.Column<string>(nullable: true),
                    DefaultTransitionTime = table.Column<int>(nullable: false),
                    DefaultTransitionAutoStart = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoTransition", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoTransition");
        }
    }
}
