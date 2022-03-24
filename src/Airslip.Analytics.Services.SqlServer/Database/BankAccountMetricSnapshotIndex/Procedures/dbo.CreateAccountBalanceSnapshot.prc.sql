create or alter proc dbo.CreateAccountBalanceSnapshot
(
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @Id as nvarchar(50)
)
AS
insert into BankAccountBalanceSnapshots
(Id, EntityId, AirslipUserType, IntegrationId, Balance, TimeStamp, Currency, UpdatedOn)
select dbo.getId(), AB.EntityId, AB.AirslipUserType, AB.IntegrationId,
       case AB.BalanceStatus when 1 then AB.Balance * -1 else AB.Balance end, AB.TimeStamp, AB.Currency,
       dbo.round5min(DATEADD(ss, AB.TimeStamp/1000, '19700101'))
from Integrations as a
         join BankAccountBalances AB on a.Id = AB.IntegrationId
where a.EntityId = @EntityId
  and a.AirslipUserType = @AirslipUserType
  and ab.Id = @Id
order by AB.TimeStamp DESC