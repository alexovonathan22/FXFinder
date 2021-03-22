using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class newstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
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
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AuthToken = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsEmailConfirm = table.Column<bool>(nullable: false),
                    IsPhoneNumConfirm = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    OTP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletAccts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ActionTaken = table.Column<string>(nullable: true),
                    GrandAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsCurrencyConverted = table.Column<bool>(nullable: false),
                    IsMainCurrency = table.Column<bool>(nullable: false),
                    AcctDigits = table.Column<string>(nullable: true),
                    CurrnencyTitle = table.Column<string>(nullable: true),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletAccts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletAccts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "Email", "FirstName", "IsEmailConfirm", "IsPhoneNumConfirm", "LastName", "ModifiedAt", "ModifiedBy", "OTP", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "Role", "Username" },
                values: new object[] { 1, null, null, new DateTime(2021, 3, 22, 16, 58, 10, 158, DateTimeKind.Local).AddTicks(4828), null, "avo.nathan@gmail.com", null, true, true, null, null, null, null, new byte[] { 249, 54, 1, 226, 149, 109, 207, 95, 138, 168, 129, 114, 173, 80, 138, 248, 161, 51, 16, 143, 135, 144, 187, 3, 5, 159, 28, 151, 194, 226, 200, 247, 123, 93, 219, 82, 201, 120, 45, 98, 31, 223, 165, 223, 176, 166, 15, 66, 192, 135, 42, 5, 242, 202, 79, 249, 147, 11, 183, 75, 157, 157, 36, 73 }, new byte[] { 232, 21, 212, 151, 74, 146, 63, 251, 12, 58, 31, 45, 92, 106, 95, 162, 218, 254, 176, 15, 174, 185, 62, 253, 115, 61, 211, 32, 252, 78, 125, 191, 7, 82, 2, 132, 155, 22, 139, 135, 51, 175, 51, 225, 158, 112, 106, 204, 35, 178, 120, 17, 19, 167, 67, 202, 85, 117, 127, 88, 38, 211, 255, 200, 92, 234, 52, 49, 226, 224, 144, 197, 149, 100, 251, 244, 58, 117, 201, 36, 176, 49, 158, 189, 214, 104, 243, 181, 3, 245, 125, 220, 190, 99, 0, 177, 144, 18, 27, 34, 69, 26, 235, 130, 121, 59, 16, 93, 93, 0, 52, 242, 238, 46, 199, 203, 227, 75, 232, 252, 1, 177, 167, 33, 200, 7, 229, 169 }, null, null, "Administrator", "adminovo" });

            migrationBuilder.CreateIndex(
                name: "IX_WalletAccts_UserId",
                table: "WalletAccts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletAccts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
