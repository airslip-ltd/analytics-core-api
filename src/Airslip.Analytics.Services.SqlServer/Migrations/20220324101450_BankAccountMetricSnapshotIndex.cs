using Airslip.Analytics.Services.SqlServer.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class BankAccountMetricSnapshotIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BankAccountMetricSnapshots_EntityId_AirslipUserType_Day_Month_Year_IntegrationId",
                table: "BankAccountMetricSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "Day", "Month", "Year", "IntegrationId" });
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountMetricSnapshots_EntityId_AirslipUserType_Day_Month_Year_IntegrationId",
                table: "BankAccountMetricSnapshots");
        }
    }
}
