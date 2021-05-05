using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedVaccinesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Vaccines",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomForUser",
                table: "Vaccines",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomForUser",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Vaccines",
                newName: "Type");
        }
    }
}
