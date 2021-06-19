using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedDoctorIdToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "PacientHistories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PacientHistories_DoctorId",
                table: "PacientHistories",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories");

            migrationBuilder.DropIndex(
                name: "IX_PacientHistories_DoctorId",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "PacientHistories");
        }
    }
}
