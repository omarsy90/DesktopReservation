using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class userIDManuallyInserted : Migration
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
                    feature = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    OfficeID = table.Column<int>(type: "int", nullable: false)
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
                    UserID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Desks",
                columns: new[] { "DeskID", "Label", "Map", "OfficeID" },
                values: new object[,]
                {
                    { 1, "desk_1", "default.png", 1 },
                    { 2, "desk_2", "default.png", 1 },
                    { 3, "desk_3", "default.png", 1 },
                    { 4, "desk_4", "default.png", 1 },
                    { 5, "desk_5", "default.png", 1 },
                    { 6, "desk_6", "default.png", 1 },
                    { 7, "desk_7", "default.png", 1 },
                    { 8, "desk_8", "default.png", 1 },
                    { 9, "desk_9", "default.png", 1 },
                    { 10, "desk_10", "default.png", 1 },
                    { 11, "desk_11", "default.png", 2 },
                    { 12, "desk_12", "default.png", 2 },
                    { 13, "desk_13", "default.png", 2 },
                    { 14, "desk_14", "default.png", 2 },
                    { 15, "desk_15", "default.png", 2 },
                    { 16, "desk_16", "default.png", 2 },
                    { 17, "desk_17", "default.png", 2 },
                    { 18, "desk_18", "default.png", 2 },
                    { 19, "desk_19", "default.png", 2 },
                    { 20, "desk_20", "default.png", 2 },
                    { 21, "desk_21", "default.png", 3 },
                    { 22, "desk_22", "default.png", 3 },
                    { 23, "desk_23", "default.png", 3 },
                    { 24, "desk_24", "default.png", 3 },
                    { 25, "desk_25", "default.png", 3 },
                    { 26, "desk_26", "default.png", 3 },
                    { 27, "desk_27", "default.png", 3 },
                    { 28, "desk_28", "default.png", 3 },
                    { 29, "desk_29", "default.png", 3 },
                    { 30, "desk_30", "default.png", 3 },
                    { 31, "desk_31", "default.png", 4 },
                    { 32, "desk_32", "default.png", 4 },
                    { 33, "desk_33", "default.png", 4 },
                    { 34, "desk_34", "default.png", 4 },
                    { 35, "desk_35", "default.png", 4 },
                    { 36, "desk_36", "default.png", 4 },
                    { 37, "desk_37", "default.png", 4 },
                    { 38, "desk_38", "default.png", 4 },
                    { 39, "desk_39", "default.png", 4 },
                    { 40, "desk_40", "default.png", 4 },
                    { 41, "desk_41", "default.png", 5 },
                    { 42, "desk_42", "default.png", 5 },
                    { 43, "desk_43", "default.png", 5 },
                    { 44, "desk_44", "default.png", 5 },
                    { 45, "desk_45", "default.png", 5 },
                    { 46, "desk_46", "default.png", 5 },
                    { 47, "desk_47", "default.png", 5 },
                    { 48, "desk_48", "default.png", 5 },
                    { 49, "desk_49", "default.png", 5 },
                    { 50, "desk_50", "default.png", 5 }
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
