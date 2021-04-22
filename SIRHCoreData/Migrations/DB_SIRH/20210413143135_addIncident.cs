using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIRHCoreData.Migrations.DB_SIRH
{
    public partial class addIncident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_DIM_DIRECTION",
                columns: table => new
                {
                    ID_DIRECTION = table.Column<int>(type: "int", nullable: false),
                    LIBELLE_DIRECTION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIRECTION", x => x.ID_DIRECTION);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_PAYS",
                columns: table => new
                {
                    ID_PAYS = table.Column<int>(type: "int", nullable: false),
                    NOM_PAYS = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TBL_DIM___B68ABC4D6B18DA05", x => x.ID_PAYS);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_SITUATION_FAMILIALE",
                columns: table => new
                {
                    ID_SITUATION_FAMILIALE = table.Column<int>(type: "int", nullable: false),
                    LIBELLE_SITUATION_FAMILIALE = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TBL_DIM___88406D8F40C882DD", x => x.ID_SITUATION_FAMILIALE);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_STRUCTURE",
                columns: table => new
                {
                    ID_STRUCTURE = table.Column<int>(type: "int", nullable: false),
                    LIBELLE_STRUCTURE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STRUCTURE", x => x.ID_STRUCTURE);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_DEPARTEMENT",
                columns: table => new
                {
                    ID_DEPARTEMENT = table.Column<int>(type: "int", nullable: false),
                    ID_DIRECTION = table.Column<int>(type: "int", nullable: false),
                    LIBELLE_DEPARTEMENT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DIM_DEPARTEMENT", x => x.ID_DEPARTEMENT);
                    table.ForeignKey(
                        name: "FK_TBL_DIM_DEPARTEMENT_TBL_DIM_DIRECTION",
                        column: x => x.ID_DIRECTION,
                        principalTable: "TBL_DIM_DIRECTION",
                        principalColumn: "ID_DIRECTION",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_FONCTION",
                columns: table => new
                {
                    ID_FONCTION = table.Column<int>(type: "int", nullable: false),
                    ID_DIRECTION = table.Column<int>(type: "int", nullable: false),
                    LIBELLE_FONCTION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FONCTION", x => x.ID_FONCTION);
                    table.ForeignKey(
                        name: "FK_fonction",
                        column: x => x.ID_DIRECTION,
                        principalTable: "TBL_DIM_DIRECTION",
                        principalColumn: "ID_DIRECTION",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DIM_VILLE",
                columns: table => new
                {
                    ID_VILLE = table.Column<int>(type: "int", nullable: false),
                    ID_PAYS = table.Column<int>(type: "int", nullable: false),
                    NOM_VILLE = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TBL_DIM___1FFE7135E409D1CC", x => x.ID_VILLE);
                    table.ForeignKey(
                        name: "FK_TBL_DIM_VILLE_TBL_DIM_PAYS",
                        column: x => x.ID_PAYS,
                        principalTable: "TBL_DIM_PAYS",
                        principalColumn: "ID_PAYS",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DW_SALARIE",
                columns: table => new
                {
                    MATRICULE = table.Column<int>(type: "int", nullable: false),
                    CIVILITE = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    NOM = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PRENOM = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ID_FONCTION = table.Column<int>(type: "int", nullable: false),
                    ID_STRUCTURE = table.Column<int>(type: "int", nullable: false),
                    TEL = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATRICULE", x => x.MATRICULE);
                    table.ForeignKey(
                        name: "FK_Fonction_salarie",
                        column: x => x.ID_FONCTION,
                        principalTable: "TBL_DIM_FONCTION",
                        principalColumn: "ID_FONCTION",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_structure_salarie",
                        column: x => x.ID_STRUCTURE,
                        principalTable: "TBL_DIM_STRUCTURE",
                        principalColumn: "ID_STRUCTURE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DW_ETAT_CIVIL",
                columns: table => new
                {
                    MATRICULE = table.Column<int>(type: "int", nullable: false),
                    NUM_SECU_SOCIALE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DATE_NAISSANCE = table.Column<DateTime>(type: "date", nullable: true),
                    ID_PAYS_NAISSANCE = table.Column<int>(type: "int", nullable: false),
                    ID_VILLE_NAISSANCE = table.Column<int>(type: "int", nullable: false),
                    NATIONALITE = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ID_SITUATION_FAMILIALE = table.Column<int>(type: "int", nullable: false),
                    ADRESSE = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    EMAIL_PERSONNEL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LINKEDIN = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DW_ETAT_CIVIL", x => x.MATRICULE);
                    table.ForeignKey(
                        name: "FK_TBL_DW_ETAT_CIVIL_TBL_DIM_SITUATION_FAMILIALE",
                        column: x => x.ID_SITUATION_FAMILIALE,
                        principalTable: "TBL_DIM_SITUATION_FAMILIALE",
                        principalColumn: "ID_SITUATION_FAMILIALE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_DW_ETAT_CIVIL_TBL_DIM_VILLE",
                        column: x => x.ID_VILLE_NAISSANCE,
                        principalTable: "TBL_DIM_VILLE",
                        principalColumn: "ID_VILLE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_DW_ETAT_CIVIL_TBL_DW_SALARIE",
                        column: x => x.MATRICULE,
                        principalTable: "TBL_DW_SALARIE",
                        principalColumn: "MATRICULE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_DEPARTEMENT_ID_DEPARTEMENT",
                table: "TBL_DIM_DEPARTEMENT",
                column: "ID_DEPARTEMENT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_DEPARTEMENT_ID_DIRECTION",
                table: "TBL_DIM_DEPARTEMENT",
                column: "ID_DIRECTION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_DIRECTION_ID_DIRECTION",
                table: "TBL_DIM_DIRECTION",
                column: "ID_DIRECTION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_FONCTION_ID_DIRECTION",
                table: "TBL_DIM_FONCTION",
                column: "ID_DIRECTION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_FONCTION_ID_FONCTION",
                table: "TBL_DIM_FONCTION",
                column: "ID_FONCTION",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_STRUCTURE_ID_STRUCTURE",
                table: "TBL_DIM_STRUCTURE",
                column: "ID_STRUCTURE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DIM_VILLE_ID_PAYS",
                table: "TBL_DIM_VILLE",
                column: "ID_PAYS");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_ETAT_CIVIL_ID_SITUATION_FAMILIALE",
                table: "TBL_DW_ETAT_CIVIL",
                column: "ID_SITUATION_FAMILIALE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_ETAT_CIVIL_ID_VILLE_NAISSANCE",
                table: "TBL_DW_ETAT_CIVIL",
                column: "ID_VILLE_NAISSANCE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_ETAT_CIVIL_MATRICULE",
                table: "TBL_DW_ETAT_CIVIL",
                column: "MATRICULE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_SALARIE_ID_FONCTION",
                table: "TBL_DW_SALARIE",
                column: "ID_FONCTION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_SALARIE_ID_STRUCTURE",
                table: "TBL_DW_SALARIE",
                column: "ID_STRUCTURE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_DW_SALARIE_MATRICULE",
                table: "TBL_DW_SALARIE",
                column: "MATRICULE",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_DIM_DEPARTEMENT");

            migrationBuilder.DropTable(
                name: "TBL_DW_ETAT_CIVIL");

            migrationBuilder.DropTable(
                name: "TBL_DIM_SITUATION_FAMILIALE");

            migrationBuilder.DropTable(
                name: "TBL_DIM_VILLE");

            migrationBuilder.DropTable(
                name: "TBL_DW_SALARIE");

            migrationBuilder.DropTable(
                name: "TBL_DIM_PAYS");

            migrationBuilder.DropTable(
                name: "TBL_DIM_FONCTION");

            migrationBuilder.DropTable(
                name: "TBL_DIM_STRUCTURE");

            migrationBuilder.DropTable(
                name: "TBL_DIM_DIRECTION");
        }
    }
}
