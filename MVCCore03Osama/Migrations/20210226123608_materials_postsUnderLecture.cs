using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class materials_postsUnderLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materials_courses_CourseID",
                table: "materials");

            migrationBuilder.DropForeignKey(
                name: "FK_materials_lectures_LectureID",
                table: "materials");

            migrationBuilder.DropIndex(
                name: "IX_materials_CourseID",
                table: "materials");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "materials");

            migrationBuilder.AlterColumn<int>(
                name: "LectureID",
                table: "materials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_materials_lectures_LectureID",
                table: "materials",
                column: "LectureID",
                principalTable: "lectures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_materials_lectures_LectureID",
                table: "materials");

            migrationBuilder.AlterColumn<int>(
                name: "LectureID",
                table: "materials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_materials_CourseID",
                table: "materials",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_materials_courses_CourseID",
                table: "materials",
                column: "CourseID",
                principalTable: "courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_materials_lectures_LectureID",
                table: "materials",
                column: "LectureID",
                principalTable: "lectures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
