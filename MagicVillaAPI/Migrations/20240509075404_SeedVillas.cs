using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Villas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedAt", "Details", "Name", "Occupancy", "Rate", "Sqft" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 9, 7, 54, 4, 97, DateTimeKind.Utc).AddTicks(9067), "With personal Pool and Beach View", "Royal Villa", 5, 200, 200 },
                    { 2, new DateTime(2024, 5, 9, 7, 54, 4, 97, DateTimeKind.Utc).AddTicks(9070), "With 2 Floor, Jungle view and personal Pool", "Lux Villa", 8, 350, 250 },
                    { 3, new DateTime(2024, 5, 9, 7, 54, 4, 97, DateTimeKind.Utc).AddTicks(9072), "With Pool view and terrace", "Diamond Villa", 4, 150, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Villas");
        }
    }
}
