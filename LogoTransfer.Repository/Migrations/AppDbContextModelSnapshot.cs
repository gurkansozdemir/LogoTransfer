﻿// <auto-generated />
using System;
using LogoTransfer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LogoTransfer.Core.Entities.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MainMenuItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RowNumber")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MainMenuItemId");

                    b.ToTable("MenuItem");
                });

            modelBuilder.Entity("LogoTransfer.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                            CreatedOn = new DateTime(2023, 4, 21, 4, 20, 3, 463, DateTimeKind.Local).AddTicks(1352),
                            Description = "Full Authorize",
                            IsDeleted = false,
                            Name = "Supervisor"
                        },
                        new
                        {
                            Id = new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                            CreatedOn = new DateTime(2023, 4, 21, 4, 20, 3, 463, DateTimeKind.Local).AddTicks(1357),
                            Description = "Default User",
                            IsDeleted = false,
                            Name = "StandartUser"
                        });
                });

            modelBuilder.Entity("LogoTransfer.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EMail")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                            CreatedOn = new DateTime(2023, 4, 21, 4, 20, 3, 463, DateTimeKind.Local).AddTicks(1618),
                            EMail = "admin@logo.com.tr",
                            FirstName = "Super",
                            IsDeleted = false,
                            LastName = "User",
                            Password = "123",
                            RoleId = new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                            UserName = "supervisor"
                        });
                });

            modelBuilder.Entity("MenuItemRole", b =>
                {
                    b.Property<Guid>("MenuItemsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MenuItemsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("MenuItemRole");
                });

            modelBuilder.Entity("LogoTransfer.Core.Entities.MenuItem", b =>
                {
                    b.HasOne("LogoTransfer.Core.Entities.MenuItem", "MainMenuItem")
                        .WithMany()
                        .HasForeignKey("MainMenuItemId");

                    b.Navigation("MainMenuItem");
                });

            modelBuilder.Entity("LogoTransfer.Core.Entities.User", b =>
                {
                    b.HasOne("LogoTransfer.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MenuItemRole", b =>
                {
                    b.HasOne("LogoTransfer.Core.Entities.MenuItem", null)
                        .WithMany()
                        .HasForeignKey("MenuItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogoTransfer.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
