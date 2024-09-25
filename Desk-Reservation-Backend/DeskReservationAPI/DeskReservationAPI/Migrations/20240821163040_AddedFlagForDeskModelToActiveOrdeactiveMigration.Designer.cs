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
    [Migration("20240821163040_AddedFlagForDeskModelToActiveOrdeactiveMigration")]
    partial class AddedFlagForDeskModelToActiveOrdeactiveMigration
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentID");

                    b.HasIndex("DeskID");

                    b.HasIndex("UserID1");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Desk", b =>
                {
                    b.Property<int>("DeskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeskID"));

                    b.Property<int>("IsDeskActive")
                        .HasColumnType("int");

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
                            IsDeskActive = 0,
                            Label = "desk_1",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 2,
                            IsDeskActive = 0,
                            Label = "desk_2",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 3,
                            IsDeskActive = 0,
                            Label = "desk_3",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 4,
                            IsDeskActive = 0,
                            Label = "desk_4",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 5,
                            IsDeskActive = 0,
                            Label = "desk_5",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 6,
                            IsDeskActive = 0,
                            Label = "desk_6",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 7,
                            IsDeskActive = 0,
                            Label = "desk_7",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 8,
                            IsDeskActive = 0,
                            Label = "desk_8",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 9,
                            IsDeskActive = 0,
                            Label = "desk_9",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 10,
                            IsDeskActive = 0,
                            Label = "desk_10",
                            Map = "default.png",
                            OfficeID = 1
                        },
                        new
                        {
                            DeskID = 11,
                            IsDeskActive = 0,
                            Label = "desk_11",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 12,
                            IsDeskActive = 0,
                            Label = "desk_12",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 13,
                            IsDeskActive = 0,
                            Label = "desk_13",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 14,
                            IsDeskActive = 0,
                            Label = "desk_14",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 15,
                            IsDeskActive = 0,
                            Label = "desk_15",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 16,
                            IsDeskActive = 0,
                            Label = "desk_16",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 17,
                            IsDeskActive = 0,
                            Label = "desk_17",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 18,
                            IsDeskActive = 0,
                            Label = "desk_18",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 19,
                            IsDeskActive = 0,
                            Label = "desk_19",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 20,
                            IsDeskActive = 0,
                            Label = "desk_20",
                            Map = "default.png",
                            OfficeID = 2
                        },
                        new
                        {
                            DeskID = 21,
                            IsDeskActive = 0,
                            Label = "desk_21",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 22,
                            IsDeskActive = 0,
                            Label = "desk_22",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 23,
                            IsDeskActive = 0,
                            Label = "desk_23",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 24,
                            IsDeskActive = 0,
                            Label = "desk_24",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 25,
                            IsDeskActive = 0,
                            Label = "desk_25",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 26,
                            IsDeskActive = 0,
                            Label = "desk_26",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 27,
                            IsDeskActive = 0,
                            Label = "desk_27",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 28,
                            IsDeskActive = 0,
                            Label = "desk_28",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 29,
                            IsDeskActive = 0,
                            Label = "desk_29",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 30,
                            IsDeskActive = 0,
                            Label = "desk_30",
                            Map = "default.png",
                            OfficeID = 3
                        },
                        new
                        {
                            DeskID = 31,
                            IsDeskActive = 0,
                            Label = "desk_31",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 32,
                            IsDeskActive = 0,
                            Label = "desk_32",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 33,
                            IsDeskActive = 0,
                            Label = "desk_33",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 34,
                            IsDeskActive = 0,
                            Label = "desk_34",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 35,
                            IsDeskActive = 0,
                            Label = "desk_35",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 36,
                            IsDeskActive = 0,
                            Label = "desk_36",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 37,
                            IsDeskActive = 0,
                            Label = "desk_37",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 38,
                            IsDeskActive = 0,
                            Label = "desk_38",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 39,
                            IsDeskActive = 0,
                            Label = "desk_39",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 40,
                            IsDeskActive = 0,
                            Label = "desk_40",
                            Map = "default.png",
                            OfficeID = 4
                        },
                        new
                        {
                            DeskID = 41,
                            IsDeskActive = 0,
                            Label = "desk_41",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 42,
                            IsDeskActive = 0,
                            Label = "desk_42",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 43,
                            IsDeskActive = 0,
                            Label = "desk_43",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 44,
                            IsDeskActive = 0,
                            Label = "desk_44",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 45,
                            IsDeskActive = 0,
                            Label = "desk_45",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 46,
                            IsDeskActive = 0,
                            Label = "desk_46",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 47,
                            IsDeskActive = 0,
                            Label = "desk_47",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 48,
                            IsDeskActive = 0,
                            Label = "desk_48",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 49,
                            IsDeskActive = 0,
                            Label = "desk_49",
                            Map = "default.png",
                            OfficeID = 5
                        },
                        new
                        {
                            DeskID = 50,
                            IsDeskActive = 0,
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

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EquipmentID");

                    b.ToTable("Equipments");
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isFavourited")
                        .HasColumnType("bit");

                    b.HasKey("ReservationID");

                    b.HasIndex("DeskID");

                    b.HasIndex("UserID1");

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
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

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
                });

            modelBuilder.Entity("DeskReservationAPI.Model.Fixreservation", b =>
                {
                    b.HasBaseType("DeskReservationAPI.Model.Reservation");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("fix");
                });

            modelBuilder.Entity("DeskReservationAPI.Model.FlexReservation", b =>
                {
                    b.HasBaseType("DeskReservationAPI.Model.Reservation");

                    b.HasDiscriminator().HasValue("flex");
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
                        .HasForeignKey("UserID1")
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

            modelBuilder.Entity("DeskReservationAPI.Model.Reservation", b =>
                {
                    b.HasOne("DeskReservationAPI.Model.Desk", "Desk")
                        .WithMany()
                        .HasForeignKey("DeskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeskReservationAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("User");
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

            modelBuilder.Entity("DeskReservationAPI.Model.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
