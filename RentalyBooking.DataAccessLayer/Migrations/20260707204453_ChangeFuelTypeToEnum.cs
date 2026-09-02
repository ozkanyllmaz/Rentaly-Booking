using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalyBooking.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFuelTypeToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Cars SET FuelType = '1' Where FuelType = 'Benzin'");
            migrationBuilder.Sql("UPDATE Cars SET FuelType = '2' Where FuelType = 'Dizel'");
            migrationBuilder.Sql("UPDATE Cars SET FuelType = '3' Where FuelType = 'Lpg'");
            migrationBuilder.Sql("UPDATE Cars SET FuelType = '4' Where FuelType = 'Elektrik'");
            migrationBuilder.Sql("UPDATE Cars SET FuelType = '5' Where FuelType = 'Hibrit'");



            migrationBuilder.AlterColumn<int>(
                name: "FuelType",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
