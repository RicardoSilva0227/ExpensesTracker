using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class addCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    InvoicePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecimalPlaces = table.Column<int>(type: "int", nullable: false),
                    CultureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRateToBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsCrypto = table.Column<bool>(type: "bit", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Acronym", "Country", "CreationDate", "CultureCode", "DecimalPlaces", "ExchangeRateToBase", "IsCrypto", "IsDefault", "LastUpdated", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "EUR", "European Union", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "pt-PT", 2, 0m, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Euro", "€" },
                    { 2, "USD", "United States", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "en-US", 2, 0m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "US Dollar", "$" },
                    { 3, "JPY", "Japan", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ja-JP", 0, 0m, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Japanese Yen", "¥" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
