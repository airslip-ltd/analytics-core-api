using Airslip.Analytics.Services.SqlServer.Extensions;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airslip.Analytics.Services.SqlServer.Migrations
{
    public partial class BankAccountMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "BankTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "BankTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "BankTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccountMetricSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "dbo.getId()"),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirslipUserType = table.Column<int>(type: "int", nullable: false),
                    MetricDate = table.Column<DateTime>(type: "date", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: true),
                    TotalTransaction = table.Column<long>(type: "bigint", nullable: false),
                    TransactionCount = table.Column<int>(type: "int", nullable: false),
                    TotalCredit = table.Column<long>(type: "bigint", nullable: false),
                    CreditCount = table.Column<int>(type: "int", nullable: false),
                    TotalDebit = table.Column<long>(type: "bigint", nullable: false),
                    DebitCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountMetricSnapshots_Id", x => x.Id);
                });
            
            // Migrate data
            migrationBuilder.Sql(@"
                update BankTransactions
                set     Year = datepart(year, dbo.round5min(DATEADD(ss, CapturedDate / 1000, '19700101'))),
                        Month = datepart(month, dbo.round5min(DATEADD(ss, CapturedDate / 1000, '19700101'))),
                        Day = datepart(day, dbo.round5min(DATEADD(ss, CapturedDate / 1000, '19700101')))");
            
            migrationBuilder.Sql(@"merge into BankAccountMetricSnapshots as bams
using
    (
        select count(*)                                                as TransactionCount,
               SUM(bt.Amount)                                          as TotalTransaction,
               SUM(case when bt.Amount < 0 then bt.Amount else 0 end)  as TotalDebit,
               SUM(case when bt.Amount < 0 then 1 else 0 end)          as DebitCount,
               SUM(case when bt.Amount >= 0 then bt.Amount else 0 end) as TotalCredit,
               SUM(case when bt.Amount >= 0 then 1 else 0 end)         as CreditCount,
               bt.EntityId,
               bt.AirslipUserType,
               bt.Year,
               bt.Month,
               bt.Day,
               bt.AccountId
        from BankTransactions as bt
        group by bt.EntityId, bt.AirslipUserType, bt.AccountId, bt.Year, bt.Month, bt.Day) as y
on bams.EntityId = y.EntityId
    and bams.AirslipUserType = y.AirslipUserType
    and bams.Day = y.Day
    and bams.Month = y.Month
    and bams.Year = y.Year
    and bams.AccountId = y.AccountId
when matched then
    update
    set bams.TotalTransaction = y.TotalTransaction,
        bams.TransactionCount = y.TransactionCount,
        bams.TotalCredit      = y.TotalCredit,
        bams.CreditCount      = y.CreditCount,
        bams.TotalDebit       = y.TotalDebit,
        bams.DebitCount       = y.DebitCount
when not matched then
    insert (AccountId, EntityId, AirslipUserType, MetricDate, Year, Month, Day, TotalTransaction,
            TransactionCount, TotalCredit, CreditCount, TotalDebit, DebitCount)
    VALUES (y.AccountId, y.EntityId, y.AirslipUserType, datefromparts(y.Year, y.Month, y.Day), y.Year, y.Month, y.Day,
            y.TotalTransaction,
            y.TransactionCount, y.TotalCredit, y.CreditCount, y.TotalDebit, y.DebitCount);
");
            
            migrationBuilder.AddSqlFiles(nameof(BankAccountMetrics));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountMetricSnapshots");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "BankTransactions");
        }
    }
}
