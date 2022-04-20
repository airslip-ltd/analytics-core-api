create or alter proc dbo.UpdateAccountBalanceSummary(
    @Id as nvarchar(50)
)
as
begin

    Declare @AccountType as int,
        @IntegrationId as nvarchar(50),
        @EntityId as nvarchar(50),
        @AirslipUserType as int,
        @Currency as varchar(5)

    select @AccountType = IAD.AccountType,
           @IntegrationId = bab.IntegrationId,
           @EntityId = bab.EntityId,
           @AirslipUserType = bab.AirslipUserType,
           @Currency = bab.Currency
    from BankAccountBalances as bab
             join IntegrationAccountDetails IAD on bab.IntegrationId = IAD.IntegrationId
    where bab.Id = @Id

    if not exists(select *
                  from BankAccountBalanceSummaries
                  where IntegrationId = @IntegrationId
                    and EntityId = @EntityId
                    and AirslipUserType = @AirslipUserType
                    and Currency = @Currency)
        insert into BankAccountBalanceSummaries
        (IntegrationId, EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency, Movement, AccountType)
        values (@IntegrationId, @EntityId, @AirslipUserType, datefromparts(2000, 1, 1), 0, 0, @Currency, 0,
                @AccountType)

    update babs
    set babs.Movement  = dbo.calcMovement(y.Balance, babs.Balance),
        babs.UpdatedOn = y.UpdatedOn,
        babs.Balance   = y.Balance,
        babs.TimeStamp = y.TimeStamp
    from BankAccountBalanceSummaries as babs
             join (select top 1 t.IntegrationId,
                                t.EntityId,
                                t.AirslipUserType,
                                t.UpdatedOn,
                                t.Balance,
                                t.TimeStamp,
                                t.Currency
                   from BankAccountBalanceSnapshots t
                   where t.EntityId = @EntityId
                     and t.AirslipUserType = @AirslipUserType
                     and t.IntegrationId = @IntegrationId
                   order by TimeStamp desc) y on babs.IntegrationId = y.IntegrationId
        and babs.EntityId = y.EntityId
        and babs.AirslipUserType = y.AirslipUserType
        and babs.Currency = y.Currency
        and babs.IntegrationId = y.IntegrationId

end
