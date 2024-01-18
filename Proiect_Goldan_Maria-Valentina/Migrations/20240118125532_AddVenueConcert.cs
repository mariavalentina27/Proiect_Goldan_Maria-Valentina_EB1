using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_Goldan_Maria_Valentina.Migrations
{
    /// <inheritdoc />
    public partial class AddVenueConcert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VenueConcert",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false),
                    ConcertID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueConcert", x => new { x.ConcertID, x.VenueID });
                    table.ForeignKey(
                        name: "FK_VenueConcert_Concert_ConcertID",
                        column: x => x.ConcertID,
                        principalTable: "Concert",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueConcert_Venue_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VenueConcert_VenueID",
                table: "VenueConcert",
                column: "VenueID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VenueConcert");

            migrationBuilder.DropTable(
                name: "Venue");
        }
    }
}
