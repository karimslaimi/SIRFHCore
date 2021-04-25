using Microsoft.EntityFrameworkCore.Migrations;

namespace SIRHCoreData.Migrations
{
    public partial class newone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creepar",
                table: "Incidents",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "CreeparId",
                table: "Incidents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CreeparId",
                table: "Incidents",
                column: "CreeparId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_AspNetUsers_CreeparId",
                table: "Incidents",
                column: "CreeparId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_AspNetUsers_CreeparId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_CreeparId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "CreeparId",
                table: "Incidents");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Incidents",
                newName: "Creepar");
        }
    }
}
