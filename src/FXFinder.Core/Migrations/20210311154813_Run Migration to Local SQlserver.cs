﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FXFinder.Core.Migrations
{
    public partial class RunMigrationtoLocalSQlserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 3, 11, 16, 48, 12, 738, DateTimeKind.Local).AddTicks(3855), new byte[] { 90, 42, 136, 33, 145, 158, 174, 217, 108, 29, 84, 64, 68, 98, 206, 22, 31, 173, 229, 18, 116, 51, 72, 103, 51, 93, 79, 133, 251, 138, 176, 151, 149, 130, 88, 108, 46, 5, 218, 225, 24, 14, 135, 239, 63, 18, 128, 125, 164, 86, 98, 136, 176, 195, 224, 234, 126, 32, 118, 153, 96, 112, 177, 6 }, new byte[] { 149, 145, 27, 163, 186, 102, 8, 252, 62, 93, 160, 130, 90, 31, 252, 247, 17, 215, 43, 78, 140, 172, 53, 114, 99, 18, 185, 40, 4, 120, 229, 155, 206, 94, 16, 174, 187, 152, 140, 120, 170, 128, 76, 168, 121, 91, 117, 90, 243, 98, 0, 10, 121, 30, 51, 25, 215, 180, 99, 165, 62, 61, 170, 69, 211, 153, 103, 141, 227, 80, 105, 77, 230, 183, 127, 12, 213, 96, 96, 241, 16, 191, 52, 165, 109, 46, 119, 90, 146, 111, 78, 39, 34, 46, 52, 210, 138, 78, 132, 168, 102, 46, 215, 110, 175, 249, 168, 36, 123, 241, 66, 139, 53, 239, 105, 190, 111, 234, 181, 141, 158, 205, 8, 12, 121, 47, 21, 97 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2021, 3, 11, 16, 6, 54, 639, DateTimeKind.Local).AddTicks(8998), new byte[] { 244, 29, 105, 98, 195, 106, 163, 76, 69, 36, 4, 2, 234, 149, 131, 25, 230, 31, 162, 243, 3, 5, 112, 61, 20, 99, 139, 255, 33, 163, 198, 248, 89, 73, 183, 74, 52, 202, 80, 168, 23, 50, 106, 69, 235, 111, 212, 52, 78, 115, 22, 105, 108, 216, 107, 53, 9, 234, 128, 139, 80, 67, 49, 246 }, new byte[] { 38, 176, 41, 166, 217, 238, 221, 180, 221, 113, 64, 56, 132, 207, 152, 240, 86, 204, 53, 137, 61, 156, 213, 245, 130, 196, 158, 223, 109, 177, 206, 15, 250, 38, 175, 35, 17, 155, 209, 197, 90, 239, 224, 101, 223, 125, 191, 51, 230, 85, 223, 13, 39, 30, 28, 235, 210, 26, 50, 185, 202, 198, 12, 197, 49, 227, 196, 77, 113, 145, 122, 151, 25, 238, 241, 167, 17, 60, 197, 251, 61, 46, 67, 89, 172, 102, 218, 144, 191, 38, 197, 196, 58, 187, 63, 78, 46, 10, 63, 6, 13, 202, 128, 120, 207, 16, 25, 85, 193, 154, 116, 211, 143, 157, 189, 200, 134, 83, 225, 187, 240, 5, 192, 45, 249, 112, 31, 178 } });
        }
    }
}