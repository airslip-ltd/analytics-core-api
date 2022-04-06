CREATE or alter proc dbo.CreateBankAccountMetricSnapshots(
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @Id as nvarchar(50)
)
AS
Declare @Year as int, @Month as int, @Day as int

select @Year = Year, @Month = Month, @Day = Day
from BankTransactions
where Id = @Id
    merge into BankAccountMetricSnapshots with (HOLDLOCK) as bams
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
                   bt.IntegrationId
            from BankTransactions as bt
            where bt.Day = @Day
              and bt.Month = @Month
              and bt.Year = @Year
              and bt.EntityId = @EntityId
              and bt.AirslipUserType = @AirslipUserType
            group by bt.EntityId, bt.AirslipUserType, bt.IntegrationId, bt.Year, bt.Month, bt.Day) as y
    on bams.EntityId = y.EntityId
        and bams.AirslipUserType = y.AirslipUserType
        and bams.Day = y.Day
        and bams.Month = y.Month
        and bams.Year = y.Year
        and bams.IntegrationId = y.IntegrationId
    when matched then
        update
        set bams.TotalTransaction = y.TotalTransaction,
            bams.TransactionCount = y.TransactionCount,
            bams.TotalCredit      = y.TotalCredit,
            bams.CreditCount      = y.CreditCount,
            bams.TotalDebit       = y.TotalDebit,
            bams.DebitCount       = y.DebitCount
    when not matched then
        insert (IntegrationId, EntityId, AirslipUserType, MetricDate, Year, Month, Day, TotalTransaction,
                TransactionCount, TotalCredit, CreditCount, TotalDebit, DebitCount)
        VALUES (y.IntegrationId, y.EntityId, y.AirslipUserType, datefromparts(@Year, @Month, @Day), y.Year, y.Month, y.Day,
                y.TotalTransaction,
                y.TransactionCount, y.TotalCredit, y.CreditCount, y.TotalDebit, y.DebitCount);
