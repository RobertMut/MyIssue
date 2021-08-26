using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyIssue.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<decimal>(type: "decimal(6,0)", precision: 6, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ClientCountry = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    ClientNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClientStreet = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ClientStreetNo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ClientFlatNo = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    ClientDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<decimal>(type: "decimal(3,0)", precision: 3, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    TypeId = table.Column<decimal>(type: "decimal(4,0)", precision: 4, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(3,0)", precision: 3, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientEmployees",
                columns: table => new
                {
                    EmployeeId = table.Column<decimal>(type: "decimal(10,0)", precision: 10, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Client = table.Column<decimal>(type: "decimal(6,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEmployees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_ClientEmployees_Clients_Client",
                        column: x => x.Client,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<decimal>(type: "decimal(10,0)", precision: 10, scale: 0, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskClient = table.Column<decimal>(type: "decimal(6,0)", nullable: false),
                    TaskOwner = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TaskAssignment = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TaskType = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    TaskStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MailId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Clients_TaskClient",
                        column: x => x.TaskClient,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_TaskType",
                        column: x => x.TaskType,
                        principalTable: "TaskTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserType = table.Column<decimal>(type: "decimal(3,0)", nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserLogin);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserType",
                        column: x => x.UserType,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeLogin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmployeePosition = table.Column<decimal>(type: "decimal(3,0)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Employees_Users_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Users",
                        principalColumn: "UserLogin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "TypeId", "TypeName" },
                values: new object[,]
                {
                    { 1m, "Low priority" },
                    { 2m, "Normal" },
                    { 3m, "Urgent" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1m, "Locked" },
                    { 2m, "User" },
                    { 3m, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserLogin", "EmployeeLogin", "Password", "UserType" },
                values: new object[] { "Admin", null, "1234", 3m });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmployees_Client",
                table: "ClientEmployees",
                column: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeePosition",
                table: "Employees",
                column: "EmployeePosition");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskAssignment",
                table: "Tasks",
                column: "TaskAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskClient",
                table: "Tasks",
                column: "TaskClient");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskType",
                table: "Tasks",
                column: "TaskType");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeLogin",
                table: "Users",
                column: "EmployeeLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserType",
                table: "Users",
                column: "UserType");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_TaskAssignment",
                table: "Tasks",
                column: "TaskAssignment",
                principalTable: "Employees",
                principalColumn: "EmployeeLogin",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeLogin",
                table: "Users",
                column: "EmployeeLogin",
                principalTable: "Employees",
                principalColumn: "EmployeeLogin",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_EmployeePosition",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_EmployeeLogin",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ClientEmployees");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
