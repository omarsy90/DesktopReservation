using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    OfficeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Desks",
                columns: table => new
                {
                    DeskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeID = table.Column<int>(type: "int", nullable: false),
                    IsDeskActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desks", x => x.DeskID);
                    table.ForeignKey(
                        name: "FK_Desks_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "OfficeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeskEquipment",
                columns: table => new
                {
                    DesksDeskID = table.Column<int>(type: "int", nullable: false),
                    EquipmentsEquipmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskEquipment", x => new { x.DesksDeskID, x.EquipmentsEquipmentID });
                    table.ForeignKey(
                        name: "FK_DeskEquipment_Desks_DesksDeskID",
                        column: x => x.DesksDeskID,
                        principalTable: "Desks",
                        principalColumn: "DeskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeskEquipment_Equipments_EquipmentsEquipmentID",
                        column: x => x.EquipmentsEquipmentID,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DeskID = table.Column<int>(type: "int", nullable: false),
                    CommentTxt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Desks_DeskID",
                        column: x => x.DeskID,
                        principalTable: "Desks",
                        principalColumn: "DeskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeskID = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isFavourited = table.Column<bool>(type: "bit", nullable: false),
                    ReservationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Desks_DeskID",
                        column: x => x.DeskID,
                        principalTable: "Desks",
                        principalColumn: "DeskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentID", "Feature" },
                values: new object[,]
                {
                    { 1, "socket" },
                    { 2, "screen" }
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "OfficeID", "Name" },
                values: new object[,]
                {
                    { 1, "office_1" },
                    { 2, "office_2" },
                    { 3, "office_3" },
                    { 4, "office_4" },
                    { 5, "office_5" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Desks",
                columns: new[] { "DeskID", "IsDeskActive", "Label", "Map", "OfficeID" },
                values: new object[,]
                {
                    { 1, false, "desk_1", "default.png", 1 },
                    { 2, false, "desk_2", "default.png", 1 },
                    { 3, false, "desk_3", "default.png", 1 },
                    { 4, false, "desk_4", "default.png", 1 },
                    { 5, false, "desk_5", "default.png", 1 },
                    { 6, false, "desk_6", "default.png", 1 },
                    { 7, false, "desk_7", "default.png", 1 },
                    { 8, false, "desk_8", "default.png", 1 },
                    { 9, false, "desk_9", "default.png", 1 },
                    { 10, false, "desk_10", "default.png", 1 },
                    { 11, false, "desk_11", "default.png", 2 },
                    { 12, false, "desk_12", "default.png", 2 },
                    { 13, false, "desk_13", "default.png", 2 },
                    { 14, false, "desk_14", "default.png", 2 },
                    { 15, false, "desk_15", "default.png", 2 },
                    { 16, false, "desk_16", "default.png", 2 },
                    { 17, false, "desk_17", "default.png", 2 },
                    { 18, false, "desk_18", "default.png", 2 },
                    { 19, false, "desk_19", "default.png", 2 },
                    { 20, false, "desk_20", "default.png", 2 },
                    { 21, false, "desk_21", "default.png", 3 },
                    { 22, false, "desk_22", "default.png", 3 },
                    { 23, false, "desk_23", "default.png", 3 },
                    { 24, false, "desk_24", "default.png", 3 },
                    { 25, false, "desk_25", "default.png", 3 },
                    { 26, false, "desk_26", "default.png", 3 },
                    { 27, false, "desk_27", "default.png", 3 },
                    { 28, false, "desk_28", "default.png", 3 },
                    { 29, false, "desk_29", "default.png", 3 },
                    { 30, false, "desk_30", "default.png", 3 },
                    { 31, false, "desk_31", "default.png", 4 },
                    { 32, false, "desk_32", "default.png", 4 },
                    { 33, false, "desk_33", "default.png", 4 },
                    { 34, false, "desk_34", "default.png", 4 },
                    { 35, false, "desk_35", "default.png", 4 },
                    { 36, false, "desk_36", "default.png", 4 },
                    { 37, false, "desk_37", "default.png", 4 },
                    { 38, false, "desk_38", "default.png", 4 },
                    { 39, false, "desk_39", "default.png", 4 },
                    { 40, false, "desk_40", "default.png", 4 },
                    { 41, false, "desk_41", "default.png", 5 },
                    { 42, false, "desk_42", "default.png", 5 },
                    { 43, false, "desk_43", "default.png", 5 },
                    { 44, false, "desk_44", "default.png", 5 },
                    { 45, false, "desk_45", "default.png", 5 },
                    { 46, false, "desk_46", "default.png", 5 },
                    { 47, false, "desk_47", "default.png", 5 },
                    { 48, false, "desk_48", "default.png", 5 },
                    { 49, false, "desk_49", "default.png", 5 },
                    { 50, false, "desk_50", "default.png", 5 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("66afef1e-0253-45f4-9968-6072073ad6c6"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("86c4c6c0-a30f-4595-968d-3ef3e09e9f6d"), "dep", "user2@gmail.com", "user2", "user2", "MtBDli4jZtpNRe4EYPmhzZZ8IfoCBCr3idMQMd4fZFU=", 1 },
                    { new Guid("b490ad07-7670-4b7b-8b78-e0176fa9ec4a"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationID", "DateEnd", "DateStart", "DeskID", "IsConfirmed", "ReservationType", "UserID", "UserID1", "isFavourited" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "fix", "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", null, false },
                    { 2, new DateTime(2023, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "fix", "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", null, false },
                    { 3, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "fix", "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", null, false }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationID", "DateEnd", "DateStart", "DeskID", "ReservationType", "UserID", "UserID1", "isFavourited" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "flex", "B490AD07-7670-4B7B-8B78-E0176FA9EC4A", null, false },
                    { 5, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "flex", "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", null, false },
                    { 6, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "flex", "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DeskID",
                table: "Comments",
                column: "DeskID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID1",
                table: "Comments",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_DeskEquipment_EquipmentsEquipmentID",
                table: "DeskEquipment",
                column: "EquipmentsEquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Desks_OfficeID",
                table: "Desks",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_DeskID",
                table: "Reservation",
                column: "DeskID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserID1",
                table: "Reservation",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DeskEquipment");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Desks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
