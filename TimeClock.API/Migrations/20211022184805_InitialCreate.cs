using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeClock.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimesClock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ExitTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    PointDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesClock", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimesClock");
        }
    }
}
