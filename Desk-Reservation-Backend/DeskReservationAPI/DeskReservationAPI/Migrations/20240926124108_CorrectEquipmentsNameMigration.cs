using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectEquipmentsNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("621ada31-ed84-4614-b064-d8e882154081"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("ba837924-5529-47fe-b449-bc249c3cd11b"));

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "EquipmentID",
                keyValue: 1,
                column: "Feature",
                value: "socket");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("8b1ceeed-d505-4f1e-9631-64063abc5fd7"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("d76bfe06-8fa3-4670-90cf-5e1be6df6ad7"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("8b1ceeed-d505-4f1e-9631-64063abc5fd7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("d76bfe06-8fa3-4670-90cf-5e1be6df6ad7"));

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "EquipmentID",
                keyValue: 1,
                column: "Feature",
                value: "socet");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("621ada31-ed84-4614-b064-d8e882154081"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("ba837924-5529-47fe-b449-bc249c3cd11b"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });
        }
    }
}
