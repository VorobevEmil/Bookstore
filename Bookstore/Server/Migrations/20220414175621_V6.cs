using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Server.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookModelOrderModel",
                table: "BookModelOrderModel");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookModelOrderModel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookModelOrderModel",
                table: "BookModelOrderModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookModelOrderModel_BooksId",
                table: "BookModelOrderModel",
                column: "BooksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookModelOrderModel",
                table: "BookModelOrderModel");

            migrationBuilder.DropIndex(
                name: "IX_BookModelOrderModel_BooksId",
                table: "BookModelOrderModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookModelOrderModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookModelOrderModel",
                table: "BookModelOrderModel",
                columns: new[] { "BooksId", "OrdersId" });
        }
    }
}
