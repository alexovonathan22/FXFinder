using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Seedadminfortest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 10, null, null, new DateTime(2021, 1, 14, 21, 51, 14, 158, DateTimeKind.Local).AddTicks(9284), null, null, null, "avo.nathan@gmail.com", null, null, new byte[] { 64, 86, 70, 42, 172, 209, 78, 24, 128, 94, 100, 203, 112, 122, 188, 56, 58, 153, 94, 43, 157, 114, 199, 53, 177, 215, 232, 89, 1, 165, 22, 2, 13, 52, 84, 119, 97, 184, 157, 252, 177, 34, 11, 236, 127, 147, 59, 177, 255, 40, 10, 126, 33, 13, 103, 133, 47, 172, 39, 48, 32, 38, 88, 101 }, new byte[] { 246, 61, 105, 232, 88, 1, 46, 153, 101, 139, 91, 137, 238, 194, 136, 202, 22, 139, 244, 127, 126, 230, 10, 140, 229, 191, 200, 108, 144, 182, 36, 164, 110, 35, 4, 213, 121, 248, 162, 109, 8, 28, 2, 62, 155, 216, 229, 233, 211, 81, 226, 5, 217, 37, 130, 220, 242, 236, 41, 133, 230, 207, 23, 88, 193, 195, 72, 46, 245, 144, 121, 216, 9, 251, 62, 230, 138, 180, 155, 151, 182, 125, 248, 76, 82, 168, 125, 237, 174, 112, 130, 2, 176, 222, 57, 244, 201, 122, 84, 223, 98, 229, 22, 4, 116, 207, 230, 17, 79, 51, 59, 172, 16, 172, 6, 12, 172, 211, 249, 188, 55, 211, 95, 48, 94, 157, 208, 167 }, null, "Administrator", "adminovo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 1, null, null, new DateTime(2021, 1, 14, 21, 30, 30, 732, DateTimeKind.Local).AddTicks(2342), null, null, null, "avo.nathan@gmail.com", null, null, new byte[] { 10, 199, 247, 93, 83, 13, 154, 142, 160, 52, 175, 248, 159, 235, 38, 182, 179, 166, 98, 91, 97, 165, 60, 171, 80, 6, 7, 175, 175, 35, 155, 234, 51, 18, 28, 107, 89, 140, 175, 159, 59, 19, 76, 90, 33, 78, 179, 97, 109, 146, 199, 159, 86, 23, 68, 2, 227, 153, 168, 5, 76, 246, 87, 52 }, new byte[] { 251, 248, 200, 78, 105, 8, 174, 73, 90, 205, 232, 141, 249, 238, 93, 225, 155, 224, 195, 241, 230, 157, 29, 158, 143, 217, 16, 83, 2, 108, 89, 97, 200, 51, 157, 2, 108, 85, 134, 108, 155, 252, 201, 26, 160, 184, 71, 60, 237, 20, 173, 8, 169, 117, 1, 38, 100, 127, 238, 104, 224, 242, 49, 166, 223, 56, 129, 165, 190, 222, 100, 7, 140, 2, 25, 159, 254, 94, 99, 15, 207, 150, 86, 195, 199, 218, 225, 164, 16, 102, 86, 35, 53, 25, 115, 148, 67, 170, 107, 92, 137, 235, 3, 6, 132, 107, 185, 154, 64, 255, 106, 249, 128, 26, 60, 75, 125, 46, 4, 183, 204, 87, 124, 235, 83, 24, 168, 61 }, null, "Administrator", "adminovo" });
        }
    }
}
