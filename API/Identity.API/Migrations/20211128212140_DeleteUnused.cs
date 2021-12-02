using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIssue.Identity.API.Migrations
{
    public partial class DeleteUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeUser");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<decimal>(type: "decimal(3,0)", precision: 3, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmployeePosition = table.Column<decimal>(type: "decimal(3,0)", nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeLogin);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_EmployeePosition",
                        column: x => x.EmployeePosition,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeUser",
                columns: table => new
                {
                    EmployeeLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                name: "IX_Employees_EmployeePosition",
                table: "Employees",
                column: "EmployeePosition");

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
        }
    }
}
