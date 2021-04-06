using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedPhotoAndStudies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Doctors_DoctorId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_StudiesAndExperience_Doctors_DoctorId",
                table: "StudiesAndExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudiesAndExperience",
                table: "StudiesAndExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "StudiesAndExperience",
                newName: "StudiesAndExperiences");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_StudiesAndExperience_DoctorId",
                table: "StudiesAndExperiences",
                newName: "IX_StudiesAndExperiences_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_DoctorId",
                table: "Photos",
                newName: "IX_Photos_DoctorId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Photos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudiesAndExperiences",
                table: "StudiesAndExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Doctors_DoctorId",
                table: "Photos",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudiesAndExperiences_Doctors_DoctorId",
                table: "StudiesAndExperiences",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Doctors_DoctorId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_StudiesAndExperiences_Doctors_DoctorId",
                table: "StudiesAndExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudiesAndExperiences",
                table: "StudiesAndExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "StudiesAndExperiences",
                newName: "StudiesAndExperience");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_StudiesAndExperiences_DoctorId",
                table: "StudiesAndExperience",
                newName: "IX_StudiesAndExperience_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_DoctorId",
                table: "Photo",
                newName: "IX_Photo_DoctorId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Photo",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudiesAndExperience",
                table: "StudiesAndExperience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Doctors_DoctorId",
                table: "Photo",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudiesAndExperience_Doctors_DoctorId",
                table: "StudiesAndExperience",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
