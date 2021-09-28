using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIssue.API.Infrastructure.Migrations
{
    public partial class FixesAndEmployeeUserJunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_EmployeeLogin",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeLogin",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeLogin",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeLogin",
                table: "Tasks",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeUser",
                columns: table => new
                {
                    UserLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUser", x => new { x.EmployeeLogin, x.UserLogin });
                    table.ForeignKey(
                        name: "FK_EmployeeUser_Employees_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Employees",
                        principalColumn: "EmployeeLogin",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeUser_Users_UserLogin",
                        column: x => x.UserLogin,
                        principalTable: "Users",
                        principalColumn: "UserLogin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployeeLogin",
                table: "Tasks",
                column: "EmployeeLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskOwner",
                table: "Tasks",
                column: "TaskOwner");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUser_EmployeeLogin",
                table: "EmployeeUser",
                column: "EmployeeLogin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUser_UserLogin",
                table: "EmployeeUser",
                column: "UserLogin",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_EmployeeLogin",
                table: "Tasks",
                column: "EmployeeLogin",
                principalTable: "Employees",
                principalColumn: "EmployeeLogin",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_TaskOwner",
                table: "Tasks",
                column: "TaskOwner",
                principalTable: "Employees",
                principalColumn: "EmployeeLogin",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_EmployeeLogin",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_TaskOwner",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "EmployeeUser");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EmployeeLogin",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskOwner",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EmployeeLogin",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeLogin",
                table: "Users",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeLogin",
                table: "Users",
                column: "EmployeeLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_EmployeeLogin",
                table: "Employees",
                column: "EmployeeLogin",
                principalTable: "Users",
                principalColumn: "UserLogin",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeLogin",
                table: "Users",
                column: "EmployeeLogin",
                principalTable: "Employees",
                principalColumn: "EmployeeLogin",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
