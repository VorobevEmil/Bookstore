using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Server.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Books_BookModelId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_BookModelId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "BookModelId",
                table: "Feedbacks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookModelId",
                table: "Feedbacks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BookModelId",
                table: "Feedbacks",
                column: "BookModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Books_BookModelId",
                table: "Feedbacks",
                column: "BookModelId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
