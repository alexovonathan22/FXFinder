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
    [Migration("20210112002800_Change model property")]
    partial class Changemodelproperty
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
                            CreatedAt = new DateTime(2021, 1, 12, 1, 27, 59, 498, DateTimeKind.Local).AddTicks(6601),
                            Email = "aov.nathan@gmail.com",
                            PasswordHash = new byte[] { 148, 43, 24, 217, 209, 88, 55, 156, 118, 152, 242, 3, 168, 51, 117, 216, 198, 203, 39, 62, 255, 108, 246, 208, 168, 163, 253, 205, 14, 71, 76, 117, 92, 39, 164, 167, 71, 72, 38, 216, 186, 141, 247, 23, 249, 248, 209, 132, 118, 102, 243, 62, 44, 80, 230, 36, 159, 250, 49, 13, 50, 236, 42, 87 },
                            PasswordSalt = new byte[] { 162, 130, 255, 30, 44, 249, 78, 101, 194, 89, 175, 90, 198, 228, 182, 67, 172, 170, 231, 18, 146, 107, 244, 97, 177, 222, 36, 45, 85, 124, 13, 247, 248, 96, 39, 156, 94, 221, 163, 119, 76, 64, 187, 121, 221, 112, 230, 15, 128, 127, 101, 142, 139, 106, 54, 213, 147, 156, 28, 81, 159, 84, 190, 175, 16, 48, 159, 38, 147, 246, 22, 221, 228, 48, 151, 199, 136, 221, 43, 17, 8, 246, 83, 78, 160, 216, 9, 145, 79, 129, 4, 202, 18, 96, 244, 182, 223, 96, 212, 5, 232, 76, 41, 61, 189, 49, 172, 139, 136, 7, 146, 197, 78, 110, 175, 81, 62, 242, 77, 147, 198, 149, 13, 174, 93, 84, 94, 138 },
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
