CREATE OR ALTER PROCEDURE dbo.GetRevenueAndRefundsByYear(
    @Year as int,
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @IntegrationId as nvarchar(50) = null
    )
    AS
BEGIN
select m.ROWNO                          as Month,
           SUM(isnull(mms.TotalSales, 0))   as TotalSales,
           SUM(isnull(mms.TotalRefunds, 0)) as TotalRefunds
from dbo.getYearMonths(1, 12) as m
    left outer join MerchantAccountMetricSnapshots as mms
on mms.Month = m.ROWNO and mms.Year = @Year and mms.EntityId = @EntityId
    and mms.AirslipUserType = @AirslipUserType
    and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
group by m.ROWNO
END