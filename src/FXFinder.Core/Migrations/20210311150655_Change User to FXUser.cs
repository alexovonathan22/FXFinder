using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class ChangeUsertoFXUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsEmailConfirm", "IsPhoneNumConfirm", "LastName", "ModifiedAt", "ModifiedBy", "OTP", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "Role", "Username" },
                values: new object[] { 10, null, null, new DateTime(2021, 3, 11, 16, 6, 54, 639, DateTimeKind.Local).AddTicks(8998), null, "avo.nathan@gmail.com", null, true, true, null, null, null, null, new byte[] { 244, 29, 105, 98, 195, 106, 163, 76, 69, 36, 4, 2, 234, 149, 131, 25, 230, 31, 162, 243, 3, 5, 112, 61, 20, 99, 139, 255, 33, 163, 198, 248, 89, 73, 183, 74, 52, 202, 80, 168, 23, 50, 106, 69, 235, 111, 212, 52, 78, 115, 22, 105, 108, 216, 107, 53, 9, 234, 128, 139, 80, 67, 49, 246 }, new byte[] { 38, 176, 41, 166, 217, 238, 221, 180, 221, 113, 64, 56, 132, 207, 152, 240, 86, 204, 53, 137, 61, 156, 213, 245, 130, 196, 158, 223, 109, 177, 206, 15, 250, 38, 175, 35, 17, 155, 209, 197, 90, 239, 224, 101, 223, 125, 191, 51, 230, 85, 223, 13, 39, 30, 28, 235, 210, 26, 50, 185, 202, 198, 12, 197, 49, 227, 196, 77, 113, 145, 122, 151, 25, 238, 241, 167, 17, 60, 197, 251, 61, 46, 67, 89, 172, 102, 218, 144, 191, 38, 197, 196, 58, 187, 63, 78, 46, 10, 63, 6, 13, 202, 128, 120, 207, 16, 25, 85, 193, 154, 116, 211, 143, 157, 189, 200, 134, 83, 225, 187, 240, 5, 192, 45, 249, 112, 31, 178 }, null, null, "Administrator", "adminovo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "IsEmailConfirm", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 10, null, null, new DateTime(2021, 3, 11, 15, 57, 38, 457, DateTimeKind.Local).AddTicks(8318), null, null, null, "avo.nathan@gmail.com", false, null, null, new byte[] { 28, 65, 195, 251, 187, 43, 58, 32, 225, 71, 114, 239, 152, 184, 72, 50, 247, 164, 251, 25, 38, 169, 44, 33, 209, 66, 150, 31, 177, 234, 185, 118, 138, 239, 79, 129, 101, 252, 30, 135, 223, 169, 235, 238, 36, 184, 128, 5, 166, 202, 68, 112, 37, 214, 231, 33, 160, 183, 104, 240, 131, 219, 82, 20 }, new byte[] { 240, 176, 34, 100, 54, 242, 11, 69, 35, 4, 238, 154, 233, 95, 150, 240, 46, 124, 61, 169, 86, 81, 178, 81, 121, 116, 40, 137, 34, 145, 222, 212, 192, 235, 101, 160, 123, 239, 20, 126, 94, 206, 96, 174, 222, 143, 2, 220, 181, 39, 155, 119, 35, 48, 252, 67, 19, 19, 75, 182, 109, 55, 105, 228, 241, 147, 243, 188, 151, 197, 26, 56, 206, 103, 247, 193, 3, 226, 119, 43, 93, 228, 191, 97, 195, 249, 1, 141, 222, 115, 79, 75, 70, 232, 240, 211, 24, 129, 183, 246, 218, 158, 35, 185, 42, 169, 19, 186, 47, 153, 31, 9, 255, 231, 242, 219, 251, 117, 240, 164, 249, 15, 19, 153, 120, 166, 220, 225 }, null, "Administrator", "adminovo" });
        }
    }
}
