CREATE or alter PROCEDURE dbo.GetTotalSalesSnapshot(
    @DayRange as int,
    @StatRange as int,
    @ViewerEntityId as nvarchar(50),
    @ViewerAirslipUserType as int,
    @OwnerEntityId as nvarchar(50),
    @OwnerAirslipUserType as int,
    @StartDate as date = null,
    @IntegrationId as nvarchar(50) = null
)
AS
BEGIN

    select dr.EndCalendarDate             as MetricDate,
           dr.Year                        as Year,
           dr.Month                       as Month,
           dr.Day                         as Day,
           SUM(isnull(mms.TotalSales, 0)) as Balance
    from dbo.getMetricDateRange(@DayRange, @StatRange, @StartDate) as dr
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
                             on mms.EntityId = rd.OwnerEntityId AND mms.AirslipUserType = rd.OwnerAirslipUserType
                                 AND mms.MetricDate > dr.StartCalendarDate and mms.MetricDate <= dr.EndCalendarDate
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)

    group by dr.EndCalendarDate, dr.Year, dr.Month, dr.Day
    order by dr.EndCalendarDate DESC

end