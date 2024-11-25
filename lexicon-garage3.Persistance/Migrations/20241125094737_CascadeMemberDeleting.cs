using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lexicon_garage3.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CascadeMemberDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_MemberId",
                table: "Vehicle",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
