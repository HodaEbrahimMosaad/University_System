using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class lastisa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_ApplicationUserId",
                table: "comments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_ApplicationUserId",
                table: "comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_ApplicationUserId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_ApplicationUserId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "comments");
        }
    }
}
