using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletToExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walletEntries_ExpenseTypes_ExpenseTypeId",
                table: "walletEntries");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseTypeId",
                table: "walletEntries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_WalletId",
                table: "Expenses",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_wallet_WalletId",
                table: "Expenses",
                column: "WalletId",
                principalTable: "wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_walletEntries_ExpenseTypes_ExpenseTypeId",
                table: "walletEntries",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_wallet_WalletId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_walletEntries_ExpenseTypes_ExpenseTypeId",
                table: "walletEntries");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_WalletId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseTypeId",
                table: "walletEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_walletEntries_ExpenseTypes_ExpenseTypeId",
                table: "walletEntries",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
