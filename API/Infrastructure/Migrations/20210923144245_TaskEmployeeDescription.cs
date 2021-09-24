using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIssue.API.Infrastructure.Migrations
{
    public partial class TaskEmployeeDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeDescription",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeDescription",
                table: "Tasks");
        }
    }
}
