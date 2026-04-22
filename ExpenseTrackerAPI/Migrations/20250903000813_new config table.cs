using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class newconfigtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "InvoicePath",
                table: "Configs");

            migrationBuilder.AddColumn<string>(
                name: "DateFormat",
                table: "Configs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Configs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DefaultCurrencyId",
                table: "Configs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableDiscounts",
                table: "Configs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableMultiCurrency",
                table: "Configs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FtpPassword",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FtpPort",
                table: "Configs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpServer",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpUsername",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Configs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpPassword",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmtpPort",
                table: "Configs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpServer",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpUsername",
                table: "Configs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "Configs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "UseFtp",
                table: "Configs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Configs_DefaultCurrencyId",
                table: "Configs",
                column: "DefaultCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Currency_DefaultCurrencyId",
                table: "Configs",
                column: "DefaultCurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Currency_DefaultCurrencyId",
                table: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_Configs_DefaultCurrencyId",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "DateFormat",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "DefaultCurrencyId",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "EnableDiscounts",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "EnableMultiCurrency",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "FtpPassword",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "FtpPort",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "FtpServer",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "FtpUsername",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "SmtpPassword",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "SmtpPort",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "SmtpServer",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "SmtpUsername",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "Configs");

            migrationBuilder.DropColumn(
                name: "UseFtp",
                table: "Configs");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Configs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InvoicePath",
                table: "Configs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
