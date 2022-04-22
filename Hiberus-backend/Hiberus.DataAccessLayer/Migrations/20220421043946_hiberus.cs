using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiberus.DataAccessLayer.Migrations
{
    public partial class hiberus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "hiberus",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "hiberus",
                table: "Rates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                schema: "hiberus",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                schema: "hiberus",
                table: "Rates",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                schema: "hiberus",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                schema: "hiberus",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "hiberus",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "hiberus",
                table: "Rates");
        }
    }
}
