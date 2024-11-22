using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "FirstName", "LastName", "PersonNumber", "UserName" },
                values: new object[,]
                {
                    { "M001", "Anna", "Darke", 123456, "annadark" },
                    { "M002", "Jane", "Austin", 654321, "janeaustin" }
                });

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
                    { "BIKE001", new DateTime(2024, 11, 20, 19, 49, 16, 97, DateTimeKind.Local).AddTicks(5267), "Yamaha", new DateTime(2024, 11, 20, 23, 49, 16, 97, DateTimeKind.Local).AddTicks(5272), "Black", null, "MT-15", 2 },
                    { "CAR001", new DateTime(2024, 11, 20, 18, 49, 16, 97, DateTimeKind.Local).AddTicks(5173), "Toyota", new DateTime(2024, 11, 20, 22, 49, 16, 97, DateTimeKind.Local).AddTicks(5255), "Red", null, "Corolla", 1 }
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
                table: "Member",
                keyColumn: "Id",
                keyValue: "M001");

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: "M002");

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
