﻿// <auto-generated />
using System;
using FXFinder.Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FXFinder.Core.Migrations
{
    [DbContext(typeof(WalletDbContext))]
    [Migration("20210311150655_Change User to FXUser")]
    partial class ChangeUsertoFXUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FXFinder.Core.DBModels.FXUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionTaken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPhoneNumConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 10,
                            CreatedAt = new DateTime(2021, 3, 11, 16, 6, 54, 639, DateTimeKind.Local).AddTicks(8998),
                            Email = "avo.nathan@gmail.com",
                            IsEmailConfirm = true,
                            IsPhoneNumConfirm = true,
                            PasswordHash = new byte[] { 244, 29, 105, 98, 195, 106, 163, 76, 69, 36, 4, 2, 234, 149, 131, 25, 230, 31, 162, 243, 3, 5, 112, 61, 20, 99, 139, 255, 33, 163, 198, 248, 89, 73, 183, 74, 52, 202, 80, 168, 23, 50, 106, 69, 235, 111, 212, 52, 78, 115, 22, 105, 108, 216, 107, 53, 9, 234, 128, 139, 80, 67, 49, 246 },
                            PasswordSalt = new byte[] { 38, 176, 41, 166, 217, 238, 221, 180, 221, 113, 64, 56, 132, 207, 152, 240, 86, 204, 53, 137, 61, 156, 213, 245, 130, 196, 158, 223, 109, 177, 206, 15, 250, 38, 175, 35, 17, 155, 209, 197, 90, 239, 224, 101, 223, 125, 191, 51, 230, 85, 223, 13, 39, 30, 28, 235, 210, 26, 50, 185, 202, 198, 12, 197, 49, 227, 196, 77, 113, 145, 122, 151, 25, 238, 241, 167, 17, 60, 197, 251, 61, 46, 67, 89, 172, 102, 218, 144, 191, 38, 197, 196, 58, 187, 63, 78, 46, 10, 63, 6, 13, 202, 128, 120, 207, 16, 25, 85, 193, 154, 116, 211, 143, 157, 189, 200, 134, 83, 225, 187, 240, 5, 192, 45, 249, 112, 31, 178 },
                            Role = "Administrator",
                            Username = "adminovo"
                        });
                });

            modelBuilder.Entity("FXFinder.Core.DBModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionTaken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencySymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirm")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FXFinder.Core.DBModels.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcctDigits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActionTaken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencySymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrnencyTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrandAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsCurrencyConverted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMainCurrency")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WalletAccts");
                });

            modelBuilder.Entity("FXFinder.Core.DBModels.Wallet", b =>
                {
                    b.HasOne("FXFinder.Core.DBModels.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}