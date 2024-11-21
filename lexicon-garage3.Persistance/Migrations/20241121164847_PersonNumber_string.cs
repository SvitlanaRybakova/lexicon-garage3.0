using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PersonNumber_string : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonNumber",
                table: "Member",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: "M001",
                column: "PersonNumber",
                value: "19870324");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: "M002",
                column: "PersonNumber",
                value: "19960712");

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "BIKE001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 21, 16, 48, 46, 441, DateTimeKind.Local).AddTicks(1981), new DateTime(2024, 11, 21, 20, 48, 46, 441, DateTimeKind.Local).AddTicks(1983) });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "CAR001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 21, 15, 48, 46, 441, DateTimeKind.Local).AddTicks(1929), new DateTime(2024, 11, 21, 19, 48, 46, 441, DateTimeKind.Local).AddTicks(1977) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonNumber",
                table: "Member",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: "M001",
                column: "PersonNumber",
                value: 123456);

            migrationBuilder.UpdateData(
                table: "Member",
                keyColumn: "Id",
                keyValue: "M002",
                column: "PersonNumber",
                value: 654321);

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "BIKE001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 20, 19, 49, 16, 97, DateTimeKind.Local).AddTicks(5267), new DateTime(2024, 11, 20, 23, 49, 16, 97, DateTimeKind.Local).AddTicks(5272) });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "CAR001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 20, 18, 49, 16, 97, DateTimeKind.Local).AddTicks(5173), new DateTime(2024, 11, 20, 22, 49, 16, 97, DateTimeKind.Local).AddTicks(5255) });
        }
    }
}
