using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class carsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CarTypes",
                columns: table => new
                {
                    cartypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CarTypes", x => x.cartypeID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Cars",
                columns: table => new
                {
                    carID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carYear = table.Column<int>(type: "int", nullable: false),
                    carTransmission = table.Column<bool>(type: "bit", nullable: false),
                    carColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carType = table.Column<int>(type: "int", nullable: false),
                    carMileage = table.Column<int>(type: "int", nullable: false),
                    carFuelType = table.Column<bool>(type: "bit", nullable: false),
                    carStatus = table.Column<bool>(type: "bit", nullable: true),
                    carIsActive = table.Column<bool>(type: "bit", nullable: false),
                    carInactiveInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    carLastMaintenance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    carNextMaintenance = table.Column<DateTime>(type: "datetime2", nullable: true),
                    carLicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Cars", x => x.carID);
                    table.ForeignKey(
                        name: "FK_TBL_Cars_TBL_CarTypes_carType",
                        column: x => x.carType,
                        principalTable: "TBL_CarTypes",
                        principalColumn: "cartypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Cars_carType",
                table: "TBL_Cars",
                column: "carType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Cars");

            migrationBuilder.DropTable(
                name: "TBL_CarTypes");
        }
    }
}
