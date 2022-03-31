CREATE or alter FUNCTION dbo.getReportMonthsAndYears(
    @Start DATE,
    @End DATE
)
    RETURNS TABLE
        AS
        return
            (
                with MonthCTE (date)
                         AS
                         (
                             SELECT @Start
                             UNION ALL
                             SELECT DATEADD(month, 1, date)
                             from MonthCTE
                             where DATEADD(month, 1, date) <= @End
                         )
                select datepart(month, date) as Month, datepart(year, date) as Year
                from MonthCTE
            )
