using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedDoctorIdToHistoryV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "PacientHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "PacientHistories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PacientHistories_Doctors_DoctorId",
                table: "PacientHistories",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
