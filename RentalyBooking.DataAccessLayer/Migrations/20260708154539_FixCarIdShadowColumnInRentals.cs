using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalyBooking.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixCarIdShadowColumnInRentals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cars_CarId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CarId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CarId1",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId1",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId1",
                table: "Rentals",
                column: "CarId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cars_CarId1",
                table: "Rentals",
                column: "CarId1",
                principalTable: "Cars",
                principalColumn: "CarId");
        }
    }
}
