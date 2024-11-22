using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_Identities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Member_MemberId",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "Member");

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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "FirstName", "LastName", "PersonNumber", "UserName" },
                values: new object[,]
                {
                    { "M001", "Anna", "Darke", "19870324", "annadark" },
                    { "M002", "Jane", "Austin", "19960712", "janeaustin" }
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
                    { "BIKE001", new DateTime(2024, 11, 21, 16, 48, 46, 441, DateTimeKind.Local).AddTicks(1981), "Yamaha", new DateTime(2024, 11, 21, 20, 48, 46, 441, DateTimeKind.Local).AddTicks(1983), "Black", null, "MT-15", 2 },
                    { "CAR001", new DateTime(2024, 11, 21, 15, 48, 46, 441, DateTimeKind.Local).AddTicks(1929), "Toyota", new DateTime(2024, 11, 21, 19, 48, 46, 441, DateTimeKind.Local).AddTicks(1977), "Red", null, "Corolla", 1 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpot",
                columns: new[] { "Id", "HourRate", "IsAvailable", "ParkingNumber", "RegNumber", "Size" },
                values: new object[,]
                {
                    { "P001", 10, true, 1, "CAR001", "Medium" },
                    { "P002", 15, true, 2, "BIKE001", "Small" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Member_MemberId",
                table: "Vehicle",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }
    }
}
