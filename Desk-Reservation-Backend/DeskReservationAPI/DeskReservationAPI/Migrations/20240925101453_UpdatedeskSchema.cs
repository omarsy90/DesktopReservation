using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedeskSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "feature",
                table: "Equipments",
                newName: "Feature");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeskActive",
                table: "Desks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 1,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 2,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 3,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 4,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 5,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 6,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 7,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 8,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 9,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 10,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 11,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 12,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 13,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 14,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 15,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 16,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 17,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 18,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 19,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 20,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 21,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 22,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 23,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 24,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 25,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 26,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 27,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 28,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 29,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 30,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 31,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 32,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 33,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 34,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 35,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 36,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 37,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 38,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 39,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 40,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 41,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 42,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 43,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 44,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 45,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 46,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 47,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 48,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 49,
                column: "IsDeskActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 50,
                column: "IsDeskActive",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Feature",
                table: "Equipments",
                newName: "feature");

            migrationBuilder.AlterColumn<int>(
                name: "IsDeskActive",
                table: "Desks",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 1,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 2,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 3,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 4,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 5,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 6,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 7,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 8,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 9,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 10,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 11,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 12,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 13,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 14,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 15,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 16,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 17,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 18,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 19,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 20,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 21,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 22,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 23,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 24,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 25,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 26,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 27,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 28,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 29,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 30,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 31,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 32,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 33,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 34,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 35,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 36,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 37,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 38,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 39,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 40,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 41,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 42,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 43,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 44,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 45,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 46,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 47,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 48,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 49,
                column: "IsDeskActive",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Desks",
                keyColumn: "DeskID",
                keyValue: 50,
                column: "IsDeskActive",
                value: 0);
        }
    }
}
