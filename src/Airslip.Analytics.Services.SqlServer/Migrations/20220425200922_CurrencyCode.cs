using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class CurrencyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "MerchantMetricSnapshots",
                type: "nvarchar (3)",
                nullable: true,
                defaultValue: "GBP");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "MerchantAccountMetricSnapshots",
                type: "nvarchar (3)",
                nullable: true,
                defaultValue: "GBP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "MerchantMetricSnapshots");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "MerchantAccountMetricSnapshots");
        }
    }
}
