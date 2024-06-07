using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class orderTBLupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_PayTypes_orderPaymentMethod",
                table: "TBL_Orders");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Orders_orderPaymentMethod",
                table: "TBL_Orders");

            migrationBuilder.DropColumn(
                name: "orderPickupLocation",
                table: "TBL_Orders");

            migrationBuilder.DropColumn(
                name: "orderReturnLocation",
                table: "TBL_Orders");

            migrationBuilder.AlterColumn<int>(
                name: "orderStatus",
                table: "TBL_Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "orderReservationID",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "orderLocationLimit",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "orderStatus",
                table: "TBL_Orders",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "orderReservationID",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "orderLocationLimit",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "orderPickupLocation",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "orderReturnLocation",
                table: "TBL_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_orderPaymentMethod",
                table: "TBL_Orders",
                column: "orderPaymentMethod");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_PayTypes_orderPaymentMethod",
                table: "TBL_Orders",
                column: "orderPaymentMethod",
                principalTable: "TBL_PayTypes",
                principalColumn: "paytypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
