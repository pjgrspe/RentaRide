using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class CarImagesDBupd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "carPictureExt",
                table: "TBL_Cars",
                newName: "carThumbnailExt");

            migrationBuilder.RenameColumn(
                name: "carPicture",
                table: "TBL_Cars",
                newName: "carThumbnail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "carThumbnailExt",
                table: "TBL_Cars",
                newName: "carPictureExt");

            migrationBuilder.RenameColumn(
                name: "carThumbnail",
                table: "TBL_Cars",
                newName: "carPicture");
        }
    }
}
