using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_Goldan_Maria_Valentina.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Concert");

            migrationBuilder.RenameColumn(
                name: "PurchaseID",
                table: "Purchase",
                newName: "ID");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Purchase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Concert",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ArtistID",
                table: "Concert",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CityID",
                table: "Customer",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_ArtistID",
                table: "Concert",
                column: "ArtistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Concert_Artist_ArtistID",
                table: "Concert",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityID",
                table: "Customer",
                column: "CityID",
                principalTable: "City",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concert_Artist_ArtistID",
                table: "Concert");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityID",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CityID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Concert_ArtistID",
                table: "Concert");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Concert");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Purchase",
                newName: "PurchaseID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Concert",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Concert",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
