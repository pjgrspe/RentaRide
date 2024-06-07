using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class finalupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_Drivers_driverID",
                table: "TBL_Orders");

            migrationBuilder.AlterColumn<int>(
                name: "driverID",
                table: "TBL_Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_Drivers_driverID",
                table: "TBL_Orders",
                column: "driverID",
                principalTable: "TBL_Drivers",
                principalColumn: "driverID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_Drivers_driverID",
                table: "TBL_Orders");

            migrationBuilder.AlterColumn<int>(
                name: "driverID",
                table: "TBL_Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_Drivers_driverID",
                table: "TBL_Orders",
                column: "driverID",
                principalTable: "TBL_Drivers",
                principalColumn: "driverID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
