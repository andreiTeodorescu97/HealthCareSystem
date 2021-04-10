using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddedTimeSpans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndTimeSpan",
                table: "WorkDays",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartTimeSpan",
                table: "WorkDays",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimeSpan",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "StartTimeSpan",
                table: "WorkDays");
        }
    }
}
