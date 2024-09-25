using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedEquipmentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("a13dc7a2-5451-413b-ac26-415f6157d53d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("c4541a59-d377-46d0-92a7-03e9cde1522e"));

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentID", "Feature" },
                values: new object[,]
                {
                    { 1, "socet" },
                    { 2, "screen" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("621ada31-ed84-4614-b064-d8e882154081"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("ba837924-5529-47fe-b449-bc249c3cd11b"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "EquipmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "EquipmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("621ada31-ed84-4614-b064-d8e882154081"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("ba837924-5529-47fe-b449-bc249c3cd11b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("a13dc7a2-5451-413b-ac26-415f6157d53d"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 },
                    { new Guid("c4541a59-d377-46d0-92a7-03e9cde1522e"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 }
                });
        }
    }
}
