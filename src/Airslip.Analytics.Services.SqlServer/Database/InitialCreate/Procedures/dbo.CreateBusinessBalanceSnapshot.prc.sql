create or alter proc dbo.CreateBusinessBalanceSnapshot(
    @EntityId as varchar(33),
    @AirslipUserType as int,
    @Id as varchar(33)
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
from (select distinct AccountId
    from BankAccountBalanceSnapshots
    where EntityId = @EntityId
    and AirslipUserType = @AirslipUserType) c
    cross apply (select top 1 *
    from BankAccountBalanceSnapshots t
    where t.EntityId = @EntityId
    and t.AirslipUserType = @AirslipUserType
    and t.AccountId = c.AccountId
    and t.UpdatedOn = @UpdatedOn
    order by TimeStamp desc) x
group by x.EntityId, x.AirslipUserType, x.Currency
