using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class newones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletAccts_Users_UserId",
                table: "WalletAccts");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "IsFundOrWithdrawApproved",
                table: "WalletAccts");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrencyTitle",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirm",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneNumConfirm",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ActionTaken = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    CurrencyTitle = table.Column<string>(nullable: true),
                    AuthToken = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsEmailConfirm = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "IsEmailConfirm", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 10, null, null, new DateTime(2021, 3, 4, 12, 8, 39, 297, DateTimeKind.Local).AddTicks(7863), null, null, null, "avo.nathan@gmail.com", false, null, null, new byte[] { 18, 73, 170, 236, 139, 99, 174, 102, 253, 113, 188, 44, 122, 207, 53, 200, 81, 102, 186, 160, 230, 98, 55, 216, 245, 128, 91, 254, 2, 172, 250, 73, 179, 5, 198, 81, 98, 179, 101, 188, 34, 230, 217, 29, 191, 2, 143, 173, 148, 89, 151, 149, 102, 101, 114, 243, 249, 114, 9, 237, 163, 135, 85, 104 }, new byte[] { 34, 67, 98, 72, 244, 38, 15, 28, 105, 255, 51, 225, 2, 192, 185, 130, 64, 77, 48, 178, 92, 133, 127, 151, 227, 121, 225, 72, 86, 245, 60, 57, 227, 244, 216, 59, 120, 29, 121, 108, 28, 127, 240, 147, 226, 7, 21, 78, 113, 121, 26, 186, 94, 231, 91, 54, 101, 77, 204, 194, 27, 248, 151, 143, 97, 217, 83, 228, 34, 212, 175, 24, 44, 30, 115, 164, 230, 214, 204, 155, 64, 207, 227, 154, 219, 200, 233, 106, 223, 227, 243, 191, 202, 199, 100, 133, 74, 15, 185, 197, 199, 157, 86, 179, 96, 216, 210, 120, 71, 188, 218, 215, 103, 99, 37, 156, 139, 252, 133, 228, 214, 127, 230, 0, 184, 161, 221, 225 }, null, "Administrator", "adminovo" });

            migrationBuilder.AddForeignKey(
                name: "FK_WalletAccts_User_UserId",
                table: "WalletAccts",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletAccts_User_UserId",
                table: "WalletAccts");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirm",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPhoneNumConfirm",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsFundOrWithdrawApproved",
                table: "WalletAccts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyTitle",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 10, null, null, new DateTime(2021, 1, 15, 7, 34, 45, 919, DateTimeKind.Local).AddTicks(9315), null, null, null, "avo.nathan@gmail.com", null, null, new byte[] { 177, 239, 22, 4, 247, 45, 158, 131, 206, 211, 37, 117, 81, 155, 136, 129, 142, 161, 114, 66, 100, 149, 124, 101, 232, 65, 124, 53, 164, 72, 248, 26, 251, 133, 223, 245, 31, 67, 83, 115, 150, 252, 174, 0, 142, 172, 67, 19, 141, 33, 180, 229, 54, 181, 76, 87, 100, 63, 63, 242, 130, 238, 119, 247 }, new byte[] { 76, 244, 236, 254, 234, 122, 74, 229, 228, 212, 34, 70, 220, 181, 181, 239, 5, 173, 143, 32, 35, 247, 16, 72, 157, 65, 240, 228, 208, 102, 69, 73, 99, 33, 250, 214, 67, 95, 157, 60, 110, 94, 128, 87, 29, 136, 171, 31, 2, 203, 247, 29, 146, 150, 182, 177, 0, 134, 174, 235, 4, 112, 70, 208, 221, 165, 78, 104, 97, 80, 167, 251, 234, 100, 62, 121, 56, 70, 137, 139, 168, 113, 110, 213, 21, 205, 115, 14, 219, 116, 222, 89, 114, 42, 1, 44, 168, 83, 185, 235, 64, 200, 206, 63, 94, 249, 209, 33, 255, 176, 224, 254, 24, 75, 129, 144, 122, 62, 174, 20, 77, 182, 242, 248, 30, 117, 179, 225 }, null, "Administrator", "adminovo" });

            migrationBuilder.AddForeignKey(
                name: "FK_WalletAccts_Users_UserId",
                table: "WalletAccts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
