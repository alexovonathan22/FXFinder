﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FXFinder.Core.DataAccess;

namespace FXFinder.Core.Migrations
{
    [DbContext(typeof(WalletDbContext))]
    [Migration("20210112012337_Account digits")]
    partial class Accountdigits
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2021, 1, 12, 2, 23, 36, 862, DateTimeKind.Local).AddTicks(3713),
                            Email = "aov.nathan@gmail.com",
                            PasswordHash = new byte[] { 62, 175, 181, 71, 251, 228, 35, 39, 189, 30, 51, 98, 47, 113, 67, 247, 36, 104, 8, 177, 246, 32, 15, 98, 205, 196, 227, 106, 180, 16, 136, 16, 130, 183, 165, 12, 65, 132, 119, 132, 254, 230, 17, 83, 169, 73, 179, 98, 237, 114, 192, 119, 152, 79, 7, 227, 124, 225, 120, 106, 196, 186, 100, 224 },
                            PasswordSalt = new byte[] { 114, 5, 140, 190, 105, 121, 172, 225, 10, 206, 169, 240, 192, 186, 234, 217, 193, 238, 121, 99, 0, 133, 58, 104, 175, 47, 142, 65, 252, 54, 113, 158, 207, 98, 99, 100, 146, 165, 113, 238, 152, 218, 226, 65, 46, 115, 12, 175, 214, 124, 38, 78, 12, 89, 229, 83, 79, 244, 37, 37, 224, 206, 54, 54, 171, 180, 17, 72, 81, 102, 154, 250, 81, 186, 78, 55, 73, 206, 52, 3, 53, 5, 182, 98, 175, 199, 62, 168, 127, 216, 240, 209, 97, 68, 3, 17, 124, 35, 249, 85, 62, 215, 96, 102, 120, 168, 58, 15, 88, 107, 57, 51, 37, 158, 138, 15, 21, 170, 178, 57, 241, 114, 79, 128, 241, 0, 164, 41 },
                            Role = "Administrator",
                            Username = "The Admin"
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

                    b.Property<string>("MainCurrencySymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainCurrencyTitle")
                        .HasColumnType("nvarchar(max)");

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
