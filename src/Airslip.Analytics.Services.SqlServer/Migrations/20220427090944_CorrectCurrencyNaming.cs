using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class CorrectCurrencyNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_Currency",
                table: "BankBusinessBalances");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_Currency",
                table: "BankAccountBalanceSnapshots");
            
            migrationBuilder.Sql("Update MerchantTransactions set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantTransactions",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (5)",
                oldNullable: true);

            migrationBuilder.Sql("Update MerchantMetricSnapshots set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantMetricSnapshots",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldNullable: true,
                oldDefaultValue: "GBP");

            migrationBuilder.Sql("Update MerchantAccountMetricSnapshots set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantAccountMetricSnapshots",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldNullable: true,
                oldDefaultValue: "GBP");

            migrationBuilder.Sql("Update IntegrationAccountDetails set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "IntegrationAccountDetails",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (5)");

            migrationBuilder.Sql("Update BankTransactions set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankTransactions",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "BankBusinessBalanceSnapshots",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP");
            migrationBuilder.Sql("Update BankBusinessBalanceSnapshots set CurrencyCode = Currency where Currency is not null");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "BankBusinessBalances",
                type: "nvarchar(3)",
                nullable: false,
                defaultValue: "GBP");
            migrationBuilder.Sql("Update BankBusinessBalances set CurrencyCode = Currency where Currency is not null");
            
            migrationBuilder.Sql("Update BankAccountMetricSnapshots set CurrencyCode = 'GBP' where CurrencyCode is null");
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankAccountMetricSnapshots",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldNullable: true,
                oldDefaultValue: "GBP");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "BankAccountBalanceSummaries",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP");
            migrationBuilder.Sql("Update BankAccountBalanceSummaries set CurrencyCode = Currency where Currency is not null");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "BankAccountBalanceSnapshots",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP");
            migrationBuilder.Sql("Update BankAccountBalanceSnapshots set CurrencyCode = Currency where Currency is not null");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "BankAccountBalances",
                type: "nvarchar (3)",
                nullable: false,
                defaultValue: "GBP");
            migrationBuilder.Sql("Update BankAccountBalances set CurrencyCode = Currency where Currency is not null");

            migrationBuilder.CreateIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_CurrencyCode",
                table: "BankBusinessBalances",
                columns: new[] { "EntityId", "AirslipUserType", "AccountType", "CurrencyCode" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_CurrencyCode",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "IntegrationId", "UpdatedOn", "TimeStamp", "AccountType", "CurrencyCode" })
                .Annotation("SqlServer:Include", new[] { "Balance" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_CurrencyCode",
                table: "BankBusinessBalances");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_CurrencyCode",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "BankBusinessBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "BankBusinessBalances");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "BankAccountBalanceSummaries");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "BankAccountBalanceSnapshots");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "BankAccountBalances");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantTransactions",
                type: "nvarchar (5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantMetricSnapshots",
                type: "nvarchar (3)",
                nullable: true,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "MerchantAccountMetricSnapshots",
                type: "nvarchar (3)",
                nullable: true,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "IntegrationAccountDetails",
                type: "nvarchar (5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankTransactions",
                type: "nvarchar (5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "BankAccountMetricSnapshots",
                type: "nvarchar (3)",
                nullable: true,
                defaultValue: "GBP",
                oldClrType: typeof(string),
                oldType: "nvarchar (3)",
                oldDefaultValue: "GBP");

            migrationBuilder.CreateIndex(
                name: "IX_BankBusinessBalances_EntityId_AirslipUserType_AccountType_Currency",
                table: "BankBusinessBalances",
                columns: new[] { "EntityId", "AirslipUserType", "AccountType", "Currency" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBalanceSnapshots_EntityId_AirslipUserType_IntegrationId_UpdatedOn_TimeStamp_AccountType_Currency",
                table: "BankAccountBalanceSnapshots",
                columns: new[] { "EntityId", "AirslipUserType", "IntegrationId", "UpdatedOn", "TimeStamp", "AccountType", "Currency" })
                .Annotation("SqlServer:Include", new[] { "Balance" });
        }
    }
}
