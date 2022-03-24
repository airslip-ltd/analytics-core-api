CREATE OR ALTER PROCEDURE dbo.GetCreditsAndDebitsByYear(
    @Year as int,
    @EntityId as nvarchar(50),
    @AirslipUserType as int,
    @IntegrationId as nvarchar(max) = null
)
AS
BEGIN
    select m.ROWNO as Month, SUM(isnull(mms.TotalDebit, 0)) as TotalDebit, SUM(isnull(mms.TotalCredit, 0)) as TotalCredit
    from dbo.getYearMonths(1, 12) as m
             left outer join BankAccountMetricSnapshots as mms
                             on mms.Month = m.ROWNO and mms.Year = @Year and mms.EntityId = @EntityId
                                 and mms.AirslipUserType = @AirslipUserType
                                 and (@IntegrationId is null OR mms.IntegrationId = @IntegrationId)
    group by m.ROWNO
END