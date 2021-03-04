using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class responsee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "AdminContact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "AdminContact");
        }
    }
}
