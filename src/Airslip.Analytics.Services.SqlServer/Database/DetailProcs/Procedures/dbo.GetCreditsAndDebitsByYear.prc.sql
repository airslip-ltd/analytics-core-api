CREATE OR ALTER PROCEDURE dbo.GetCreditsAndDebitsByYear(
    @Year as int,
    @ViewerEntityId as nvarchar(50),
    @ViewerAirslipUserType as int,
    @OwnerEntityId as nvarchar(50),
    @OwnerAirslipUserType as int,
    @IntegrationId as nvarchar(max) = null
)
AS
BEGIN
    select m.ROWNO                         as Month,
           SUM(isnull(mms.TotalDebit, 0))  as TotalDebit,
           SUM(isnull(mms.TotalCredit, 0)) as TotalCredit
    from dbo.getYearMonths(1, 12) as m
             left outer join RelationshipDetails as rd
                             on rd.ViewerEntityId = @ViewerEntityId AND
                                rd.ViewerAirslipUserType = @ViewerAirslipUserType and
                                rd.OwnerEntityId = @OwnerEntityId and
                                rd.OwnerAirslipUserType = @OwnerAirslipUserType and
                                rd.PermissionType = 'Banking'
                                 and rd.Allowed = 1
             left outer join RelationshipHeaders as rh
                             on rh.Id = rd.RelationshipHeaderId and rh.EntityStatus = 1
             left outer join BankAccountMetricSnapshots as mms
                             on mms.Month = m.ROWNO and mms.Year = @Year and mms.EntityId = rd.OwnerEntityId
                                 AND mms.AirslipUserType = rd.OwnerAirslipUserType
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
    group by m.ROWNO
END