using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuniPortal.Migrations
{
    public partial class ModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentID = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyType = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Faculties_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Faculties_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Department_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Assignment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assignment_Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Assignment_Discription = table.Column<string>(type: "varchar", nullable: true),
                    Document = table.Column<string>(type: "varchar", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Assignment_Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Department_Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assignments_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Submission_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    Faculty_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Assignment_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Submission_Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_Assignment_Id",
                        column: x => x.Assignment_Id,
                        principalTable: "Assignments",
                        principalColumn: "Assignment_Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Submissions_Faculties_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "Faculties",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Department_Id",
                table: "Assignments",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Student_Id",
                table: "Assignments",
                column: "Student_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Department_Id",
                table: "Faculties",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Assignment_Id",
                table: "Submissions",
                column: "Assignment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Faculty_Id",
                table: "Submissions",
                column: "Faculty_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
