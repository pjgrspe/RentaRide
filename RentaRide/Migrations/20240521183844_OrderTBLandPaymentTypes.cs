using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class OrderTBLandPaymentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Listings_TBL_ListingRates_listingRate",
                table: "TBL_Listings");

            migrationBuilder.DropTable(
                name: "TBL_ListingRates");

            migrationBuilder.CreateTable(
                name: "TBL_PayTypes",
                columns: table => new
                {
                    paytypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paytypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PayTypes", x => x.paytypeID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Rates",
                columns: table => new
                {
                    listingRateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    listingRateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Rates", x => x.listingRateID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    driverID = table.Column<int>(type: "int", nullable: false),
                    orderBookDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderPickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    orderReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    orderStatus = table.Column<bool>(type: "bit", nullable: true),
                    orderTotalCost = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    orderExtraFees = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    orderPickupLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderReturnLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    orderPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    orderReservationID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderRating = table.Column<int>(type: "int", nullable: true),
                    orderReview = table.Column<int>(type: "int", nullable: true),
                    orderNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderLocationLimit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Orders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_AspNetUsers_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_TBL_Cars_carID",
                        column: x => x.carID,
                        principalTable: "TBL_Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_TBL_Drivers_driverID",
                        column: x => x.driverID,
                        principalTable: "TBL_Drivers",
                        principalColumn: "driverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_TBL_PayTypes_orderPaymentMethod",
                        column: x => x.orderPaymentMethod,
                        principalTable: "TBL_PayTypes",
                        principalColumn: "paytypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_TBL_Rates_orderRating",
                        column: x => x.orderRating,
                        principalTable: "TBL_Rates",
                        principalColumn: "listingRateID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_carID",
                table: "TBL_Orders",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_driverID",
                table: "TBL_Orders",
                column: "driverID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_orderPaymentMethod",
                table: "TBL_Orders",
                column: "orderPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_orderRating",
                table: "TBL_Orders",
                column: "orderRating");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_userID",
                table: "TBL_Orders",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Listings_TBL_Rates_listingRate",
                table: "TBL_Listings",
                column: "listingRate",
                principalTable: "TBL_Rates",
                principalColumn: "listingRateID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Listings_TBL_Rates_listingRate",
                table: "TBL_Listings");

            migrationBuilder.DropTable(
                name: "TBL_Orders");

            migrationBuilder.DropTable(
                name: "TBL_PayTypes");

            migrationBuilder.DropTable(
                name: "TBL_Rates");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Listings_TBL_ListingRates_listingRate",
                table: "TBL_Listings",
                column: "listingRate",
                principalTable: "TBL_ListingRates",
                principalColumn: "listingRateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
