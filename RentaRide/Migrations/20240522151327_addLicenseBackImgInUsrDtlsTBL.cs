using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class addLicenseBackImgInUsrDtlsTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userLicenseBack",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userLicenseBackFileExt",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userLicenseBack",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userLicenseBackFileExt",
                table: "TBL_UserDetails");
        }
    }
}
