using Microsoft.EntityFrameworkCore.Migrations;

namespace SIRHCoreData.Migrations
{
    public partial class Collab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "collaborations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonneId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Projetid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborations", x => x.id);
                    table.ForeignKey(
                        name: "FK_collaborations_AspNetUsers_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_collaborations_Projets_Projetid",
                        column: x => x.Projetid,
                        principalTable: "Projets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_collaborations_PersonneId",
                table: "collaborations",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_collaborations_Projetid",
                table: "collaborations",
                column: "Projetid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collaborations");
        }
    }
}
