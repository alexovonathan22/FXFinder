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
    [Migration("20210322155811_new start")]
    partial class newstart
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
                            Id = 1,
                            CreatedAt = new DateTime(2021, 3, 22, 16, 58, 10, 158, DateTimeKind.Local).AddTicks(4828),
                            Email = "avo.nathan@gmail.com",
                            IsEmailConfirm = true,
                            IsPhoneNumConfirm = true,
                            PasswordHash = new byte[] { 249, 54, 1, 226, 149, 109, 207, 95, 138, 168, 129, 114, 173, 80, 138, 248, 161, 51, 16, 143, 135, 144, 187, 3, 5, 159, 28, 151, 194, 226, 200, 247, 123, 93, 219, 82, 201, 120, 45, 98, 31, 223, 165, 223, 176, 166, 15, 66, 192, 135, 42, 5, 242, 202, 79, 249, 147, 11, 183, 75, 157, 157, 36, 73 },
                            PasswordSalt = new byte[] { 232, 21, 212, 151, 74, 146, 63, 251, 12, 58, 31, 45, 92, 106, 95, 162, 218, 254, 176, 15, 174, 185, 62, 253, 115, 61, 211, 32, 252, 78, 125, 191, 7, 82, 2, 132, 155, 22, 139, 135, 51, 175, 51, 225, 158, 112, 106, 204, 35, 178, 120, 17, 19, 167, 67, 202, 85, 117, 127, 88, 38, 211, 255, 200, 92, 234, 52, 49, 226, 224, 144, 197, 149, 100, 251, 244, 58, 117, 201, 36, 176, 49, 158, 189, 214, 104, 243, 181, 3, 245, 125, 220, 190, 99, 0, 177, 144, 18, 27, 34, 69, 26, 235, 130, 121, 59, 16, 93, 93, 0, 52, 242, 238, 46, 199, 203, 227, 75, 232, 252, 1, 177, 167, 33, 200, 7, 229, 169 },
                            Role = "Administrator",
                            Username = "adminovo"
                        });
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
                    b.HasOne("FXFinder.Core.DBModels.FXUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}