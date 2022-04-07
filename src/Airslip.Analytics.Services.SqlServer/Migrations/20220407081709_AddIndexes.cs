using Airslip.Analytics.Services.SqlServer.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "IntegrationId", "UpdatedOn", "TimeStamp" })
                .Annotation("SqlServer:Include", new[] { "Balance", "Currency" });
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp",
                table: "BankAccountBalanceSnapshots");
        }
    }
}
