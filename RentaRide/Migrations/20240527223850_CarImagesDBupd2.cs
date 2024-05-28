using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class CarImagesDBupd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarsDBModelcarID",
                table: "TBL_CarImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CarImages_CarsDBModelcarID",
                table: "TBL_CarImages",
                column: "CarsDBModelcarID");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_CarImages_TBL_Cars_CarsDBModelcarID",
                table: "TBL_CarImages",
                column: "CarsDBModelcarID",
                principalTable: "TBL_Cars",
                principalColumn: "carID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_CarImages_TBL_Cars_CarsDBModelcarID",
                table: "TBL_CarImages");

            migrationBuilder.DropIndex(
                name: "IX_TBL_CarImages_CarsDBModelcarID",
                table: "TBL_CarImages");

            migrationBuilder.DropColumn(
                name: "CarsDBModelcarID",
                table: "TBL_CarImages");
        }
    }
}
