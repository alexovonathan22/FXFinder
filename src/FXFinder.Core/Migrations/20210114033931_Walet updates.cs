using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Waletupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainCurrencySymbol",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "MainCurrencyTitle",
                table: "WalletAccts");

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "WalletAccts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrnencyTitle",
                table: "WalletAccts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandAmount",
                table: "WalletAccts",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrencyConverted",
                table: "WalletAccts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFundApproved",
                table: "WalletAccts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 14, 4, 39, 31, 322, DateTimeKind.Local).AddTicks(9173), new byte[] { 81, 116, 142, 140, 162, 175, 222, 158, 39, 236, 43, 97, 122, 201, 125, 62, 32, 110, 71, 242, 191, 215, 183, 76, 173, 16, 217, 97, 201, 77, 57, 232, 195, 19, 80, 223, 127, 174, 46, 107, 171, 144, 225, 92, 31, 52, 31, 254, 13, 132, 163, 241, 100, 164, 218, 1, 81, 211, 255, 10, 36, 92, 91, 252 }, new byte[] { 190, 188, 228, 15, 66, 195, 171, 35, 165, 240, 206, 223, 33, 14, 204, 3, 99, 99, 68, 211, 227, 80, 23, 134, 56, 145, 209, 232, 160, 0, 6, 226, 227, 150, 5, 68, 136, 182, 97, 26, 107, 78, 164, 90, 130, 113, 68, 73, 66, 13, 178, 122, 206, 115, 74, 199, 252, 192, 204, 184, 56, 212, 90, 170, 66, 247, 202, 28, 9, 194, 1, 217, 95, 238, 112, 83, 199, 181, 142, 3, 140, 212, 191, 204, 252, 94, 82, 253, 20, 25, 191, 135, 190, 229, 178, 225, 134, 13, 131, 81, 2, 66, 179, 79, 24, 149, 63, 195, 240, 246, 159, 73, 100, 51, 119, 115, 159, 35, 10, 182, 148, 97, 79, 96, 184, 131, 253, 223 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "CurrnencyTitle",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "GrandAmount",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "IsCurrencyConverted",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "IsFundApproved",
                table: "WalletAccts");

            migrationBuilder.AddColumn<string>(
                name: "MainCurrencySymbol",
                table: "WalletAccts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainCurrencyTitle",
                table: "WalletAccts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 12, 2, 23, 36, 862, DateTimeKind.Local).AddTicks(3713), new byte[] { 62, 175, 181, 71, 251, 228, 35, 39, 189, 30, 51, 98, 47, 113, 67, 247, 36, 104, 8, 177, 246, 32, 15, 98, 205, 196, 227, 106, 180, 16, 136, 16, 130, 183, 165, 12, 65, 132, 119, 132, 254, 230, 17, 83, 169, 73, 179, 98, 237, 114, 192, 119, 152, 79, 7, 227, 124, 225, 120, 106, 196, 186, 100, 224 }, new byte[] { 114, 5, 140, 190, 105, 121, 172, 225, 10, 206, 169, 240, 192, 186, 234, 217, 193, 238, 121, 99, 0, 133, 58, 104, 175, 47, 142, 65, 252, 54, 113, 158, 207, 98, 99, 100, 146, 165, 113, 238, 152, 218, 226, 65, 46, 115, 12, 175, 214, 124, 38, 78, 12, 89, 229, 83, 79, 244, 37, 37, 224, 206, 54, 54, 171, 180, 17, 72, 81, 102, 154, 250, 81, 186, 78, 55, 73, 206, 52, 3, 53, 5, 182, 98, 175, 199, 62, 168, 127, 216, 240, 209, 97, 68, 3, 17, 124, 35, 249, 85, 62, 215, 96, 102, 120, 168, 58, 15, 88, 107, 57, 51, 37, 158, 138, 15, 21, 170, 178, 57, 241, 114, 79, 128, 241, 0, 164, 41 } });
        }
    }
}
