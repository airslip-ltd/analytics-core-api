CREATE or alter FUNCTION dbo.getYearMonths(
    @StartRow int = 1,
    @MawRows int = 12
)
    RETURNS TABLE
        AS
        return
            (
                with ROWCTE as
                         (
                             SELECT @StartRow as ROWNO
                             UNION ALL
                             SELECT ROWNO + 1
                             FROM ROWCTE
                             WHERE RowNo < @MawRows
                         )

                SELECT *
                FROM ROWCTE
            )