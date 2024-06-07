using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class tblupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Cars_TBL_CarTypes_carType",
                table: "TBL_Cars");

            migrationBuilder.DropTable(
                name: "TBL_CarTypes");

            migrationBuilder.DropTable(
                name: "TBL_PayTypes");

            migrationBuilder.DropIndex(
                name: "IX_TBL_Cars_carType",
                table: "TBL_Cars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "orderReturnDate",
                table: "TBL_Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "orderReturnDate",
                table: "TBL_Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Cars_carType",
                table: "TBL_Cars",
                column: "carType");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Cars_TBL_CarTypes_carType",
                table: "TBL_Cars",
                column: "carType",
                principalTable: "TBL_CarTypes",
                principalColumn: "cartypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
