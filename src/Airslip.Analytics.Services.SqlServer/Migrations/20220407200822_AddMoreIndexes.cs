using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AddMoreIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType",
                table: "MerchantTransactions",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType" });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantTransactions",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId" });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantAccountMetricSnapshots",
                columns: new[] { "Day", "Month", "Year", "EntityId", "AirslipUserType", "IntegrationId" });
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType",
                table: "MerchantTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MerchantTransactions_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MerchantAccountMetricSnapshots_Day_Month_Year_EntityId_AirslipUserType_IntegrationId",
                table: "MerchantAccountMetricSnapshots");
        }
    }
}
