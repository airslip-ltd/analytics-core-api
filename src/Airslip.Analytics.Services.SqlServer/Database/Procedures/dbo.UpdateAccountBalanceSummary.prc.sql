create or alter proc dbo.UpdateAccountBalanceSummary(
    @EntityId as nvarchar(50),
    @AirslipUserType as int
)
as
begin
    merge into BankAccountBalanceSummaries with (HOLDLOCK) as abs
    using
        (
            select x.IntegrationId, x.EntityId, x.AirslipUserType, x.UpdatedOn, x.Balance, x.TimeStamp, x.Currency
            from (select distinct IntegrationId
                  from BankAccountBalanceSnapshots
                  where EntityId = @EntityId
                    and AirslipUserType = @AirslipUserType) c
                     cross apply (select top 1 *
                                  from BankAccountBalanceSnapshots t
                                  where t.EntityId = @EntityId
                                    and t.AirslipUserType = @AirslipUserType
                                    and t.IntegrationId = c.IntegrationId
                                  order by TimeStamp desc) x) y
    on abs.IntegrationId = y.IntegrationId
        and abs.EntityId = y.EntityId
        and abs.AirslipUserType = y.AirslipUserType
        and abs.Currency = y.Currency
    when matched then
        update
        set abs.Movement  = dbo.calcMovement(y.Balance, abs.Balance),
            abs.UpdatedOn = y.UpdatedOn,
            abs.Balance   = y.Balance,
            abs.TimeStamp = y.TimeStamp
    when not matched then
        insert
        (IntegrationId, EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency, Movement)
        values (y.IntegrationId, y.EntityId, y.AirslipUserType, y.UpdatedOn, y.Balance, y.TimeStamp, y.Currency, 0);

    merge into BankBusinessBalances with (HOLDLOCK) as bb
    using
        (
            select x.EntityId,
                   x.AirslipUserType,
                   MAX(x.UpdatedOn) as UpdatedOn,
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
                                  order by TimeStamp desc) x
            group by x.EntityId, x.AirslipUserType, x.Currency) y
    on bb.EntityId = y.EntityId
        and bb.AirslipUserType = y.AirslipUserType
        and bb.Currency = y.Currency
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
