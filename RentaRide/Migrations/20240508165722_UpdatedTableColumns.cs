using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAdd",
                table: "TBL_UserDetails",
                newName: "userStreetAdd");

            migrationBuilder.RenameColumn(
                name: "ProvinceAdd",
                table: "TBL_UserDetails",
                newName: "userProvinceAdd");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "TBL_UserDetails",
                newName: "userDateCreated");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "TBL_UserDetails",
                newName: "userContact");

            migrationBuilder.RenameColumn(
                name: "CityAdd",
                table: "TBL_UserDetails",
                newName: "userCityAdd");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TBL_UserDetails",
                newName: "userDetailID");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "AspNetUsers",
                newName: "userMiddleName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "userLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "userFirstName");

            migrationBuilder.AddColumn<DateTime>(
                name: "userDOB",
                table: "TBL_UserDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "userDateLastModified",
                table: "TBL_UserDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "userDateModified",
                table: "TBL_UserDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "userisApproved",
                table: "AspNetUsers",
                type: "bit",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userDOB",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userDateLastModified",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userDateModified",
                table: "TBL_UserDetails");

            migrationBuilder.DropColumn(
                name: "userisApproved",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "userStreetAdd",
                table: "TBL_UserDetails",
                newName: "StreetAdd");

            migrationBuilder.RenameColumn(
                name: "userProvinceAdd",
                table: "TBL_UserDetails",
                newName: "ProvinceAdd");

            migrationBuilder.RenameColumn(
                name: "userDateCreated",
                table: "TBL_UserDetails",
                newName: "DOB");

            migrationBuilder.RenameColumn(
                name: "userContact",
                table: "TBL_UserDetails",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "userCityAdd",
                table: "TBL_UserDetails",
                newName: "CityAdd");

            migrationBuilder.RenameColumn(
                name: "userDetailID",
                table: "TBL_UserDetails",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "userMiddleName",
                table: "AspNetUsers",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "userLastName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "userFirstName",
                table: "AspNetUsers",
                newName: "FirstName");
        }
    }
}
