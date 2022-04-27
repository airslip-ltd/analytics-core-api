using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class FixCurrencyUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankBusinessBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankBusinessBalances");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccountBalanceSummaries");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccountBalances");
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankBusinessBalanceSnapshots",
                type: "varchar (5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankBusinessBalances",
                type: "varchar (5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankAccountBalanceSummaries",
                type: "varchar (5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankAccountBalanceSnapshots",
                type: "varchar (5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankAccountBalances",
                type: "varchar (5)",
                nullable: true);
        }
    }
}
