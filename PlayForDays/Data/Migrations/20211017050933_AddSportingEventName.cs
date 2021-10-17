using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayForDays.Data.Migrations
{
    public partial class AddSportingEventName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SportingEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SportingEvents");
        }
    }
}
