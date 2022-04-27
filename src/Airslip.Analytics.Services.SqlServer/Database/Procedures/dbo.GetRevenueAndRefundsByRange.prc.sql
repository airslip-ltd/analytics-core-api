CREATE OR ALTER PROCEDURE dbo.GetRevenueAndRefundsByRange(
    @Start as date,
    @End as date,
    @ViewerEntityId as nvarchar(50),
    @ViewerAirslipUserType as int,
    @OwnerEntityId as nvarchar(50),
    @OwnerAirslipUserType as int,
    @IntegrationId as nvarchar(50) = null,
    @CurrencyCode as nvarchar(3)
)
AS
BEGIN
    select m.Month as Month,
           m.Year as Year,
           SUM(isnull(mms.TotalSales, 0))   as TotalSales,
           SUM(isnull(mms.TotalRefunds, 0)) as TotalRefunds
    from dbo.getReportMonthsAndYears(@Start, @End) as m
             left outer join RelationshipDetails as rd
                             on rd.ViewerEntityId = @ViewerEntityId AND
                                rd.ViewerAirslipUserType = @ViewerAirslipUserType and
                                rd.OwnerEntityId = @OwnerEntityId and
                                rd.OwnerAirslipUserType = @OwnerAirslipUserType and
                                rd.PermissionType = 'Commerce' and
                                rd.Allowed = 1
             left outer join MerchantAccountMetricSnapshots as mms
                             on mms.Month = m.Month and mms.Year = m.Year and mms.EntityId = rd.OwnerEntityId
                                 AND mms.AirslipUserType = rd.OwnerAirslipUserType
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
                                 and mms.CurrencyCode = @CurrencyCode
    group by m.Month, m.Year
    ORDER BY m.year asc, m.month asc
END


