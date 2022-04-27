create or alter proc dbo.CreateAccountBalanceSnapshot
(
    @Id as nvarchar(50)
)
AS
insert into BankAccountBalanceSnapshots
(Id, EntityId, AirslipUserType, IntegrationId, Balance, TimeStamp, CurrencyCode, UpdatedOn, AccountType)
select dbo.getId(), AB.EntityId, AB.AirslipUserType, AB.IntegrationId,
       case AB.BalanceStatus when 1 then AB.Balance * -1 else AB.Balance end, AB.TimeStamp, AB.CurrencyCode,
       dbo.round5min(DATEADD(ss, AB.TimeStamp/1000, '19700101')),
       IAD.AccountType
from BankAccountBalances AB
         join Integrations as a on a.Id = AB.IntegrationId
        join IntegrationAccountDetails IAD on a.Id = IAD.IntegrationId
where ab.Id = @Id
order by AB.TimeStamp DESC