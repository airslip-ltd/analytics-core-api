CREATE OR ALTER PROCEDURE dbo.GetRevenueAndRefundsByYear(
    @Year as int,
    @EntityId as varchar(33),
    @AirslipUserType as int
)
AS
BEGIN
    select m.ROWNO as Month, SUM(mms.TotalSales) as TotalSales, SUM(mms.TotalRefunds) as TotalRefunds
    from dbo.getYearMonths(1, 12) as m
             left outer join MerchantMetricSnapshots as mms
                             on mms.Month = m.ROWNO and mms.Year = @Year and mms.EntityId = @EntityId
                                 and mms.AirslipUserType = @AirslipUserType
    group by m.ROWNO
END