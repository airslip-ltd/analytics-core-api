CREATE OR ALTER PROCEDURE dbo.GetRevenueAndRefundsByYear(
    @Year as int,
    @ViewerEntityId as nvarchar(50),
    @ViewerAirslipUserType as int,
    @OwnerEntityId as nvarchar(50),
    @OwnerAirslipUserType as int,
    @IntegrationId as nvarchar(50) = null,
    @CurrencyCode as nvarchar(3)
)
AS
BEGIN
    select m.ROWNO as Month,
            mms.Year as Year,
           SUM(isnull(mms.TotalSales, 0))   as TotalSales,
           SUM(isnull(mms.TotalRefunds, 0)) as TotalRefunds
    from dbo.getYearMonths(1, 12) as m
             left outer join RelationshipDetails as rd
                             on rd.ViewerEntityId = @ViewerEntityId AND
                                rd.ViewerAirslipUserType = @ViewerAirslipUserType and
                                rd.OwnerEntityId = @OwnerEntityId and
                                rd.OwnerAirslipUserType = @OwnerAirslipUserType and
                                rd.PermissionType = 'Commerce'
                                 and rd.Allowed = 1
             left outer join RelationshipHeaders as rh
                             on rh.Id = rd.RelationshipHeaderId and rh.EntityStatus = 1
             left outer join MerchantAccountMetricSnapshots as mms
                             on mms.Month = m.ROWNO and mms.Year = @Year and mms.EntityId = rd.OwnerEntityId
                                 AND mms.AirslipUserType = rd.OwnerAirslipUserType
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
                                 and mms.CurrencyCode = @CurrencyCode
    group by m.ROWNO, mms.Year
    ORDER BY mms.year asc, m.ROWNO asc
END