create or alter proc dbo.CreateBusinessBalanceSnapshot(
    @Id as nvarchar(50)
)
AS

Declare @UpdatedOn as datetime2, 
    @AccountType as int,     
    @EntityId as nvarchar(50),
    @AirslipUserType as int

select @UpdatedOn = dbo.round5min(DATEADD(ss, bab.TimeStamp / 1000, '19700101')), 
       @AccountType = IAD.AccountType,
       @EntityId = bab.EntityId, @AirslipUserType = bab.AirslipUserType
from BankAccountBalances as bab
         join IntegrationAccountDetails IAD on bab.IntegrationId = IAD.IntegrationId
where bab.Id = @Id

insert into BankBusinessBalanceSnapshots
(Id, EntityId, AirslipUserType, UpdatedOn, AccountType, Balance, TimeStamp, Currency)
select dbo.getId(),
       x.EntityId,
       x.AirslipUserType,
       @UpdatedOn,
       x.AccountType,
       SUM(x.Balance)   as Balance,
       MAX(x.TimeStamp) as TimeStamp,
       x.Currency
from (select distinct IntegrationId
      from BankAccountBalanceSnapshots
      where EntityId = @EntityId
        and AirslipUserType = @AirslipUserType
        and AccountType = @AccountType) c
         cross apply (select top 1 *
                      from BankAccountBalanceSnapshots t
                      where t.EntityId = @EntityId
                        and t.AirslipUserType = @AirslipUserType
                        and t.IntegrationId = c.IntegrationId
                        and t.UpdatedOn = @UpdatedOn
                      order by TimeStamp desc) x
group by x.EntityId, x.AirslipUserType, x.Currency, x.AccountType
