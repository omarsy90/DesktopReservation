using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class isConfirmaedAttributeNullableMigaration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("8b1ceeed-d505-4f1e-9631-64063abc5fd7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("d76bfe06-8fa3-4670-90cf-5e1be6df6ad7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("2595a9ad-3d0f-484c-aadd-353e2c6ff942"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("54a31ae5-b075-4fe1-8426-7d3d5e1e2d89"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("2595a9ad-3d0f-484c-aadd-353e2c6ff942"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("54a31ae5-b075-4fe1-8426-7d3d5e1e2d89"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[,]
                {
                    { new Guid("8b1ceeed-d505-4f1e-9631-64063abc5fd7"), "dep", "admin@gmail.com", "admin", "admin", "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=", 2 },
                    { new Guid("d76bfe06-8fa3-4670-90cf-5e1be6df6ad7"), "dep", "user@gmail.com", "user", "user", "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=", 1 }
                });
        }
    }
}
