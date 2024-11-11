using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCommentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentID", "CommentTxt", "CommentedAt", "DeskID", "UserID" },
                values: new object[,]
                {
                    { 1, "headset not available", new DateTime(2024, 10, 28, 18, 47, 30, 730, DateTimeKind.Local).AddTicks(4795), 1, "B490AD07-7670-4B7B-8B78-E0176FA9EC4A" },
                    { 2, "screen should be bigger", new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(6997), 2, "B490AD07-7670-4B7B-8B78-E0176FA9EC4A" },
                    { 3, "chair should be movable", new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(7021), 2, "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D" },
                    { 4, "good infrastructured", new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(7025), 3, "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4);
        }
    }
}
