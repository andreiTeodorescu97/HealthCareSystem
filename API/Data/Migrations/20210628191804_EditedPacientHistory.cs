using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class EditedPacientHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNP",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "PacientHistories");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "PacientHistories");

            migrationBuilder.CreateIndex(
                name: "IX_PacientHistories_PacientId",
                table: "PacientHistories",
                column: "PacientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PacientHistories_Pacients_PacientId",
                table: "PacientHistories",
                column: "PacientId",
                principalTable: "Pacients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacientHistories_Pacients_PacientId",
                table: "PacientHistories");

            migrationBuilder.DropIndex(
                name: "IX_PacientHistories_PacientId",
                table: "PacientHistories");

            migrationBuilder.AddColumn<string>(
                name: "CNP",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "PacientHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "PacientHistories",
                type: "text",
                nullable: true);
        }
    }
}
