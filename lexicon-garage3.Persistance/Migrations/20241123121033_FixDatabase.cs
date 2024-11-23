using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "BIKE001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 23, 12, 10, 31, 687, DateTimeKind.Local).AddTicks(8983), new DateTime(2024, 11, 23, 16, 10, 31, 687, DateTimeKind.Local).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "CAR001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 23, 11, 10, 31, 687, DateTimeKind.Local).AddTicks(8890), new DateTime(2024, 11, 23, 15, 10, 31, 687, DateTimeKind.Local).AddTicks(8970) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "BIKE001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 22, 17, 17, 30, 209, DateTimeKind.Local).AddTicks(2742), new DateTime(2024, 11, 22, 21, 17, 30, 209, DateTimeKind.Local).AddTicks(2744) });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "CAR001",
                columns: new[] { "ArrivalTime", "CheckoutTime" },
                values: new object[] { new DateTime(2024, 11, 22, 16, 17, 30, 209, DateTimeKind.Local).AddTicks(2693), new DateTime(2024, 11, 22, 20, 17, 30, 209, DateTimeKind.Local).AddTicks(2739) });
        }
    }
}
