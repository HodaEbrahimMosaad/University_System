using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class responserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponserID",
                table: "AdminContact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponserID",
                table: "AdminContact");
        }
    }
}
