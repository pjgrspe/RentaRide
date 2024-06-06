using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class ListingsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Listings_TBL_Rates_listingRate",
                table: "TBL_Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_Rates_orderRating",
                table: "TBL_Orders");

            migrationBuilder.DropTable(
                name: "TBL_Rates");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Orders_orderRating",
                table: "TBL_Orders");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Listings_listingRate",
                table: "TBL_Listings");

            migrationBuilder.DropColumn(
                name: "orderRating",
                table: "TBL_Orders");

            migrationBuilder.DropColumn(
                name: "listingRate",
                table: "TBL_Listings");

            migrationBuilder.RenameColumn(
                name: "listingPrice",
                table: "TBL_Listings",
                newName: "listingWeeklyPrice");

            migrationBuilder.RenameColumn(
                name: "listingIsRented",
                table: "TBL_Listings",
                newName: "listingIsActive");

            migrationBuilder.AddColumn<decimal>(
                name: "listingDailyPrice",
                table: "TBL_Listings",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "listingHourlyPrice",
                table: "TBL_Listings",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "listingMonthlyPrice",
                table: "TBL_Listings",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "listingDailyPrice",
                table: "TBL_Listings");

            migrationBuilder.DropColumn(
                name: "listingHourlyPrice",
                table: "TBL_Listings");

            migrationBuilder.DropColumn(
                name: "listingMonthlyPrice",
                table: "TBL_Listings");

            migrationBuilder.RenameColumn(
                name: "listingWeeklyPrice",
                table: "TBL_Listings",
                newName: "listingPrice");

            migrationBuilder.RenameColumn(
                name: "listingIsActive",
                table: "TBL_Listings",
                newName: "listingIsRented");

            migrationBuilder.AddColumn<int>(
                name: "orderRating",
                table: "TBL_Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "listingRate",
                table: "TBL_Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_orderRating",
                table: "TBL_Orders",
                column: "orderRating");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Listings_listingRate",
                table: "TBL_Listings",
                column: "listingRate");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Listings_TBL_Rates_listingRate",
                table: "TBL_Listings",
                column: "listingRate",
                principalTable: "TBL_Rates",
                principalColumn: "listingRateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_Rates_orderRating",
                table: "TBL_Orders",
                column: "orderRating",
                principalTable: "TBL_Rates",
                principalColumn: "listingRateID");
        }
    }
}
