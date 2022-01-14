create or alter proc dbo.CreateBusinessBalanceSnapshot
(
    @EntityId as varchar(33),
    @AirslipUserType as int
)
AS
insert into BusinessBalanceSnapshots
    (Id, EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency)
select dbo.getId(),
       x.EntityId,
       x.AirslipUserType,
       MAX(x.UpdatedOn) as UpdatedOn,
       SUM(x.Balance)   as Balance,
       MAX(x.TimeStamp) as TimeStamp,
       x.Currency
from (select distinct AccountId
    from AccountBalanceSnapshots
    where EntityId = @EntityId and AirslipUserType = @AirslipUserType) c
    cross apply (select top 1 *
    from AccountBalanceSnapshots t
    where t.EntityId = @EntityId
    and t.AirslipUserType = @AirslipUserType
    and t.AccountId = c.AccountId
    order by TimeStamp desc) x
group by x.EntityId, x.AirslipUserType, x.Currency
