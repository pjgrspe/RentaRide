using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class TBLCarsDocsCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carDocuments",
                table: "TBL_Cars");

            migrationBuilder.AddColumn<string>(
                name: "carCRDoc",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carORDoc",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carCRDoc",
                table: "TBL_Cars");

            migrationBuilder.DropColumn(
                name: "carORDoc",
                table: "TBL_Cars");

            migrationBuilder.AddColumn<string>(
                name: "carDocuments",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
