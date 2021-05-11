using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    public partial class AddedRecipeAndPrescriptionV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ConsultationId = table.Column<int>(type: "integer", nullable: false),
                    PacientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DosageType = table.Column<string>(type: "text", nullable: true),
                    DosageNumberPerDay = table.Column<int>(type: "integer", nullable: false),
                    Frequency = table.Column<int>(type: "integer", nullable: false),
                    FoodRelation = table.Column<string>(type: "text", nullable: true),
                    NumberOfDays = table.Column<int>(type: "integer", nullable: false),
                    Route = table.Column<string>(type: "text", nullable: true),
                    Instructions = table.Column<string>(type: "text", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_RecipeId",
                table: "Prescriptions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ConsultationId",
                table: "Recipes",
                column: "ConsultationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
