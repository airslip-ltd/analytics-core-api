

#nullable disable

using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;
namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AccountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE dbo.BankBusinessBalanceSnapshots");
            migrationBuilder.Sql("TRUNCATE TABLE dbo.BankBusinessBalances");
            
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankBusinessBalanceSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankBusinessBalances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankBusinessBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankBusinessBalances");
        }
    }
}
