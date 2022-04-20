

#nullable disable

using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;
namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AccountTypePart3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE dbo.BankAccountBalanceSnapshots");
            
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankAccountBalanceSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAccountBalanceSnapshots");
        }
    }
}
