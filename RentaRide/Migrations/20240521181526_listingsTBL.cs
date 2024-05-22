using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class listingsTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "carFuelType",
                table: "TBL_Cars",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "TBL_ListingRates",
                columns: table => new
                {
                    listingRateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    listingRateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ListingRates", x => x.listingRateID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Listings",
                columns: table => new
                {
                    listingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carID = table.Column<int>(type: "int", nullable: false),
                    listingDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    listingPrice = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    listingRate = table.Column<int>(type: "int", nullable: false),
                    listingIsRented = table.Column<bool>(type: "bit", nullable: false),
                    listingAvailabilityStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    listingAvailabilityEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Listings", x => x.listingID);
                    table.ForeignKey(
                        name: "FK_TBL_Listings_TBL_Cars_carID",
                        column: x => x.carID,
                        principalTable: "TBL_Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Listings_TBL_ListingRates_listingRate",
                        column: x => x.listingRate,
                        principalTable: "TBL_ListingRates",
                        principalColumn: "listingRateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Listings_carID",
                table: "TBL_Listings",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Listings_listingRate",
                table: "TBL_Listings",
                column: "listingRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Listings");

            migrationBuilder.DropTable(
                name: "TBL_ListingRates");

            migrationBuilder.AlterColumn<bool>(
                name: "carFuelType",
                table: "TBL_Cars",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
