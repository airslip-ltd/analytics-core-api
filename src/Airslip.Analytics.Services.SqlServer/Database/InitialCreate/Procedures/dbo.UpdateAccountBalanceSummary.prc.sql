create or alter proc dbo.UpdateAccountBalanceSummary(
    @EntityId as varchar(33),
    @AirslipUserType as int
)
as
begin
merge into BankAccountBalanceSummaries as abs
    using
        (
            select x.AccountId, x.EntityId, x.AirslipUserType, x.UpdatedOn, x.Balance, x.TimeStamp, x.Currency
            from (select distinct AccountId
                  from BankAccountBalanceSnapshots
                  where EntityId = @EntityId
                    and AirslipUserType = @AirslipUserType) c
                cross apply (select top 1 *
                                  from AccountBalanceSnapshots t
                                  where t.EntityId = @EntityId
                                    and t.AirslipUserType = @AirslipUserType
                                    and t.AccountId = c.AccountId
                                  order by TimeStamp desc) x) y
    on
                abs.AccountId = y.AccountId and abs.EntityId = y.EntityId and abs.AirslipUserType = y.AirslipUserType
    when matched then
        update
            set abs.Movement  = dbo.calcMovement(y.Balance, abs.Balance),
                abs.UpdatedOn = y.UpdatedOn,
                abs.Balance   = y.Balance,
                abs.TimeStamp = y.TimeStamp
    when not matched then
        insert
            (AccountId, EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency, Movement)
            values (y.AccountId, y.EntityId, y.AirslipUserType, y.UpdatedOn, y.Balance, y.TimeStamp, y.Currency, 0);

merge into BankBusinessBalances as bb
    using
        (
            select x.EntityId,
                   x.AirslipUserType,
                   MAX(x.UpdatedOn) as UpdatedOn,
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
                order by TimeStamp desc) x
            group by x.EntityId, x.AirslipUserType, x.Currency) y
    on
                bb.EntityId = y.EntityId and bb.AirslipUserType = y.AirslipUserType
    when matched then
        update
            set bb.Movement  = dbo.calcMovement(y.Balance, bb.Balance),
                bb.UpdatedOn = y.UpdatedOn,
                bb.Balance   = y.Balance,
                bb.TimeStamp = y.TimeStamp
    when not matched then
        insert
            (EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency, Movement)
            values (y.EntityId, y.AirslipUserType, y.UpdatedOn, y.Balance, y.TimeStamp, y.Currency, 0);

end
