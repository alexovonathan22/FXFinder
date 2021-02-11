using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Accountdigits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcctDigits",
                table: "WalletAccts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 12, 2, 23, 36, 862, DateTimeKind.Local).AddTicks(3713), new byte[] { 62, 175, 181, 71, 251, 228, 35, 39, 189, 30, 51, 98, 47, 113, 67, 247, 36, 104, 8, 177, 246, 32, 15, 98, 205, 196, 227, 106, 180, 16, 136, 16, 130, 183, 165, 12, 65, 132, 119, 132, 254, 230, 17, 83, 169, 73, 179, 98, 237, 114, 192, 119, 152, 79, 7, 227, 124, 225, 120, 106, 196, 186, 100, 224 }, new byte[] { 114, 5, 140, 190, 105, 121, 172, 225, 10, 206, 169, 240, 192, 186, 234, 217, 193, 238, 121, 99, 0, 133, 58, 104, 175, 47, 142, 65, 252, 54, 113, 158, 207, 98, 99, 100, 146, 165, 113, 238, 152, 218, 226, 65, 46, 115, 12, 175, 214, 124, 38, 78, 12, 89, 229, 83, 79, 244, 37, 37, 224, 206, 54, 54, 171, 180, 17, 72, 81, 102, 154, 250, 81, 186, 78, 55, 73, 206, 52, 3, 53, 5, 182, 98, 175, 199, 62, 168, 127, 216, 240, 209, 97, 68, 3, 17, 124, 35, 249, 85, 62, 215, 96, 102, 120, 168, 58, 15, 88, 107, 57, 51, 37, 158, 138, 15, 21, 170, 178, 57, 241, 114, 79, 128, 241, 0, 164, 41 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcctDigits",
                table: "WalletAccts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 12, 1, 27, 59, 498, DateTimeKind.Local).AddTicks(6601), new byte[] { 148, 43, 24, 217, 209, 88, 55, 156, 118, 152, 242, 3, 168, 51, 117, 216, 198, 203, 39, 62, 255, 108, 246, 208, 168, 163, 253, 205, 14, 71, 76, 117, 92, 39, 164, 167, 71, 72, 38, 216, 186, 141, 247, 23, 249, 248, 209, 132, 118, 102, 243, 62, 44, 80, 230, 36, 159, 250, 49, 13, 50, 236, 42, 87 }, new byte[] { 162, 130, 255, 30, 44, 249, 78, 101, 194, 89, 175, 90, 198, 228, 182, 67, 172, 170, 231, 18, 146, 107, 244, 97, 177, 222, 36, 45, 85, 124, 13, 247, 248, 96, 39, 156, 94, 221, 163, 119, 76, 64, 187, 121, 221, 112, 230, 15, 128, 127, 101, 142, 139, 106, 54, 213, 147, 156, 28, 81, 159, 84, 190, 175, 16, 48, 159, 38, 147, 246, 22, 221, 228, 48, 151, 199, 136, 221, 43, 17, 8, 246, 83, 78, 160, 216, 9, 145, 79, 129, 4, 202, 18, 96, 244, 182, 223, 96, 212, 5, 232, 76, 41, 61, 189, 49, 172, 139, 136, 7, 146, 197, 78, 110, 175, 81, 62, 242, 77, 147, 198, 149, 13, 174, 93, 84, 94, 138 } });
        }
    }
}
