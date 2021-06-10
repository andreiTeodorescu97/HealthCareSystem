using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    public partial class AddedPacientsHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacientHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacientId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    IdentityNumber = table.Column<string>(type: "text", nullable: true),
                    Series = table.Column<string>(type: "text", nullable: true),
                    CNP = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalNumberOfVisits = table.Column<int>(type: "integer", nullable: false),
                    LastVisitDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientHistories");
        }
    }
}
