using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class admincontcorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "AdminContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AdminContact",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
