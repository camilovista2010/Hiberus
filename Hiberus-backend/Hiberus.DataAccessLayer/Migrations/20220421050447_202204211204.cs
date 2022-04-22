using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiberus.DataAccessLayer.Migrations
{
    public partial class _202204211204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RateValue",
                schema: "hiberus",
                table: "Rates",
                newName: "rate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rate",
                schema: "hiberus",
                table: "Rates",
                newName: "RateValue");
        }
    }
}
