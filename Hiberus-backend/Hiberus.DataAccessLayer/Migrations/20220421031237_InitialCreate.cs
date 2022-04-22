using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiberus.DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hiberus");

            migrationBuilder.CreateTable(
                name: "Rates",
                schema: "hiberus",
                columns: table => new
                {
                    From = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    To = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    RateValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "hiberus",
                columns: table => new
                {
                    Sku = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates",
                schema: "hiberus");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "hiberus");
        }
    }
}
