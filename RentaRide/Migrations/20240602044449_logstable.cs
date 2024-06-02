using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class logstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CarLogs",
                columns: table => new
                {
                    logID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carID = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogMileage = table.Column<int>(type: "int", nullable: false),
                    LogType = table.Column<bool>(type: "bit", nullable: true),
                    LogDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CarLogs", x => x.logID);
                    table.ForeignKey(
                        name: "FK_TBL_CarLogs_TBL_Cars_carID",
                        column: x => x.carID,
                        principalTable: "TBL_Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CarLogs_carID",
                table: "TBL_CarLogs",
                column: "carID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CarLogs");
        }
    }
}
