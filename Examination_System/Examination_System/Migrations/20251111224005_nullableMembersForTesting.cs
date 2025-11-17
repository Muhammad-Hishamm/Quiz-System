using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class nullableMembersForTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_StudentExams_StudentExamStudentId_StudentExamExamId",
                table: "Results");

            migrationBuilder.AlterColumn<int>(
                name: "StudentExamStudentId",
                table: "Results",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StudentExamExamId",
                table: "Results",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_StudentExams_StudentExamStudentId_StudentExamExamId",
                table: "Results",
                columns: new[] { "StudentExamStudentId", "StudentExamExamId" },
                principalTable: "StudentExams",
                principalColumns: new[] { "StudentId", "ExamId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_StudentExams_StudentExamStudentId_StudentExamExamId",
                table: "Results");

            migrationBuilder.AlterColumn<int>(
                name: "StudentExamStudentId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentExamExamId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_StudentExams_StudentExamStudentId_StudentExamExamId",
                table: "Results",
                columns: new[] { "StudentExamStudentId", "StudentExamExamId" },
                principalTable: "StudentExams",
                principalColumns: new[] { "StudentId", "ExamId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
