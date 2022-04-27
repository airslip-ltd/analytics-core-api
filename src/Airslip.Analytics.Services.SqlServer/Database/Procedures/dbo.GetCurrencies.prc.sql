CREATE OR ALTER PROCEDURE dbo.GetCurrencies(
    @ViewerEntityId as nvarchar(50),
    @ViewerAirslipUserType as int,
    @OwnerEntityId as nvarchar(50),
    @OwnerAirslipUserType as int
)
AS
BEGIN

    select distinct bams.CurrencyCode
    from RelationshipDetails as rd
             join BankAccountMetricSnapshots as bams
                  on bams.EntityId = rd.OwnerEntityId
                      AND bams.AirslipUserType = rd.OwnerAirslipUserType
    where rd.ViewerEntityId = @ViewerEntityId
      AND rd.ViewerAirslipUserType = @ViewerAirslipUserType
      and rd.OwnerEntityId = @OwnerEntityId
      and rd.OwnerAirslipUserType = @OwnerAirslipUserType
      and rd.PermissionType = 'Banking'
      and rd.Allowed = 1
    union
    select distinct mams.CurrencyCode
    from RelationshipDetails as rd
             join MerchantAccountMetricSnapshots as mams
                  on mams.EntityId = rd.OwnerEntityId
                      AND mams.AirslipUserType = rd.OwnerAirslipUserType
    where rd.ViewerEntityId = @ViewerEntityId
      AND rd.ViewerAirslipUserType = @ViewerAirslipUserType
      and rd.OwnerEntityId = @OwnerEntityId
      and rd.OwnerAirslipUserType = @OwnerAirslipUserType
      and rd.PermissionType = 'Commerce'
      and rd.Allowed = 1
END