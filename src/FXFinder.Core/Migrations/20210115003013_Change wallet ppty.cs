using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Changewalletppty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFundApproved",
                table: "WalletAccts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFundOrWithdrawApproved",
                table: "WalletAccts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 15, 1, 30, 13, 371, DateTimeKind.Local).AddTicks(282), new byte[] { 80, 63, 124, 91, 228, 126, 205, 100, 28, 24, 4, 240, 168, 150, 170, 40, 84, 146, 93, 250, 215, 197, 85, 160, 42, 67, 0, 55, 79, 93, 125, 113, 33, 76, 23, 120, 104, 200, 125, 251, 101, 210, 244, 9, 140, 114, 199, 114, 1, 87, 217, 251, 40, 189, 135, 117, 92, 182, 132, 213, 236, 218, 238, 144 }, new byte[] { 219, 208, 184, 27, 87, 151, 153, 113, 232, 191, 212, 145, 221, 222, 100, 75, 210, 109, 188, 62, 152, 175, 139, 50, 0, 80, 161, 135, 68, 37, 59, 188, 207, 208, 180, 107, 93, 182, 205, 206, 8, 58, 200, 195, 204, 213, 86, 99, 219, 61, 199, 144, 76, 123, 142, 97, 15, 225, 230, 112, 88, 32, 220, 6, 239, 215, 128, 181, 226, 123, 159, 31, 25, 6, 31, 17, 143, 18, 234, 203, 30, 136, 174, 102, 125, 49, 206, 70, 140, 174, 231, 109, 51, 49, 17, 141, 210, 104, 244, 108, 240, 129, 36, 162, 108, 26, 222, 92, 49, 108, 100, 181, 176, 201, 30, 63, 112, 5, 228, 158, 67, 50, 175, 1, 29, 195, 244, 181 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFundOrWithdrawApproved",
                table: "WalletAccts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFundApproved",
                table: "WalletAccts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 1, 14, 23, 29, 35, 981, DateTimeKind.Local).AddTicks(3632), new byte[] { 254, 54, 79, 213, 193, 157, 140, 15, 167, 219, 27, 207, 51, 58, 45, 243, 82, 12, 231, 159, 3, 175, 200, 95, 25, 69, 231, 248, 138, 23, 230, 73, 199, 26, 128, 251, 130, 202, 150, 101, 216, 192, 201, 171, 67, 2, 177, 240, 139, 244, 138, 22, 53, 142, 170, 213, 244, 32, 187, 167, 35, 158, 215, 163 }, new byte[] { 181, 97, 87, 166, 125, 79, 145, 13, 184, 227, 8, 250, 245, 140, 181, 125, 14, 127, 5, 32, 94, 217, 47, 81, 107, 68, 166, 5, 191, 96, 89, 138, 102, 205, 237, 241, 144, 43, 170, 133, 61, 91, 83, 157, 164, 15, 107, 177, 188, 46, 72, 9, 198, 10, 153, 82, 73, 14, 155, 189, 92, 121, 96, 197, 71, 92, 191, 196, 46, 156, 139, 158, 26, 12, 169, 81, 107, 161, 174, 78, 95, 235, 12, 178, 207, 19, 74, 179, 158, 190, 90, 195, 153, 28, 55, 52, 5, 7, 30, 7, 110, 140, 185, 184, 29, 101, 159, 45, 183, 20, 13, 1, 116, 11, 214, 230, 5, 57, 12, 67, 128, 19, 38, 154, 218, 82, 195, 209 } });
        }
    }
}
