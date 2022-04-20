

#nullable disable

using Airslip.Analytics.Services.SqlServer.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Migrations;
namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.CreateIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_Currency",
                table: "BankBusinessBalances",
                columns: new[] { "EntityId", "AirslipUserType", "AccountType", "Currency" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_AccountType",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "AccountType" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_Currency",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "IntegrationId", "UpdatedOn", "TimeStamp", "AccountType", "Currency" })
                .Annotation("SqlServer:Include", new[] { "Balance" });
            
            migrationBuilder.AddSqlFiles();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_Currency",
                table: "BankBusinessBalances");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_AccountType",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_Currency",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "IntegrationId", "UpdatedOn", "TimeStamp" })
                .Annotation("SqlServer:Include", new[] { "Balance", "Currency" });
        }
    }
}
