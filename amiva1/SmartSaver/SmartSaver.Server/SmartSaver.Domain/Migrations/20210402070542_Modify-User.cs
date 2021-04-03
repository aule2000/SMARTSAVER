using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SmartSaver.Domain.Migrations
{
    public partial class ModifyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "HashedPassword",
                table: "Users",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(32)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Base64UserImage",
                table: "Users",
                type: "varchar(MAX)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e33fa08-bc0f-438c-a21b-bcf4fc227661"),
                column: "HashedPassword",
                value: "hashed_password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64UserImage",
                table: "Users");

            migrationBuilder.AlterColumn<byte[]>(
                name: "HashedPassword",
                table: "Users",
                type: "binary(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "UserImage",
                table: "Users",
                type: "image",
                nullable: true);

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
                columns: new[] { "HashedPassword", "Username" },
                values: new object[] { new byte[] { 1, 2, 3 }, "test_user" });
        }
    }
}
