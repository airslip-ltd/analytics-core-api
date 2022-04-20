create or alter proc dbo.UpdateBusinessBalanceSummary(
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
                      from BankBusinessBalances
                      where EntityId = @EntityId
                        and AirslipUserType = @AirslipUserType
                        and AccountType = @AccountType
                        and Currency = @Currency)
            insert into BankBusinessBalances
            (EntityId, AirslipUserType, UpdatedOn, Balance, TimeStamp, Currency, Movement, AccountType)
            values (@EntityId, @AirslipUserType, datefromparts(2000, 1, 1), 0, 0, @Currency, 0, @AccountType)

        update babs
        set babs.Movement  = dbo.calcMovement(y.Balance, babs.Balance),
            babs.UpdatedOn = y.UpdatedOn,
            babs.Balance   = y.Balance,
            babs.TimeStamp = y.TimeStamp
        from BankBusinessBalances as babs
                 join (select x.EntityId,
                              x.AirslipUserType,
                              MAX(x.UpdatedOn) as UpdatedOn,
                              SUM(x.Balance)   as Balance,
                              MAX(x.TimeStamp) as TimeStamp,
                              x.Currency,
                              x.AccountType
                       from (select distinct IntegrationId
                             from BankAccountBalanceSnapshots
                             where EntityId = @EntityId
                               and AirslipUserType = @AirslipUserType
                               and AccountType = @AccountType
                               and Currency = @Currency) c
                                cross apply (select top 1 *
                                             from BankAccountBalanceSnapshots t
                                             where t.EntityId = @EntityId
                                               and t.AirslipUserType = @AirslipUserType
                                               and t.IntegrationId = c.IntegrationId
                                             order by TimeStamp desc) x
                       group by x.EntityId, x.AirslipUserType, x.Currency, x.AccountType) y 
                     on babs.AccountType = y.AccountType
            and babs.EntityId = y.EntityId
            and babs.AirslipUserType = y.AirslipUserType
            and babs.Currency = y.Currency
            and babs.AccountType = y.AccountType

    end
