using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSaver.Domain.Migrations
{
    public partial class AddEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BalanceType",
                table: "Transactions",
                type: "int",
                unicode: false,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldUnicode: false,
                oldMaxLength: 16);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BalanceType",
                table: "Transactions",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldMaxLength: 16);
        }
    }
}
