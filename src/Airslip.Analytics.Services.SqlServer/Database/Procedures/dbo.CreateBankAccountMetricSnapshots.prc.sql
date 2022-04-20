CREATE or alter proc dbo.CreateBankAccountMetricSnapshots(
    @Id as nvarchar(50)
)
AS
Declare
    @Year as     int, @Month as int, @Day as int, @AccountType as int,
    @EntityId as nvarchar(50), @AirslipUserType as int, @IntegrationId as nvarchar(50)

select @Year = Year,
       @Month = Month,
       @Day = Day,
       @EntityId = EntityId,
       @AirslipUserType = AirslipUserType,
       @IntegrationId = bt.IntegrationId,
       @AccountType = IAD.AccountType
from BankTransactions as bt
join IntegrationAccountDetails IAD on bt.IntegrationId = IAD.IntegrationId
where bt.Id = @Id
    
    if not exists(select *
                  from BankAccountMetricSnapshots
                  where EntityId = @EntityId
                    and AirslipUserType = @AirslipUserType
                    and Day = @Day
                    and Month = @Month
                    and Year = @Year
                    and IntegrationId = @IntegrationId)
        insert into BankAccountMetricSnapshots
        (IntegrationId, AccountType, EntityId, AirslipUserType, MetricDate, Year, Month, Day, TotalTransaction,
         TransactionCount, TotalCredit, CreditCount, TotalDebit, DebitCount)
        VALUES (@IntegrationId, @AccountType, @EntityId, @AirslipUserType, datefromparts(@Year, @Month, @Day), @Year,
                @Month, @Day,
                0, 0, 0, 0, 0, 0)

update bams
set bams.TotalTransaction = y.TotalTransaction,
    bams.TransactionCount = y.TransactionCount,
    bams.TotalCredit      = y.TotalCredit,
    bams.CreditCount      = y.CreditCount,
    bams.TotalDebit       = y.TotalDebit,
    bams.DebitCount       = y.DebitCount
from BankAccountMetricSnapshots as bams
         join (select count(*)                                                as TransactionCount,
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
                 and bt.IntegrationId = @IntegrationId
               group by bt.EntityId, bt.AirslipUserType, bt.IntegrationId, bt.Year, bt.Month, bt.Day) as y
              on bams.IntegrationId = y.IntegrationId
where bams.EntityId = y.EntityId
  and bams.AirslipUserType = y.AirslipUserType
  and bams.Day = y.Day
  and bams.Month = y.Month
  and bams.Year = y.Year
  and bams.IntegrationId = y.IntegrationId


