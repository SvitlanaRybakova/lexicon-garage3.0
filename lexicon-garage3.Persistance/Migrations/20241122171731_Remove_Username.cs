using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleType",
                columns: new[] { "Id", "NumOfWheels", "VehicleSize", "VehicleTypeName" },
                values: new object[,]
                {
                    { 1, 4, "Medium", "Car" },
                    { 2, 2, "Small", "Motorcycle" },
                    { 3, 6, "Large", "Truck" }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "RegNumber", "ArrivalTime", "Brand", "CheckoutTime", "Color", "MemberId", "Model", "VehicleTypeId" },
                values: new object[,]
                {
                    { "BIKE001", new DateTime(2024, 11, 22, 17, 17, 30, 209, DateTimeKind.Local).AddTicks(2742), "Yamaha", new DateTime(2024, 11, 22, 21, 17, 30, 209, DateTimeKind.Local).AddTicks(2744), "Black", null, "MT-15", 2 },
                    { "CAR001", new DateTime(2024, 11, 22, 16, 17, 30, 209, DateTimeKind.Local).AddTicks(2693), "Toyota", new DateTime(2024, 11, 22, 20, 17, 30, 209, DateTimeKind.Local).AddTicks(2739), "Red", null, "Corolla", 1 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpot",
                columns: new[] { "Id", "HourRate", "IsAvailable", "ParkingNumber", "RegNumber", "Size" },
                values: new object[,]
                {
                    { "P001", 10, true, 1, "CAR001", "Medium" },
                    { "P002", 15, true, 2, "BIKE001", "Small" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingSpot",
                keyColumn: "Id",
                keyValue: "P001");

            migrationBuilder.DeleteData(
                table: "ParkingSpot",
                keyColumn: "Id",
                keyValue: "P002");

            migrationBuilder.DeleteData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "BIKE001");

            migrationBuilder.DeleteData(
                table: "Vehicle",
                keyColumn: "RegNumber",
                keyValue: "CAR001");

            migrationBuilder.DeleteData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
