using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AccountTypePart4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE dbo.BankAccountMetricSnapshots");

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankAccountMetricSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAccountMetricSnapshots");
        }
    }
}
