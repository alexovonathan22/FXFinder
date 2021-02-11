using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Changewalletppt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainCurrency",
                table: "WalletAccts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 15, 7, 34, 45, 919, DateTimeKind.Local).AddTicks(9315), new byte[] { 177, 239, 22, 4, 247, 45, 158, 131, 206, 211, 37, 117, 81, 155, 136, 129, 142, 161, 114, 66, 100, 149, 124, 101, 232, 65, 124, 53, 164, 72, 248, 26, 251, 133, 223, 245, 31, 67, 83, 115, 150, 252, 174, 0, 142, 172, 67, 19, 141, 33, 180, 229, 54, 181, 76, 87, 100, 63, 63, 242, 130, 238, 119, 247 }, new byte[] { 76, 244, 236, 254, 234, 122, 74, 229, 228, 212, 34, 70, 220, 181, 181, 239, 5, 173, 143, 32, 35, 247, 16, 72, 157, 65, 240, 228, 208, 102, 69, 73, 99, 33, 250, 214, 67, 95, 157, 60, 110, 94, 128, 87, 29, 136, 171, 31, 2, 203, 247, 29, 146, 150, 182, 177, 0, 134, 174, 235, 4, 112, 70, 208, 221, 165, 78, 104, 97, 80, 167, 251, 234, 100, 62, 121, 56, 70, 137, 139, 168, 113, 110, 213, 21, 205, 115, 14, 219, 116, 222, 89, 114, 42, 1, 44, 168, 83, 185, 235, 64, 200, 206, 63, 94, 249, 209, 33, 255, 176, 224, 254, 24, 75, 129, 144, 122, 62, 174, 20, 77, 182, 242, 248, 30, 117, 179, 225 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainCurrency",
                table: "WalletAccts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 15, 1, 30, 13, 371, DateTimeKind.Local).AddTicks(282), new byte[] { 80, 63, 124, 91, 228, 126, 205, 100, 28, 24, 4, 240, 168, 150, 170, 40, 84, 146, 93, 250, 215, 197, 85, 160, 42, 67, 0, 55, 79, 93, 125, 113, 33, 76, 23, 120, 104, 200, 125, 251, 101, 210, 244, 9, 140, 114, 199, 114, 1, 87, 217, 251, 40, 189, 135, 117, 92, 182, 132, 213, 236, 218, 238, 144 }, new byte[] { 219, 208, 184, 27, 87, 151, 153, 113, 232, 191, 212, 145, 221, 222, 100, 75, 210, 109, 188, 62, 152, 175, 139, 50, 0, 80, 161, 135, 68, 37, 59, 188, 207, 208, 180, 107, 93, 182, 205, 206, 8, 58, 200, 195, 204, 213, 86, 99, 219, 61, 199, 144, 76, 123, 142, 97, 15, 225, 230, 112, 88, 32, 220, 6, 239, 215, 128, 181, 226, 123, 159, 31, 25, 6, 31, 17, 143, 18, 234, 203, 30, 136, 174, 102, 125, 49, 206, 70, 140, 174, 231, 109, 51, 49, 17, 141, 210, 104, 244, 108, 240, 129, 36, 162, 108, 26, 222, 92, 49, 108, 100, 181, 176, 201, 30, 63, 112, 5, 228, 158, 67, 50, 175, 1, 29, 195, 244, 181 } });
        }
    }
}
