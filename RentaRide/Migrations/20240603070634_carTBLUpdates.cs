using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class carTBLUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carInactiveInfo",
                table: "TBL_Cars");

            migrationBuilder.DropColumn(
                name: "carIsActive",
                table: "TBL_Cars");

            migrationBuilder.RenameColumn(
                name: "carNextMaintenance",
                table: "TBL_Cars",
                newName: "carLastLogDate");

            migrationBuilder.RenameColumn(
                name: "carDateLogged",
                table: "TBL_Cars",
                newName: "carDateRegistered");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "carLastLogDate",
                table: "TBL_Cars",
                newName: "carNextMaintenance");

            migrationBuilder.RenameColumn(
                name: "carDateRegistered",
                table: "TBL_Cars",
                newName: "carDateLogged");

            migrationBuilder.AddColumn<string>(
                name: "carInactiveInfo",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "carIsActive",
                table: "TBL_Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
