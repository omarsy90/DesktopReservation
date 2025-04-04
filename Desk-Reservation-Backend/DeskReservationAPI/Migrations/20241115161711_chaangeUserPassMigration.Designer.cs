﻿// <auto-generated />
using System;
using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241115161711_chaangeUserPassMigration")]
    partial class chaangeUserPassMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeskEquipment", b =>
                {
                    b.Property<int>("DesksDeskID")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentsEquipmentID")
                        .HasColumnType("int");

                    b.HasKey("DesksDeskID", "EquipmentsEquipmentID");

                    b.HasIndex("EquipmentsEquipmentID");

                    b.ToTable("DeskEquipment");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentID"));

                    b.Property<string>("CommentTxt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeskID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentID");

                    b.HasIndex("DeskID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentID = 1,
                            CommentTxt = "headset not available",
                            CommentedAt = new DateTime(2024, 11, 15, 17, 17, 10, 505, DateTimeKind.Local).AddTicks(2318),
                            DeskID = 1,
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A"
                        },
                        new
                        {
                            CommentID = 2,
                            CommentTxt = "screen should be bigger",
                            CommentedAt = new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1040),
                            DeskID = 2,
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A"
                        },
                        new
                        {
                            CommentID = 3,
                            CommentTxt = "chair should be movable",
                            CommentedAt = new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1062),
                            DeskID = 2,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D"
                        },
                        new
                        {
                            CommentID = 4,
                            CommentTxt = "good infrastructured",
                            CommentedAt = new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1066),
                            DeskID = 3,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D"
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Desk", b =>
                {
                    b.Property<int>("DeskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeskID"));

                    b.Property<bool>("IsDeskActive")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeID")
                        .HasColumnType("int");

                    b.HasKey("DeskID");

                    b.HasIndex("OfficeID");

                    b.ToTable("Desks");

                    b.HasData(
                        new
                        {
                            DeskID = 1,
                            IsDeskActive = false,
                            Label = "desk_1",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 2,
                            IsDeskActive = false,
                            Label = "desk_2",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 3,
                            IsDeskActive = false,
                            Label = "desk_3",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 4,
                            IsDeskActive = false,
                            Label = "desk_4",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 5,
                            IsDeskActive = false,
                            Label = "desk_5",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 6,
                            IsDeskActive = false,
                            Label = "desk_6",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 7,
                            IsDeskActive = false,
                            Label = "desk_7",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 8,
                            IsDeskActive = false,
                            Label = "desk_8",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 9,
                            IsDeskActive = false,
                            Label = "desk_9",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 10,
                            IsDeskActive = false,
                            Label = "desk_10",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 11,
                            IsDeskActive = false,
                            Label = "desk_11",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 12,
                            IsDeskActive = false,
                            Label = "desk_12",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 13,
                            IsDeskActive = false,
                            Label = "desk_13",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 14,
                            IsDeskActive = false,
                            Label = "desk_14",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 15,
                            IsDeskActive = false,
                            Label = "desk_15",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 16,
                            IsDeskActive = false,
                            Label = "desk_16",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 17,
                            IsDeskActive = false,
                            Label = "desk_17",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 18,
                            IsDeskActive = false,
                            Label = "desk_18",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 19,
                            IsDeskActive = false,
                            Label = "desk_19",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 20,
                            IsDeskActive = false,
                            Label = "desk_20",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 21,
                            IsDeskActive = false,
                            Label = "desk_21",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 22,
                            IsDeskActive = false,
                            Label = "desk_22",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 23,
                            IsDeskActive = false,
                            Label = "desk_23",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 24,
                            IsDeskActive = false,
                            Label = "desk_24",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 25,
                            IsDeskActive = false,
                            Label = "desk_25",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 26,
                            IsDeskActive = false,
                            Label = "desk_26",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 27,
                            IsDeskActive = false,
                            Label = "desk_27",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 28,
                            IsDeskActive = false,
                            Label = "desk_28",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 29,
                            IsDeskActive = false,
                            Label = "desk_29",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 30,
                            IsDeskActive = false,
                            Label = "desk_30",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 31,
                            IsDeskActive = false,
                            Label = "desk_31",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 32,
                            IsDeskActive = false,
                            Label = "desk_32",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 33,
                            IsDeskActive = false,
                            Label = "desk_33",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 34,
                            IsDeskActive = false,
                            Label = "desk_34",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 35,
                            IsDeskActive = false,
                            Label = "desk_35",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 36,
                            IsDeskActive = false,
                            Label = "desk_36",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 37,
                            IsDeskActive = false,
                            Label = "desk_37",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 38,
                            IsDeskActive = false,
                            Label = "desk_38",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 39,
                            IsDeskActive = false,
                            Label = "desk_39",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 40,
                            IsDeskActive = false,
                            Label = "desk_40",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 41,
                            IsDeskActive = false,
                            Label = "desk_41",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 42,
                            IsDeskActive = false,
                            Label = "desk_42",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 43,
                            IsDeskActive = false,
                            Label = "desk_43",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 44,
                            IsDeskActive = false,
                            Label = "desk_44",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 45,
                            IsDeskActive = false,
                            Label = "desk_45",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 46,
                            IsDeskActive = false,
                            Label = "desk_46",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 47,
                            IsDeskActive = false,
                            Label = "desk_47",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 48,
                            IsDeskActive = false,
                            Label = "desk_48",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 49,
                            IsDeskActive = false,
                            Label = "desk_49",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 50,
                            IsDeskActive = false,
                            Label = "desk_50",
                            Map = "default.png",
                            OfficeID = 5
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Equipment", b =>
                {
                    b.Property<int>("EquipmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EquipmentID"));

                    b.Property<string>("Feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EquipmentID");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            EquipmentID = 1,
                            Feature = "socket"
                        },
                        new
                        {
                            EquipmentID = 2,
                            Feature = "screen"
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Office", b =>
                {
                    b.Property<int>("OfficeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfficeID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OfficeID");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            OfficeID = 1,
                            Name = "office_1"
                        },
                        new
                        {
                            OfficeID = 2,
                            Name = "office_2"
                        },
                        new
                        {
                            OfficeID = 3,
                            Name = "office_3"
                        },
                        new
                        {
                            OfficeID = 4,
                            Name = "office_4"
                        },
                        new
                        {
                            OfficeID = 5,
                            Name = "office_5"
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"));

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeskID")
                        .HasColumnType("int");

                    b.Property<string>("ReservationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isFavourited")
                        .HasColumnType("bit");

                    b.HasKey("ReservationID");

                    b.ToTable("Reservation");

                    b.HasDiscriminator<string>("ReservationType").HasValue("Reservation");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Name = "user"
                        },
                        new
                        {
                            RoleID = 2,
                            Name = "admin"
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                            Department = "dep",
                            Email = "user@gmail.com",
                            FirstName = "user",
                            LastName = "user",
                            Password = "9UWzrvTyQewY/nGJp4UzWdxyLRADf78z+kEhLXbd95k=",
                            RoleID = 1
                        },
                        new
                        {
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                            Department = "dep",
                            Email = "user2@gmail.com",
                            FirstName = "user2",
                            LastName = "user2",
                            Password = "u/Vn+7adXTk+fRBzXjUU9uUXTczxbY6IlDOLpdNmi2c=",
                            RoleID = 1
                        },
                        new
                        {
                            UserID = "E0074AA3-8523-4021-9063-5C8DB4FE55C0",
                            Department = "dep",
                            Email = "user3@gmail.com",
                            FirstName = "user3",
                            LastName = "user3",
                            Password = "sUnz1kQLbJQo2aSMCtnNlw9vwlIPzoE5PISq3gt77iQ=",
                            RoleID = 1
                        },
                        new
                        {
                            UserID = "66AFEF1E-0253-45F4-9968-6072073AD6C6",
                            Department = "dep",
                            Email = "admin@gmail.com",
                            FirstName = "admin",
                            LastName = "admin",
                            Password = "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=",
                            RoleID = 2
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.FixReservation", b =>
                {
                    b.HasBaseType("DeskReservationAPI.Model.Reservation");

                    b.Property<bool?>("IsConfirmed")
                        .HasColumnType("bit");

                    b.HasIndex("DeskID");

                    b.HasIndex("UserID");

                    b.HasDiscriminator().HasValue("fix");

                    b.HasData(
                        new
                        {
                            ReservationID = 1,
                            DateEnd = new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 1,
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                            isFavourited = false,
                            IsConfirmed = true
                        },
                        new
                        {
                            ReservationID = 2,
                            DateEnd = new DateTime(2023, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 1,
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                            isFavourited = false,
                            IsConfirmed = true
                        },
                        new
                        {
                            ReservationID = 3,
                            DateEnd = new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 1,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                            isFavourited = false
                        },
                        new
                        {
                            ReservationID = 7,
                            DateEnd = new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 1,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                            isFavourited = false
                        });
                });

            modelBuilder.Entity("DeskReservationAPI.Model.FlexReservation", b =>
                {
                    b.HasBaseType("DeskReservationAPI.Model.Reservation");

                    b.HasIndex("DeskID");

                    b.HasIndex("UserID");

                    b.HasDiscriminator().HasValue("flex");

                    b.HasData(
                        new
                        {
                            ReservationID = 4,
                            DateEnd = new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 2,
                            UserID = "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                            isFavourited = false
                        },
                        new
                        {
                            ReservationID = 5,
                            DateEnd = new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 2,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                            isFavourited = false
                        },
                        new
                        {
                            ReservationID = 6,
                            DateEnd = new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeskID = 3,
                            UserID = "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                            isFavourited = false
                        });
                });

            modelBuilder.Entity("DeskEquipment", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Desk", null)
                        .WithMany()
                        .HasForeignKey("DesksDeskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskReservationAPI.Model.Equipment", null)
                        .WithMany()
                        .HasForeignKey("EquipmentsEquipmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Comment", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskReservationAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Desk", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.User", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.FixReservation", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskReservationAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.FlexReservation", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskReservationAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
