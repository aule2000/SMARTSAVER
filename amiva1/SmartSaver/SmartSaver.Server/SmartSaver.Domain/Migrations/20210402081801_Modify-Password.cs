using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSaver.Domain.Migrations
{
    public partial class ModifyPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
