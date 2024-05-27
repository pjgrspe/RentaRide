using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class TBLCarsDocsCol2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "carCRDocExt",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carORDocExt",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carPictureExt",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carCRDocExt",
                table: "TBL_Cars");

            migrationBuilder.DropColumn(
                name: "carORDocExt",
                table: "TBL_Cars");

            migrationBuilder.DropColumn(
                name: "carPictureExt",
                table: "TBL_Cars");
        }
    }
}
