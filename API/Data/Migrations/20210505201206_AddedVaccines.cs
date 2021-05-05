using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    public partial class AddedVaccines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecommendedAge = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccineXPacients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacientId = table.Column<int>(type: "integer", nullable: false),
                    VaccineId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineXPacients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineXPacients_Pacients_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineXPacients_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineXPacients_PacientId",
                table: "VaccineXPacients",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineXPacients_VaccineId",
                table: "VaccineXPacients",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineXPacients");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
