using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class DriversTblUpd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "driverLicenseBack",
                table: "TBL_Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "driverLicenseBackExt",
                table: "TBL_Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "driverLicenseBack",
                table: "TBL_Drivers");

            migrationBuilder.DropColumn(
                name: "driverLicenseBackExt",
                table: "TBL_Drivers");
        }
    }
}
