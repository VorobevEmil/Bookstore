using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Server.Migrations
{
    public partial class V9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Books_BookId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Feedbacks",
                newName: "BookOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_BookId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_BookOrderId");

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
                name: "FK_Feedbacks_BookModelOrderModel_BookOrderId",
                table: "Feedbacks",
                column: "BookOrderId",
                principalTable: "BookModelOrderModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Books_BookModelId",
                table: "Feedbacks",
                column: "BookModelId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_BookModelOrderModel_BookOrderId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Books_BookModelId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_BookModelId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "BookModelId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "BookOrderId",
                table: "Feedbacks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_BookOrderId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Books_BookId",
                table: "Feedbacks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
