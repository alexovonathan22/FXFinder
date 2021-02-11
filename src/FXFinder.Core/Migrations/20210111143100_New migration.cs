using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class Newmigration : Migration
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
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    CurrencyTitle = table.Column<string>(nullable: true),
                    AuthToken = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
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
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    CurrencyTitle = table.Column<string>(nullable: true),
                    MainCurrency = table.Column<string>(nullable: true),
                    MainCurrencySymbol = table.Column<string>(nullable: true),
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
                columns: new[] { "Id", "ActionTaken", "AuthToken", "CreatedAt", "CreatedBy", "CurrencySymbol", "CurrencyTitle", "Email", "ModifiedAt", "ModifiedBy", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "Username" },
                values: new object[] { 1, null, null, new DateTime(2021, 1, 11, 15, 31, 0, 28, DateTimeKind.Local).AddTicks(1052), 0, null, null, "aov.nathan@gmail.com", null, 0, new byte[] { 19, 50, 154, 148, 16, 77, 126, 251, 230, 39, 147, 46, 162, 66, 221, 160, 186, 54, 206, 148, 99, 194, 76, 214, 230, 228, 238, 75, 206, 151, 124, 169, 203, 52, 181, 237, 226, 159, 7, 236, 230, 91, 34, 22, 10, 81, 160, 29, 90, 200, 66, 118, 121, 133, 79, 61, 113, 162, 217, 90, 206, 4, 90, 193 }, new byte[] { 228, 81, 100, 208, 36, 203, 69, 139, 182, 235, 178, 171, 214, 234, 207, 134, 110, 71, 69, 16, 25, 59, 253, 90, 146, 37, 220, 180, 202, 226, 216, 231, 151, 145, 132, 118, 79, 215, 151, 55, 130, 1, 129, 199, 62, 35, 246, 179, 81, 172, 118, 36, 11, 88, 16, 80, 107, 232, 145, 54, 93, 18, 104, 175, 46, 91, 199, 133, 65, 143, 193, 62, 81, 237, 114, 128, 119, 240, 202, 40, 82, 211, 120, 128, 100, 54, 2, 46, 197, 118, 190, 143, 36, 208, 144, 233, 134, 98, 110, 32, 40, 2, 97, 131, 100, 219, 136, 136, 139, 24, 66, 118, 210, 182, 30, 18, 48, 155, 184, 253, 227, 238, 252, 102, 216, 217, 232, 216 }, null, "Administrator", "The Admin" });

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
