using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class driversTBLandUDrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_Drivers",
                columns: table => new
                {
                    driverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    driverPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverLicense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    driverLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverRegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    driverLastDutyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    driverOnDuty = table.Column<bool>(type: "bit", nullable: false),
                    driverIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Drivers", x => x.driverID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Drivers");
        }
    }
}
