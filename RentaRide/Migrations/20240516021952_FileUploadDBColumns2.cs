using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class FileUploadDBColumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user2ndValidIDFileExt",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userLicenseFileExt",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userProofofBillingFileExt",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userSelfieProofFileExt",
                table: "TBL_UserDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user2ndValidIDFileExt",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userLicenseFileExt",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userProofofBillingFileExt",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userSelfieProofFileExt",
                table: "TBL_UserDetails");
        }
    }
}
