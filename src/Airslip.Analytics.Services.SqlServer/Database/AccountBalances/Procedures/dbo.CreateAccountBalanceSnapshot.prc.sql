create or alter proc dbo.CreateAccountBalanceSnapshot
(
    @EntityId as varchar(33),
    @AirslipUserType as int,
    @Id as varchar(33)
)
AS
insert into AccountBalanceSnapshots
(Id, EntityId, AirslipUserType, AccountId, Balance, TimeStamp, Currency, UpdatedOn)
select dbo.getId(), AB.EntityId, AB.AirslipUserType, AB.AccountId, AB.Balance, AB.TimeStamp, AB.Currency,
       dbo.round5min(DATEADD(ss, AB.TimeStamp/1000, '19700101'))
from Accounts as a
         join AccountBalances AB on a.Id = AB.AccountId
where a.EntityId = @EntityId
  and a.AirslipUserType = @AirslipUserType
  and ab.Id = @Id
order by AB.TimeStamp DESC