using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentaRide.Migrations
{
    /// <inheritdoc />
    public partial class CarImagesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "carPicture",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TBL_CarImages",
                columns: table => new
                {
                    carimgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carID = table.Column<int>(type: "int", nullable: false),
                    carimgName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CarImages", x => x.carimgID);
                    table.ForeignKey(
                        name: "FK_TBL_CarImages_TBL_Cars_carID",
                        column: x => x.carID,
                        principalTable: "TBL_Cars",
                        principalColumn: "carID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CarImages_carID",
                table: "TBL_CarImages",
                column: "carID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CarImages");

            migrationBuilder.AlterColumn<string>(
                name: "carPicture",
                table: "TBL_Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
