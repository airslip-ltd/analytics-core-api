create or alter proc dbo.CreateAccountBalanceSnapshot
(
    @EntityId as varchar(33),
    @AirslipUserType as int,
    @Id as varchar(33)
)
AS
insert into BankAccountBalanceSnapshots
(Id, EntityId, AirslipUserType, AccountId, Balance, TimeStamp, Currency, UpdatedOn)
select dbo.getId(), AB.EntityId, AB.AirslipUserType, AB.AccountId,
       case AB.BalanceStatus when 1 then AB.Balance * -1 else AB.Balance end, AB.TimeStamp, AB.Currency,
       dbo.round5min(DATEADD(ss, AB.TimeStamp/1000, '19700101'))
from BankAccounts as a
         join BankAccountBalances AB on a.Id = AB.AccountId
where a.EntityId = @EntityId
  and a.AirslipUserType = @AirslipUserType
  and ab.Id = @Id
order by AB.TimeStamp DESC