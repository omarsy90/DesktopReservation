using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUser3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 21, 47, 518, DateTimeKind.Local).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 21, 47, 521, DateTimeKind.Local).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 21, 47, 521, DateTimeKind.Local).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 21, 47, 521, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Department", "Email", "FirstName", "LastName", "Password", "RoleID" },
                values: new object[] { "", "dep", "user3@gmail.com", "user3", "user3", "oKoKqib6p6u5vbpdI2sl+AmV/3TVSbDGa5uvEyRHb/0=", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "CommentedAt",
                value: new DateTime(2024, 10, 28, 18, 47, 30, 730, DateTimeKind.Local).AddTicks(4795));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "CommentedAt",
                value: new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(6997));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "CommentedAt",
                value: new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(7021));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4,
                column: "CommentedAt",
                value: new DateTime(2024, 10, 28, 18, 47, 30, 732, DateTimeKind.Local).AddTicks(7025));
        }
    }
}
