using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSaver.Domain.Migrations
{
    public partial class CascadeUserDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
