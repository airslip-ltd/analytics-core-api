CREATE or alter FUNCTION dbo.getMetricDateRange(
    @DayRange as int,
    @StatRange as int,
    @StartDate as date = null
)
    RETURNS
        @Dates TABLE
               (
                   Year              int,
                   Month             int,
                   Day               int,
                   EndCalendarDate   date,
                   StartCalendarDate date
               )
AS
BEGIN
    set @DayRange = @DayRange * -1

    Declare @EndDate as date

    set @StartDate = isnull(@StartDate, getutcdate())
    set @EndDate = dateadd(day, @DayRange * @StatRange, @StartDate);
    WITH Dates as (
        select datepart(year, @StartDate)          as Year,
               datepart(month, @StartDate)         as Month,
               datepart(day, @StartDate)           as Day,
               @Startdate                          as StartCalendarDate,
               dateadd(day, @DayRange, @Startdate) as EndCalendarDate
        union all
        select datepart(year, StartCalendarDate)          as Year,
               datepart(month, StartCalendarDate)         as Month,
               datepart(day, StartCalendarDate)           as Day,
               dateadd(day, @DayRange, StartCalendarDate) as EndCalendarDate,
               StartCalendarDate                          as StartCalendarDate
        from Dates
        where dateadd(day, @DayRange, StartCalendarDate) >= @EndDate
    )

    insert
    into @Dates
    (Year, Month, Day, EndCalendarDate, StartCalendarDate)
    select Year,
           Month,
           Day,
           EndCalendarDate,
           StartCalendarDate
    from Dates
    order by StartCalendarDate DESC
    offset 1 ROW
    OPTION (MAXRECURSION 50)

    RETURN;
end
