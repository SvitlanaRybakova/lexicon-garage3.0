using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_RegNumber_Optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_Vehicle_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "ParkingSpot",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                unique: true,
                filter: "[RegNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_Vehicle_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                principalTable: "Vehicle",
                principalColumn: "RegNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_Vehicle_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "ParkingSpot",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpot_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_Vehicle_RegNumber",
                table: "ParkingSpot",
                column: "RegNumber",
                principalTable: "Vehicle",
                principalColumn: "RegNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
