using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeUserPasswordsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 18, 3, 15, 70, DateTimeKind.Local).AddTicks(6510));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 18, 3, 15, 72, DateTimeKind.Local).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 18, 3, 15, 72, DateTimeKind.Local).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4,
                column: "CommentedAt",
                value: new DateTime(2024, 11, 15, 18, 3, 15, 72, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "66AFEF1E-0253-45F4-9968-6072073AD6C6",
                column: "Password",
                value: "y6Np0j9AGBxgWtXjG1yzbjWVyn5IAiL4Qp49ffABflw=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "66AFEF1E-0253-45F4-9968-6072073AD6C6",
                column: "Password",
                value: "ImurlGyyIPEa3+UpWmpkx3cHpNkG2U4JgJ6x6QtQjDA=");
        }
    }
}
