using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Changemodelproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "CurrencyTitle",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "MainCurrency",
                table: "WalletAccts");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "WalletAccts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "WalletAccts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "WalletAccts",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "MainCurrencyTitle",
                table: "WalletAccts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedBy", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 12, 1, 27, 59, 498, DateTimeKind.Local).AddTicks(6601), null, null, new byte[] { 148, 43, 24, 217, 209, 88, 55, 156, 118, 152, 242, 3, 168, 51, 117, 216, 198, 203, 39, 62, 255, 108, 246, 208, 168, 163, 253, 205, 14, 71, 76, 117, 92, 39, 164, 167, 71, 72, 38, 216, 186, 141, 247, 23, 249, 248, 209, 132, 118, 102, 243, 62, 44, 80, 230, 36, 159, 250, 49, 13, 50, 236, 42, 87 }, new byte[] { 162, 130, 255, 30, 44, 249, 78, 101, 194, 89, 175, 90, 198, 228, 182, 67, 172, 170, 231, 18, 146, 107, 244, 97, 177, 222, 36, 45, 85, 124, 13, 247, 248, 96, 39, 156, 94, 221, 163, 119, 76, 64, 187, 121, 221, 112, 230, 15, 128, 127, 101, 142, 139, 106, 54, 213, 147, 156, 28, 81, 159, 84, 190, 175, 16, 48, 159, 38, 147, 246, 22, 221, 228, 48, 151, 199, 136, 221, 43, 17, 8, 246, 83, 78, 160, 216, 9, 145, 79, 129, 4, 202, 18, 96, 244, 182, 223, 96, 212, 5, 232, 76, 41, 61, 189, 49, 172, 139, 136, 7, 146, 197, 78, 110, 175, 81, 62, 242, 77, 147, 198, 149, 13, 174, 93, 84, 94, 138 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainCurrencyTitle",
                table: "WalletAccts");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "WalletAccts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "WalletAccts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "WalletAccts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "WalletAccts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyTitle",
                table: "WalletAccts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainCurrency",
                table: "WalletAccts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "ModifiedBy", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 11, 15, 31, 0, 28, DateTimeKind.Local).AddTicks(1052), 0, 0, new byte[] { 19, 50, 154, 148, 16, 77, 126, 251, 230, 39, 147, 46, 162, 66, 221, 160, 186, 54, 206, 148, 99, 194, 76, 214, 230, 228, 238, 75, 206, 151, 124, 169, 203, 52, 181, 237, 226, 159, 7, 236, 230, 91, 34, 22, 10, 81, 160, 29, 90, 200, 66, 118, 121, 133, 79, 61, 113, 162, 217, 90, 206, 4, 90, 193 }, new byte[] { 228, 81, 100, 208, 36, 203, 69, 139, 182, 235, 178, 171, 214, 234, 207, 134, 110, 71, 69, 16, 25, 59, 253, 90, 146, 37, 220, 180, 202, 226, 216, 231, 151, 145, 132, 118, 79, 215, 151, 55, 130, 1, 129, 199, 62, 35, 246, 179, 81, 172, 118, 36, 11, 88, 16, 80, 107, 232, 145, 54, 93, 18, 104, 175, 46, 91, 199, 133, 65, 143, 193, 62, 81, 237, 114, 128, 119, 240, 202, 40, 82, 211, 120, 128, 100, 54, 2, 46, 197, 118, 190, 143, 36, 208, 144, 233, 134, 98, 110, 32, 40, 2, 97, 131, 100, 219, 136, 136, 139, 24, 66, 118, 210, 182, 30, 18, 48, 155, 184, 253, 227, 238, 252, 102, 216, 217, 232, 216 } });
        }
    }
}
