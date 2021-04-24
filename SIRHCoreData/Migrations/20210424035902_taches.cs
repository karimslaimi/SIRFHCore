using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIRHCoreData.Migrations
{
    public partial class taches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Projets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Taches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Projetid = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taches", x => x.id);
                    table.ForeignKey(
                        name: "FK_Taches_AspNetUsers_creatorId",
                        column: x => x.creatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taches_Projets_Projetid",
                        column: x => x.Projetid,
                        principalTable: "Projets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taches_creatorId",
                table: "Taches",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Taches_Projetid",
                table: "Taches",
                column: "Projetid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taches");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Projets");
        }
    }
}
