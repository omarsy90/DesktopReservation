using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedReservationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationID", "DateEnd", "DateStart", "DeskID", "IsConfirmed", "ReservationType", "UserID", "UserID1", "isFavourited" },
                values: new object[] { 7, new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "fix", "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D", null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "ReservationID",
                keyValue: 7);
        }
    }
}
