using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SmartSaver.Domain.Migrations
{
    public partial class Add_Username_Password : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gmail",
                table: "Users",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "HashedPassword",
                table: "Users",
                type: "binary(32)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "char(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e33fa08-bc0f-438c-a21b-bcf4fc227661"),
                columns: new[] { "HashedPassword", "Role", "Username" },
                values: new object[] { new byte[] { 1, 2, 3 }, "admin", "test_user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Gmail");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }
    }
}
