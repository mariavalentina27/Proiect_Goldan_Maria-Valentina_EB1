using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_Goldan_Maria_Valentina.Migrations
{
    /// <inheritdoc />
    public partial class ChangePurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Purchase",
                newName: "PurchaseDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "Purchase",
                newName: "OrderDate");
        }
    }
}
