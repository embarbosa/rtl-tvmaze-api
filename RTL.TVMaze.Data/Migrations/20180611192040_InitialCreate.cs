using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RTL.TVMaze.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TVShow",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShow", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    TVShowId = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cast_TVShow_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cast_TVShowId",
                table: "Cast",
                column: "TVShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cast");

            migrationBuilder.DropTable(
                name: "TVShow");
        }
    }
}
