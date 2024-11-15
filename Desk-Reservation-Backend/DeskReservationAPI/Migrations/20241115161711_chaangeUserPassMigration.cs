using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class chaangeUserPassMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 17, 17, 10, 505, DateTimeKind.Local).AddTicks(2318));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1040));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1062));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 17, 17, 10, 507, DateTimeKind.Local).AddTicks(1066));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                column: "Password",
                value: "u/Vn+7adXTk+fRBzXjUU9uUXTczxbY6IlDOLpdNmi2c=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                column: "Password",
                value: "9UWzrvTyQewY/nGJp4UzWdxyLRADf78z+kEhLXbd95k=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "E0074AA3-8523-4021-9063-5C8DB4FE55C0",
                column: "Password",
                value: "sUnz1kQLbJQo2aSMCtnNlw9vwlIPzoE5PISq3gt77iQ=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 25, 40, 852, DateTimeKind.Local).AddTicks(8932));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 25, 40, 855, DateTimeKind.Local).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 25, 40, 855, DateTimeKind.Local).AddTicks(9404));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 12, 16, 25, 40, 855, DateTimeKind.Local).AddTicks(9408));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "86C4C6C0-A30F-4595-968D-3EF3E09E9F6D",
                column: "Password",
                value: "MtBDli4jZtpNRe4EYPmhzZZ8IfoCBCr3idMQMd4fZFU=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "B490AD07-7670-4B7B-8B78-E0176FA9EC4A",
                column: "Password",
                value: "Wdhsa4sV38jU1gQf0nhLBxR3VoUjI8pcOxJHm+iImCc=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "E0074AA3-8523-4021-9063-5C8DB4FE55C0",
                column: "Password",
                value: "oKoKqib6p6u5vbpdI2sl+AmV/3TVSbDGa5uvEyRHb/0=");
        }
    }
}
