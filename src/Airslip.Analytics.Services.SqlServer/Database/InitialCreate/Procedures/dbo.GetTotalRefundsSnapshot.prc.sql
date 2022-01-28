CREATE or alter PROCEDURE dbo.GetTotalRefundsSnapshot(
    @DayRange as int,
    @StatRange as int,
    @EntityId as varchar(33),
    @AirslipUserType as int,
    @StartDate as date = null
)
AS
BEGIN
    select dr.EndCalendarDate             as MetricDate,
           dr.Year                        as Year,
           dr.Month                       as Month,
           dr.Day                         as Day,
           SUM(isnull(mms.TotalRefunds, 0)) as Balance
    from dbo.getMetricDateRange(@DayRange, @StatRange, @StartDate) as dr
             left outer join MerchantMetricSnapshots as mms
                             on mms.EntityId = @EntityId AND mms.AirslipUserType = @AirslipUserType
                                 AND mms.MetricDate > dr.StartCalendarDate and mms.MetricDate <= dr.EndCalendarDate
    group by dr.EndCalendarDate, dr.Year, dr.Month, dr.Day
    order by dr.EndCalendarDate DESC
end
