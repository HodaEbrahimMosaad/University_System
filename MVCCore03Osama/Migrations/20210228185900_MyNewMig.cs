using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCCore03Osama.Migrations
{
    public partial class MyNewMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_postOwnerId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_postOwnerId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "postOwnerId",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_posts_ApplicationUserId",
                table: "posts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_ApplicationUserId",
                table: "posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_ApplicationUserId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_ApplicationUserId",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "postOwnerId",
                table: "posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_postOwnerId",
                table: "posts",
                column: "postOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_postOwnerId",
                table: "posts",
                column: "postOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
