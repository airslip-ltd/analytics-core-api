CREATE OR ALTER PROCEDURE dbo.GetCreditsAndDebitsByRange(
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
           SUM(isnull(mms.TotalDebit, 0))  as TotalDebit,
           SUM(isnull(mms.TotalCredit, 0)) as TotalCredit
    from dbo.getReportMonthsAndYears(@Start, @End) as m
             left outer join RelationshipDetails as rd
                             on rd.ViewerEntityId = @ViewerEntityId AND
                                rd.ViewerAirslipUserType = @ViewerAirslipUserType and
                                rd.OwnerEntityId = @OwnerEntityId and
                                rd.OwnerAirslipUserType = @OwnerAirslipUserType and
                                rd.PermissionType = 'Banking' and
                                rd.Allowed = 1
             left outer join BankAccountMetricSnapshots as mms
                             on mms.Month = m.Month and mms.Year = m.Year and mms.EntityId = rd.OwnerEntityId
                                 AND mms.AirslipUserType = rd.OwnerAirslipUserType
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
                                 and mms.CurrencyCode = @CurrencyCode
    group by m.Month, m.Year
    ORDER BY m.year asc, m.month asc
END