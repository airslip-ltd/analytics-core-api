using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class LongRunningQueries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantAccountMetricSnapshots");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId_CurrencyCode",
                table: "MerchantTransactions",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId", "CurrencyCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId_CurrencyCode",
                table: "MerchantAccountMetricSnapshots",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId", "CurrencyCode" });
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId_CurrencyCode",
                table: "MerchantTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId_CurrencyCode",
                table: "MerchantAccountMetricSnapshots");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantTransactions",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId" });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantAccountMetricSnapshots",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId" });
        }
    }
}
