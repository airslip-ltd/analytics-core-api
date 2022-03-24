create or alter proc dbo.CreateBusinessBalanceSnapshot(
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @Id as nvarchar(50)
)
AS

Declare @UpdatedOn as datetime2

select @UpdatedOn = dbo.round5min(DATEADD(ss, TimeStamp / 1000, '19700101'))
from BankAccountBalances
where Id = @Id

    insert into BankBusinessBalanceSnapshots
(Id, EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency)
select dbo.getId(),
       x.EntityId,
       x.AirslipUserType,
       @UpdatedOn,
       SUM(x.Balance)   as Balance,
       MAX(x.TimeStamp) as TimeStamp,
       x.Currency
from (select distinct IntegrationId
    from BankAccountBalanceSnapshots
    where EntityId = @EntityId
    and AirslipUserType = @AirslipUserType) c
    cross apply (select top 1 *
    from BankAccountBalanceSnapshots t
    where t.EntityId = @EntityId
    and t.AirslipUserType = @AirslipUserType
    and t.IntegrationId = c.IntegrationId
    and t.UpdatedOn = @UpdatedOn
    order by TimeStamp desc) x
group by x.EntityId, x.AirslipUserType, x.Currency
