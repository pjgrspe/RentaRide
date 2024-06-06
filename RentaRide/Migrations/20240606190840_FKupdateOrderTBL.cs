using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class FKupdateOrderTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_Cars_carID",
                table: "TBL_Orders");

            migrationBuilder.RenameColumn(
                name: "carID",
                table: "TBL_Orders",
                newName: "listingID");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_Orders_carID",
                table: "TBL_Orders",
                newName: "IX_TBL_Orders_listingID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_Listings_listingID",
                table: "TBL_Orders",
                column: "listingID",
                principalTable: "TBL_Listings",
                principalColumn: "listingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_Listings_listingID",
                table: "TBL_Orders");

            migrationBuilder.RenameColumn(
                name: "listingID",
                table: "TBL_Orders",
                newName: "carID");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_Orders_listingID",
                table: "TBL_Orders",
                newName: "IX_TBL_Orders_carID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_Cars_carID",
                table: "TBL_Orders",
                column: "carID",
                principalTable: "TBL_Cars",
                principalColumn: "carID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
